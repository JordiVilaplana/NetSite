using Microsoft.AspNetCore.Mvc;
using NetSite.Models;
using NetSite.Services;

namespace NetSite.Areas.Admin.Controllers;

[Area("Admin")]
public class NavItemController : Controller
{
    private readonly NavItemsService _service;

    public NavItemController(NavItemsService service)
    {
        _service = service;
    }

    // GET: NavItemController
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var items = new Dictionary<string, IEnumerable<NavItem>>();
        var tags = await _service.ListTagsAsync();
        foreach (var tag in tags)
        {
            items.Add(tag, await _service.ListAsync(tag));
        }
        return View("Index", items);
    }

    // GET: NavItemController/Create
    [HttpGet]
    public ActionResult Create()
    {
        return View("Create");
    }

    // POST: NavItemController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(NavItem data)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateAsync(data);
            return RedirectToAction(nameof(Index));
        }
        return View("Create");
    }

    // GET: NavItemController/Edit/5
    public async Task<ActionResult> Edit(string id)
    {
        var content = await _service.GetAsync(id);

        if (content is null)
            return NotFound();

        return View("Edit", content);
    }

    // POST: NavItemController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(string id, NavItem data)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(data);
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Edit), id);
    }

    // GET: NavItemController/Delete/5
    [HttpGet]
    public async Task<ActionResult> Delete(string id)
    {
        var content = await _service.GetAsync(id);

        if (content is null)
            return NotFound();

        return View("Delete", content);
    }

    // POST: NavItemController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(string id, NavItem data)
    {
        if (ModelState.IsValid)
        {
            await _service.DeleteAsync(data);
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete), id);
    }
}
