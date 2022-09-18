using LibraryIS.DataAccess.Repository.IRepository;
using LibraryIS.Models;
using LibraryIS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryIS_6.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchString = null)
        {
            ViewData["SearchString"] = searchString;

            IEnumerable<Author> items = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = _unitOfWork.Author.GetAll(a => a.FirstName.Contains(searchString) || a.LastName.Contains(searchString));
            }
            else
            {
                items = _unitOfWork.Author.GetAll();
            }

            return View(items);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Author.Add(author);
                _unitOfWork.Save();
                
                TempData["success"] = "Author added successfully";

                return RedirectToAction("Index");
            }

            return View(author);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var authorFromDb = _unitOfWork.Author.GetFirstOrDefault(u => u.Id == id);

            if (authorFromDb == null)
            {
                return NotFound();
            }

            return View(authorFromDb);
        }

        //post
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Author.Update(author);
                _unitOfWork.Save();
                TempData["success"] = "Author updated successfully";

                return RedirectToAction("Index");
            }

            return View(author);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var authorFromDb = _unitOfWork.Author.GetFirstOrDefault(u => u.Id == id);

            if (authorFromDb == null)
            {
                return NotFound();
            }

            return View(authorFromDb);
        }

        //post
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var authorFromDb = _unitOfWork.Author.GetFirstOrDefault(u => u.Id == id);
            if (authorFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Author.Remove(authorFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Author deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
