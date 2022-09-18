using LibraryIS.DataAccess.Repository.IRepository;
using LibraryIS.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryIS_6.Controllers
{
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchString = null)
        {
            IEnumerable<Genre> genre = null;

            ViewData["SearchString"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                genre = _unitOfWork.Genre.GetAll(g => g.Name.Contains(searchString));
            }
            else
            {
                genre = _unitOfWork.Genre.GetAll();
            }

            return View(genre);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Genre.Add(genre);
                _unitOfWork.Save();
                TempData["success"] = "Genre added successfully";

                return RedirectToAction("Index");
            }

            return View(genre);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var genreFromDb = _unitOfWork.Genre.GetFirstOrDefault(u => u.Id == id);

            if (genreFromDb == null)
            {
                return NotFound();
            }

            return View(genreFromDb);
        }

        //post
        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Genre.Update(genre);
                _unitOfWork.Save();
                TempData["success"] = "Genre updated successfully";

                return RedirectToAction("Index");
            }

            return View(genre);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var genreFromDb = _unitOfWork.Genre.GetFirstOrDefault(u => u.Id == id);

            if (genreFromDb == null)
            {
                return NotFound();
            }

            return View(genreFromDb);
        }

        //post
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var genreFromDb = _unitOfWork.Genre.GetFirstOrDefault(u => u.Id == id);
            if (genreFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Genre.Remove(genreFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Genre deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
