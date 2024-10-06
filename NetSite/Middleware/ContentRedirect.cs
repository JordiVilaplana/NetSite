using NetSite.Pages;
using NetSite.Services;

namespace NetSite.Middleware;

public class ContentRedirect
{
    private readonly RequestDelegate _next;
    private readonly StaticContentService _pagesService;

    public ContentRedirect(
        RequestDelegate next,
        StaticContentService staticContentService)
    {
        _next = next;
        _pagesService = staticContentService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path;
        var acceptHeader = context.Request.Headers.Accept;

        // Check if the request asks for CSS or JS files
        if (acceptHeader.Any(s => s?.Contains("text/css") ?? false) ||
            acceptHeader.Any(s => s?.Contains("text/javascript") ?? false))
        {
            await _next(context);
            return;
        }

        // Check if the request starts with "/admin"
        if (path.StartsWithSegments("/admin", StringComparison.InvariantCultureIgnoreCase))
        {
            await _next(context);
            return;
        }

        // Check if the request starts with "/error"
        if (path.StartsWithSegments("/error", StringComparison.InvariantCultureIgnoreCase))
        {
            await _next(context);
            return;
        }

        // "/index" hack
        if (path.Equals("/"))
        {
            path = "/index";
        }

        // Redirect all other requests to "/ContentPage"
        context.Request.Path = "/ContentPage";
        var content = await _pagesService.GetByPathAsync(path);
        context.Items[ContentPageModel.ContentKey] = content;

        await _next(context);
    }
}
