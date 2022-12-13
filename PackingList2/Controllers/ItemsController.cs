using Microsoft.AspNetCore.Mvc;
using PackingList2.Models;


namespace PackingList2.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AppDbContext _db;

        public ItemsController(AppDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var items = _db.Items.ToList();

            if (items == null)
            {
                return RedirectToAction(nameof(Error));
            }

            return View(items);
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,Name,Category,Count,Notes")] Item item)
        {
            if (ModelState.IsValid)
            {
                _db.Add(item);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Edit([Bind("Id,Name,Category,Count,Notes")] Item item)
        {
            if (item.Notes == null)
            {
                item.Notes = " ";
            }

            _db.Update(item);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _db.Items.Find(id);

            return View(item);
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Delete([Bind("Id,Name,Category,Count,Notes")] Item item)
        {
            _db.Items.Remove(item);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public IActionResult Index([FromForm] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction(nameof(Index));
            }

            var items = _db.Items.Where(
                i => FuzzySharp.Fuzz.Ratio(
                    i.Name.ToLower(), query
                ) > 80
            ).ToList();
            return View(items);
        }
    }
}