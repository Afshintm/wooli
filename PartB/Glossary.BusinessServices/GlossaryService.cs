using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Glossary.BusinessServices.DataAccess;
using Glossary.BusinessServices.Models;
using Microsoft.EntityFrameworkCore;

namespace Glossary.BusinessServices
{
    public interface IGlossaryService
    {
        Task<List<GlossaryItem>> Get(string sortOrder);
        Task<GlossaryItem> Get(Guid? id);
        Task<int> Create(GlossaryItem item);
        Task<int> Update(GlossaryItem item);
        Task<int> Delete(Guid id);
        bool Exists(Guid id);
    }

    public class GlossaryService: IGlossaryService
    {
        private readonly GlossaryDbContext _context;

        public GlossaryService(GlossaryDbContext context)
        {
            _context = context;
        }

        public async Task<List<GlossaryItem>> Get(string sortOrder)
        {
            IQueryable<GlossaryItem> glossaryItemIq = from g in _context.GlossaryItems
                select g;

            switch (sortOrder)
            {
                case "term_desc":
                    glossaryItemIq = glossaryItemIq.OrderByDescending(s => s.Term);
                    break;
                case "definition":
                    glossaryItemIq = glossaryItemIq.OrderBy(s => s.Definition);
                    break;
                case "definition_desc":
                    glossaryItemIq = glossaryItemIq.OrderByDescending(s => s.Definition);
                    break;
                default:
                    glossaryItemIq = glossaryItemIq.OrderBy(s => s.Term);
                    break;
            }

            return await glossaryItemIq.AsNoTracking().ToListAsync();
        }

        public async Task<GlossaryItem> Get(Guid? id)
        {
             var item = await _context.GlossaryItems.FirstOrDefaultAsync(m => m.Id == id);
            return item;
        }

        public async Task<int> Create(GlossaryItem item)
        {
            item.Id = Guid.NewGuid();
            _context.Add(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(GlossaryItem item)
        {
            _context.Update(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var glossary = await _context.GlossaryItems.FindAsync(id);
            _context.GlossaryItems.Remove(glossary);
            return await _context.SaveChangesAsync();
        }
        public bool Exists(Guid id)
        {
            return _context.GlossaryItems.Any(e => e.Id == id);
        }
    }
}
