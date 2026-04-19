using Common.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Serialization;

namespace Lernify.Web.Infrastructure.RazorUtil;

public class BaseRazorFilter<TFilterParam> : BaseRazorPage where TFilterParam : BaseFilterParam
{
    [BindProperty(SupportsGet = true)]
    public required TFilterParam FilterParams { get; set; }
}
