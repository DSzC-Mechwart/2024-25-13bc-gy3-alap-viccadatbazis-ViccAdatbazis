using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ViccDbContext _context;

        public IndexModel(ViccDbContext context)
        {
            _context = context;
        }

        public List<Vicc> Jokes { get; set; }

        public async Task OnGetAsync()
        {
            Jokes = await _context.Viccek
                .Where(j => j.Aktiv)
                .ToListAsync();
        }
    }
}
