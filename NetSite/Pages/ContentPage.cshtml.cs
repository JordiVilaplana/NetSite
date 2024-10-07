using Microsoft.AspNetCore.Mvc.RazorPages;
using NetSite.Models;

namespace NetSite.Pages;

public class ContentPageModel : PageModel
{
    public const string ContentKey = "Content";

    public StaticContent? PageContent { get; private set; }

    public void OnGet()
    {
        PageContent = HttpContext.Items[ContentKey] as StaticContent;

        ViewData["Title"] = PageContent!.Title;
    }
}
