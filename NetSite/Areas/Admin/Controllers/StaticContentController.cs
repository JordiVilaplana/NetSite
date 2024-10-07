using Microsoft.AspNetCore.Mvc;
using NetSite.Models;
using NetSite.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetSite.Areas.Admin.Controllers;

[Area("Admin")]
public class StaticContentController : Controller
{
    private readonly StaticContentService _service;

    public StaticContentController(StaticContentService service)
    {
        _service = service;
    }

    // GET: StaticContentController
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var contentList = await _service.ListAsync();
        return View("Index", contentList);
    }

    // GET: StaticContentController/Create
    [HttpGet]
    public ActionResult Create()
    {
        return View("New");
    }

    // POST: StaticContentController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(StaticContent data)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateAsync(data);
            return RedirectToAction(nameof(Index));
        }
        return View("New");
    }

    // GET: StaticContentController/Edit/5
    public async Task<ActionResult> Edit(string id)
    {
        var content = await _service.GetAsync(id);

        if (content is null)
            return NotFound();

        return View("Edit", content);
    }

    // POST: StaticContentController/Edit/5
    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(string id, StaticContent data)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(id, data);
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Edit), id);
    }

    // GET: StaticContentController/Delete/5
    [HttpGet]
    public async Task<ActionResult> Delete(string id)
    {
        var content = await _service.GetAsync(id);

        if (content is null)
            return NotFound();

        return View("Delete", content);
    }

    // POST: StaticContentController/Delete/5
    [HttpDelete]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(string id, StaticContent data)
    {
        if (ModelState.IsValid)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete), id);
    }
}
