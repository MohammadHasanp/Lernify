using AutoMapper;
using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using TicketModule.Context;
using TicketModule.Domain;
using TicketModule.Services.DTOs.Command;
using TicketModule.Services.DTOs.Query;

namespace TicketModule.Services;

public interface ITicketService
{
    public Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command);
    public Task<OperationResult> SendMessageInTicket(SendTicketCommand command);
    public Task<OperationResult> CloseTicket(Guid ticketId);
    public Task<TicketDto?> GetTicketById(Guid id);
    public Task<TicketFilterResult> GetTicketByFilter(TicketFilterParams @params);
}


class TicketService : ITicketService
{
    private readonly TicketContext _context;
    private readonly IMapper _mapper;

    public TicketService(IMapper mapper, TicketContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<OperationResult> CloseTicket(Guid ticketId)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        if (ticket == null)
            return OperationResult.NotFound();

        ticket.TicketStatus = TicketStatus.Closed;
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command)
    {
        var ticket = _mapper.Map<Ticket>(command);
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return OperationResult<Guid>.Success(ticket.Id);
    }

    public async Task<TicketFilterResult> GetTicketByFilter(TicketFilterParams @params)
    {
        var result = _context.Tickets.AsQueryable();

        if (@params.UserId != null)
            result = result.Where(t => t.UserId == @params.UserId);

        var skip = (@params.PageId - 1) * @params.Take;
        var model = new TicketFilterResult()
        {
            Datas = await result.Skip(skip).Take(@params.Take).Select(t => new TicketFilterData()
            {
                CreationDate = t.CreationDate,
                Id = t.Id,
                IsDelete = t.IsDelete,
                TicketStatus = t.TicketStatus,
                Title = t.Title,
                UserId = t.UserId,
            }).ToListAsync()
        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }


    public async Task<TicketDto?> GetTicketById(Guid id)
    {
        var ticket = await _context.Tickets.Include(t => t.Messages).FirstOrDefaultAsync(t => t.Id == id);
        if (ticket == null)
            return null;


        //return new TicketDto()
        //{
        //    CreationDate = ticket.CreationDate,
        //    Id = ticket.Id,
        //    Mobile = ticket.Mobile,
        //    OwnerFullName = ticket.OwnerFullName,
        //    Text = ticket.Text,
        //    Title = ticket.Title,
        //    UserId = ticket.UserId,
        //    Messages = 
        //};
        return _mapper.Map<TicketDto?>(ticket);
    }

    public async Task<OperationResult> SendMessageInTicket(SendTicketCommand command)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == command.TicketId);
        if (ticket == null)
            return OperationResult.NotFound();

        var message = new TicketMessage()
        {
            UserId = ticket.UserId,
            OwnerFullName = command.OwnerFullName,
            Text = command.Text.SanitizeText(),
            TicketId = ticket.Id,
        };

        if (ticket.UserId == command.UserId)
            ticket.TicketStatus = TicketStatus.Pending;
        else
            ticket.TicketStatus = TicketStatus.Answered;


        _context.TicketMessages.Add(message);
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
        return OperationResult.Success();
    }
}