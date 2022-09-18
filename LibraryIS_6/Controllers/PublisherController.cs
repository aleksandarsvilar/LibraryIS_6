using LibraryIS.DataAccess.Repository.IRepository;
using LibraryIS.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryIS_6.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchString = null)
        {
            IEnumerable<Publisher> publisherList = null;

            ViewData["SearchString"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                publisherList = _unitOfWork.Publisher.GetAll(p => p.Name.Contains(searchString));
            }
            else
            {
                publisherList = _unitOfWork.Publisher.GetAll();
            }

            return View(publisherList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        public IActionResult Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Publisher.Add(publisher);
                _unitOfWork.Save();

                TempData["success"] = "Publisher added successfully";

                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var publisherFromDb = _unitOfWork.Publisher.GetFirstOrDefault(u => u.Id == id);

            if (publisherFromDb == null)
            {
                return NotFound();
            }

            return View(publisherFromDb);
        }

        //post
        [HttpPost]
        public IActionResult Edit(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Publisher.Update(publisher);
                _unitOfWork.Save();

                TempData["success"] = "Publisher updated successfully";

                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        //get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var publisherFromDb = _unitOfWork.Publisher.GetFirstOrDefault(u => u.Id == id);

            if (publisherFromDb == null)
            {
                return NotFound();
            }

            return View(publisherFromDb);
        }

        //post
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var publisherFromDb = _unitOfWork.Publisher.GetFirstOrDefault(u => u.Id == id);
            if (publisherFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Publisher.Remove(publisherFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Publisher deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
