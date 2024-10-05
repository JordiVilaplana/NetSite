using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetSite.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
}
