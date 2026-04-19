using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Common.Domain
{
    public class BaseDomainEvent:INotification
    {
        public DateTime CreationDate { get; private set; }

        public BaseDomainEvent()
        {
            CreationDate = DateTime.Now;
        }
    }
}
