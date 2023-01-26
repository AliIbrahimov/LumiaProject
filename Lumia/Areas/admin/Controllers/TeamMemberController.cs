using Lumia.DAL;
using Lumia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lumia.Areas.admin.Controllers
{
    [Area("admin")]
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TeamMemberController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        { 
            var membersOfTeam = _context.TeamMembers.Include(x=>x.Icons).ToList();
            return View(membersOfTeam);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeamMember teamMember)
        {
            if (!ModelState.IsValid) return View(teamMember);
            if (teamMember.FormFIle is not null)
            {
                string imageName = Guid.NewGuid() + teamMember?.FormFIle?.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/img/team", imageName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    teamMember?.FormFIle?.CopyTo(fileStream);
                }
                teamMember.Image = imageName;
                _context.TeamMembers.Add(teamMember);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(teamMember);
            
        }
        public IActionResult Edit(int id)
        {
            var existMember = _context.TeamMembers.Find(id);
            return View(existMember);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TeamMember teamMember)
        {
            var existMember = _context.TeamMembers.Find(teamMember.Id);
            if (teamMember.FormFIle is not null)
            {
                string imageName = Guid.NewGuid() + teamMember?.FormFIle?.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/img/team", imageName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    teamMember?.FormFIle?.CopyTo(fileStream);
                }
                existMember.Image = imageName;
                existMember.Name = teamMember?.Name;
                existMember.Position = teamMember.Position;
                existMember.Title = teamMember.Title;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(teamMember);
        }
        public IActionResult Delete(int id)
        {
            var existMember = _context.TeamMembers.Find(id);
            _context.Remove(existMember);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
