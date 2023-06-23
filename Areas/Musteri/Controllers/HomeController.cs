using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;
using restaurant.Data;
using restaurant.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.Areas.Musteri.Controllers // namespace düzenleme ile homecontroller yolunu düzenledik
{
	[Area("Musteri")]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toast;
        private readonly IWebHostEnvironment _whe;
        private object iletisim;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IToastNotification toast, IWebHostEnvironment whe)
        {
            _logger = logger;
            _db = db;
            _toast = toast;
            _whe = whe;
        }

        public IActionResult Index()
        {
            //özel menüleri ana sayfada getirme işlemi
            var menu = _db.Menus.Where(i=>i.OzelMenu).ToList();
            return View(menu);
        }
        public IActionResult CategoryDetails(int? id)
        {
            //kategorilere ait menüleri getirme
            var menu = _db.Menus.Where(i=>i.CategoryId == id).ToList();
            ViewBag.KategoriId = id;
            return View(menu);
        }
        public IActionResult Iletisim()
        {
            return View();
        }
        // POST: Yonetici/Iletisim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Iletisim([Bind("Id,Name,Email,Telefon,Mesaj")]Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                iletisim.Tarih = DateTime.Now;
                _db.Add(iletisim);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Mesajınız başarıyla iletildi.");
                return RedirectToAction(nameof(Index));
            }
            return View(iletisim);
        }

        public IActionResult Blog()
        {
            return View();
        }

        // POST: Yonetici/Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Blog(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                blog.Tarih = DateTime.Now;
                //yorum tarihi müşteri girmeyecek, sistemden biz çekeceğiz. şuan için normal yapalım.
                var files = HttpContext.Request.Form.Files;
                //if dosya kontrolü yaptım
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    //resim eklemek için path metodu kullanılır
                    //resim kaydetmek istediğim dosya yolunu belirttim
                    var uploads = Path.Combine(_whe.WebRootPath, @"WebSite\menu");
                    var extn = Path.GetExtension(files[0].FileName);
                    //menü resmini if ile kontrol ettim
                    //menü alanı boş değil ise resimleri ekler
                    if (blog.Image != null)
                    {
                        var ImagePath = Path.Combine(_whe.WebRootPath, blog.Image.TrimStart('\\'));
                        //menü silinirse menüye ait resmi de menü dosyasından silmesini sağladım
                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extn), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    blog.Image = @"\WebSite\menu\" + fileName + extn;
                }
                _db.Add(blog);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Yorumunuz iletildi, onaylanan yorumları sayfamızdan görebilirsiniz.Teşekkür ederiz.");
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
        public IActionResult Hakkında()
        {
            var hakkında = _db.Hakkındas.ToList();
            return View(hakkında);
        }
        public IActionResult Galeri()
        {
            var galeri = _db.Galeris.ToList();
            return View(galeri);
        }
        public IActionResult Rezervasyon()
        {
            return View();
        }

        // POST: Yonetici/Rezervasyon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rezervasyon([Bind("Id,Name,Email,TelefonNo,Sayi,Saat,Tarih")] Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                _db.Add(rezervasyon);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Rezervasyon işleminiz gerçekleşmiştir.");
                return RedirectToAction(nameof(Index));
            }
            return View(rezervasyon);
        }

        public IActionResult Menu()
        {
            //menu sayfasına tüm meüleri getirme işlemi
            var menu = _db.Menus.ToList();
            return View(menu);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
