using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Glossary.BusinessServices;
using Glossary.BusinessServices.DataAccess;
using Glossary.BusinessServices.Models;

namespace Glossary.Api.Controllers
{
    [Route("api/[controller]")]
    public class GlossariesController : Controller
    {
        private readonly IGlossaryService _glossaryService;
       
        public GlossariesController(IGlossaryService glossaryService)
        {
            _glossaryService = glossaryService;
        }
        [HttpGet]
        
        // GET: Glossaries
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.TermSort = string.IsNullOrEmpty(sortOrder) ? "term_desc" : "";
            ViewBag.DefinitionSort = sortOrder == "definition" ? "definition_desc" : "definition";
            var result = await _glossaryService.Get(sortOrder);
            return View(result);
        }
        [HttpGet]
        [Route("Details/id")]
        // GET: Glossaries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glossary = await _glossaryService.Get(id);
            if (glossary == null)
            {
                return NotFound();
            }
            return View(glossary);
        }
        [HttpGet]
        [Route("Create")]
        // GET: Glossaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Glossaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Term,Definition")] GlossaryItem glossary)
        {
            if (ModelState.IsValid)
            {
                await _glossaryService.Create(glossary);
                return RedirectToAction(nameof(Index));
            }
            return View(glossary);
        }
        [HttpGet]
        [Route("Edit/id")]
        // GET: Glossaries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glossary = await _glossaryService.Get(id);
            if (glossary == null)
            {
                return NotFound();
            }
            return View(glossary);
        }

        // POST: Glossaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/id")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Term,Definition")] GlossaryItem glossary)
        {
            if (id != glossary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ;
                    await _glossaryService.Update(glossary);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_glossaryService.Exists(glossary.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(glossary);
        }
        [HttpGet]
        [Route("Delete/id")]
        // GET: Glossaries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glossary = await _glossaryService.Get(id);
            if (glossary == null)
            {
                return NotFound();
            }

            return View(glossary);
        }

        // POST: Glossaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/id")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _glossaryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        
    }

    

    //public class GlossariesController : Controller
    //{
    //    private readonly GlossaryDbContext _context;

    //    public GlossariesController(GlossaryDbContext context)
    //    {
    //        _context = context;
    //    }
    //    [HttpGet]

    //    // GET: Glossaries
    //    public async Task<IActionResult> Index(string sortOrder)
    //    {
    //        ViewBag.TermSort = string.IsNullOrEmpty(sortOrder) ? "term_desc" : "";
    //        ViewBag.DefinitionSort = sortOrder == "definition" ? "definition_desc" : "definition";

    //        IQueryable<GlossaryItem> glossaryItemIQ = from g in _context.GlossaryItems
    //                                                  select g;

    //        switch (sortOrder)
    //        {
    //            case "term_desc":
    //                glossaryItemIQ = glossaryItemIQ.OrderByDescending(s => s.Term);
    //                break;
    //            case "definition":
    //                glossaryItemIQ = glossaryItemIQ.OrderBy(s => s.Definition);
    //                break;
    //            case "definition_desc":
    //                glossaryItemIQ = glossaryItemIQ.OrderByDescending(s => s.Definition);
    //                break;
    //            default:
    //                glossaryItemIQ = glossaryItemIQ.OrderBy(s => s.Term);
    //                break;
    //        }

    //        return View(await glossaryItemIQ.AsNoTracking().ToListAsync());
    //    }
    //    [HttpGet]
    //    [Route("Details/id")]
    //    // GET: Glossaries/Details/5
    //    public async Task<IActionResult> Details(Guid? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var glossary = await _context.GlossaryItems
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (glossary == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(glossary);
    //    }
    //    [HttpGet]
    //    [Route("Create")]
    //    // GET: Glossaries/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: Glossaries/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [Route("Create")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("Id,Term,Definition")] Models.GlossaryItem glossary)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            glossary.Id = Guid.NewGuid();
    //            _context.Add(glossary);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(glossary);
    //    }
    //    [HttpGet]
    //    [Route("Edit/id")]
    //    // GET: Glossaries/Edit/5
    //    public async Task<IActionResult> Edit(Guid? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var glossary = await _context.GlossaryItems.FindAsync(id);
    //        if (glossary == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(glossary);
    //    }

    //    // POST: Glossaries/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [Route("Edit/id")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Term,Definition")] Models.GlossaryItem glossary)
    //    {
    //        if (id != glossary.Id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(glossary);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!GlossaryExists(glossary.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(glossary);
    //    }
    //    [HttpGet]
    //    [Route("Delete/id")]
    //    // GET: Glossaries/Delete/5
    //    public async Task<IActionResult> Delete(Guid? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var glossary = await _context.GlossaryItems
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (glossary == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(glossary);
    //    }

    //    // POST: Glossaries/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    [Route("Delete/id")]
    //    public async Task<IActionResult> DeleteConfirmed(Guid id)
    //    {
    //        var glossary = await _context.GlossaryItems.FindAsync(id);
    //        _context.GlossaryItems.Remove(glossary);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool GlossaryExists(Guid id)
    //    {
    //        return _context.GlossaryItems.Any(e => e.Id == id);
    //    }
    //}



}
