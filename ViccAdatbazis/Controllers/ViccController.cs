using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

[Route("api/[controller]")]
[ApiController]
public class ViccController : ControllerBase
{
    private readonly ViccDbContext _context;

    public ViccController(ViccDbContext context)
    {
        _context = context;
    }

    // GET: api/viccs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vicc>>> GetVicc(int page = 1)
    {
        int pageSize = 10;
        var viccek = await _context.Viccek
            .Where(j => j.Aktiv)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(viccek);
    }

    // POST: api/viccs
    [HttpPost]
    public async Task<ActionResult<Vicc>> PostVicc([FromBody] Vicc vicc)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Viccek.Add(vicc);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetVicc), new { id = vicc.Id }, vicc);
    }

    // POST: api/viccs/{id}/vote
    [HttpPost("{id}/vote")]
    public async Task<ActionResult<Vicc>> VoteVicc(int id, [FromBody] string type)
    {
        var vicc = await _context.Viccek.FindAsync(id);
        if (vicc == null) return NotFound();

        if (type == "like")
        {
            vicc.Tetszik++;
        }
        else if (type == "dislike")
        {
            vicc.NemTetszik++;
        }

        await _context.SaveChangesAsync();
        return Ok(vicc);
    }

    // PUT: api/viccs/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVicc(int id, [FromBody] Vicc vicc)
    {
        if (id != vicc.Id) return BadRequest();

        _context.Entry(vicc).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/viccs/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVicc(int id)
    {
        var vicc = await _context.Viccek.FindAsync(id);
        if (vicc == null) return NotFound();

        vicc.Aktiv = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}