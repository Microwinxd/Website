
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeanScene1._1.Models;
using BeanScene1._1.Data;

public class MenuController : Controller
{
    private readonly ApplicationDbContext _context;

    public MenuController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: MENUS
    public async Task<IActionResult> Index()

    {
        var menus = await _context.Menus.ToListAsync();
        return View(menus);

    }

    // User version
    public async Task<IActionResult> WebsiteMenu()
    {
        var menus = await _context.Menu.ToListAsync();

        if (menus == null)
        {
            menus = new List<Menu>();
        }

        return View(menus);
    }

    // GET: MENUS/Details/5
    public async Task<IActionResult> Details(int? menuid)
    {
        if (menuid == null)
        {
            return NotFound();
        }

        var menu = await _context.Menu
            .FirstOrDefaultAsync(m => m.MenuId == menuid);
        if (menu == null)
        {
            return NotFound();
        }

        return View(menu);
    }

    // GET: MENUS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MENUS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MenuId,ItemName,ItemType,ItemPrice")] Menu menu)
    {
        if (ModelState.IsValid)
        {
            _context.Add(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(menu);
    }

    // GET: MENUS/Edit/5
    public async Task<IActionResult> Edit(int? menuid)
    {
        if (menuid == null)
        {
            return NotFound();
        }

        var menu = await _context.Menu.FindAsync(menuid);
        if (menu == null)
        {
            return NotFound();
        }
        return View(menu);
    }

    // POST: MENUS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? menuid, [Bind("MenuId,ItemName,ItemType,ItemPrice")] Menu menu)
    {
        if (menuid != menu.MenuId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(menu);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(menu.MenuId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(menu);
    }

    // GET: MENUS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Menu? menus;


         menus = await _context.Menu
            .FirstOrDefaultAsync(m => m.MenuId == id);
        if (menus == null)
        {
            return NotFound();
        }

        return View(menus);
    }

    // POST: MENUS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        Menu? menus;

         menus = await _context.Menu.FindAsync(id);
        if (menus == null)
        
            return NotFound();


        _context.Menu.Remove(menus);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MenuExists(int? menuid)
    {
        return _context.Menu.Any(e => e.MenuId == menuid);
    }
}
