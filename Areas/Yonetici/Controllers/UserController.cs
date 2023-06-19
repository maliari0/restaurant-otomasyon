using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant.Data;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
    [Authorize]
    public class UserController : Controller
    {


        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.AppUsers.ToList();
            var role = _context.Roles.ToList();
            var userRol = _context.UserRoles.ToList();
            foreach(var item in users)
            {
                var roleId = userRol.FirstOrDefault(i => i.UserId == item.Id).RoleId; //ilişkili farklı tablolardaki user role getirdim
                item.Role = role.FirstOrDefault(u=>u.Id==roleId).Name;
            }
            return View(users);

        }
        // GET: Yonetici/Category/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Yonetici/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.AppUsers.FindAsync(id);
            _context.AppUsers.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

