using Microsoft.AspNetCore.Mvc;
using restaurant.Data;
using System.Linq;

namespace restaurant.ViewComponents
{
    public class Iletisimim : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public Iletisimim(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var iletisim = _db.Iletisimims.ToList();
            return View(iletisim);
        }
    }
}
