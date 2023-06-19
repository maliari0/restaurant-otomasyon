using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using restaurant.Data;
using System.Linq;

namespace restaurant.ViewComponents
{
    public class CategoryList: ViewComponent //using için ctrl. bas - Kalıtım aldırdım
    {
        //veritabanına bağlanma işlemleri
        private readonly ApplicationDbContext _db;

        public CategoryList(ApplicationDbContext db) //yapıcı metot ctor tab tab yaptık
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        //Invoke bir IViewComponentsResult döndüren zaman uyumlu yöntem
        //Invoke=çağırmak
        {
            var category = _db.Categories.ToList();
            return View(category);
        }
    }
}
