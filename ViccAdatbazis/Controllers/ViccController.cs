using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ViccController : ControllerBase
    {
        //adatbazis kapcsolat
        private readonly ViccDbContext _context;


        //ctor (parameter atadas)
        public ViccController(ViccDbContext context)
        {
            _context = context;
        }




        // vicc listazas
        [HttpGet]
        public async Task<ActionResult<List<Vicc>>> GetViccek()
        {
            return await _context.Viccek.Where(x=>x.Aktiv).ToListAsync();
        }

        // egy vicc lekerdezese
        [HttpGet("{id}")]
        public async Task<ActionResult<Vicc>> GetVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            if (vicc == null)
            {
                return NotFound();
            }
            return vicc;
        }

        // ujvicc feltoltese
        [HttpPost]
        public async Task<ActionResult> PostVicc(Vicc ujVicc)
        {
            _context.Viccek.Add(ujVicc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVicc", new {id = ujVicc.Id}, ujVicc);
        }

        // vicc modositasa
        [HttpPut("{id}")]
        public async Task<ActionResult> PutVicc(int id, Vicc modositottVicc)
        {
            if (id != modositottVicc.Id)
            {
                return BadRequest();
            }

            _context.Entry(modositottVicc).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // vicc torlese
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVicc(int id)
        {
            var torlendoVicc = await _context.Viccek.FindAsync(id);
            if(torlendoVicc == null) 
            { 
                return NotFound(); 
            }
            if (torlendoVicc.Aktiv)
            {
                torlendoVicc.Aktiv = false;
                _context.Entry(torlendoVicc).State = EntityState.Modified;
            }
            else
            {
                _context.Viccek.Remove(torlendoVicc);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
