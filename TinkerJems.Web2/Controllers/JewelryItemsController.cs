using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;
using static TinkerJems.Web2.Controllers.JewelryItemsController;

namespace TinkerJems.Web2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelryItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JewelryItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JewelryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JewelryItem>>> GetJewelryItems()
        {
            return await _context.JewelryItems.ToListAsync();
        }

        // GET: api/JewelryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JewelryItem>> GetJewelryItem(int id)
        {
            var jewelryItem = await _context.JewelryItems.FindAsync(id);

            if (jewelryItem == null)
            {
                return NotFound();
            }

            return jewelryItem;
        }
        //GET:api/JewelryItems/getJewelryByMaterial
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<JewelryItem>>> GetJewelryByMaterial(string material)
        {
           return await _context.JewelryItems.Where(m => m.Material == material).ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<JewelryItem>>> GetJewelryByCategory(string category)
        {

            return await _context.JewelryItems.Where(m => m.Category == category).ToListAsync();
            //var items = await _context.JewelryItems.
            //     Include(j => j.Tags)
            //     .ThenInclude(t => t.Tag)
            //     .Where(m => m.Category == category).ToListAsync();

            //return items;
        }

        // PUT: api/JewelryItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJewelryItem(int id, JewelryItem jewelryItem)
        {
            if (id != jewelryItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(jewelryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JewelryItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/JewelryItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<JewelryItem>> PostJewelryItem(JewelryItem jewelryItem)
        {
            _context.JewelryItems.Add(jewelryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJewelryItem", new { id = jewelryItem.Id }, jewelryItem);
        }

        //public class fileUploadApi
        //{
        //    public IFormFile files { get; set; }
        //}

        //[HttpPost]
        //public async Task<string> Post(fileUploadApi files)
        //{

        //}

        // DELETE: api/JewelryItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JewelryItem>> DeleteJewelryItem(int id)
        {
            var jewelryItem = await _context.JewelryItems.FindAsync(id);
            if (jewelryItem == null)
            {
                return NotFound();
            }

            _context.JewelryItems.Remove(jewelryItem);
            await _context.SaveChangesAsync();

            return jewelryItem;
        }

        private bool JewelryItemExists(int id)
        {
            return _context.JewelryItems.Any(e => e.Id == id);
        }
    }
}
