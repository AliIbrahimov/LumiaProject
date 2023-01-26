using Lumia.DAL;
using Lumia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Areas.admin.Controllers
{
    [Area("admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var settings = _context.Settings.ToList();
            return View(settings);
        }
        public IActionResult Edit(int id)
        {
           var setting = _context.Settings.Find(id);
            return View(setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Setting setting)
        {
            var existData = _context.Settings.Find(setting.Id);
            if (existData is not null)
            {
                existData.TeamName = setting.TeamName;
                existData.TeamTitle = setting.TeamTitle;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }
    }
}
