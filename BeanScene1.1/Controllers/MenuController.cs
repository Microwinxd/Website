using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeanScene1._1.Controllers
{
    public class MenuController : Controller
    {
        // GET: MenusController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MenusController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenusController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenusController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenusController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
