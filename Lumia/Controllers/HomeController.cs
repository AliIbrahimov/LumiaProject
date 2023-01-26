using Lumia.DAL;
using Lumia.Models;
using Lumia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lumia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM()
            {
                TeamMembers = _context.TeamMembers.Include(x=>x.Icons).ToList(),
                Icons = _context.Icons.ToList(),
                Settings = _context.Settings.ToList(),
            };
            return View(vm);
        }
    }
}