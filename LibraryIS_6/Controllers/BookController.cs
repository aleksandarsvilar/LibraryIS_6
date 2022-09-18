using LibraryIS.DataAccess.Repository.IRepository;
using LibraryIS.Models;
using LibraryIS.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using LibraryIS.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;

namespace LibraryIS_6.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchString = null, string filter = null)
        {
            ViewData["SearchString"] = searchString;
            ViewData["Filter"] = filter;

            IEnumerable<Book> bookList = null;

            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(filter))
            {
                switch(filter)
                {
                    case "Author":
                        bookList = _unitOfWork.Book.GetAll(b => b.Author.FirstName.Contains(searchString) || b.Author.LastName.Contains(searchString), includeProperties: "Author,Genre,Inventory,Publisher");
                        break;
                    case "Title":
                        bookList = _unitOfWork.Book.GetAll(b => b.Name.Contains(searchString), includeProperties: "Author,Genre,Inventory,Publisher");
                        break;
                    case "Genre":
                        bookList = _unitOfWork.Book.GetAll(b => b.Genre.Name.Contains(searchString), includeProperties: "Author,Genre,Inventory,Publisher");
                        break;
                }
            }
            else
            {
                bookList = _unitOfWork.Book.GetAll(includeProperties: "Author,Genre,Inventory,Publisher");
            }

            return View(bookList);
        }

        public IActionResult List(string searchString = null, string filter = null)
        {
            ViewData["SearchString"] = searchString;
            ViewData["Filter"] = filter;

            IEnumerable<Book> bookList = null;

            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "Author":
                        bookList = _unitOfWork.Book.GetAll(b => b.Author.FirstName.Contains(searchString) || b.Author.LastName.Contains(searchString), includeProperties: "Author,Genre,Inventory,Publisher");
                        break;
                    case "Title":
                        bookList = _unitOfWork.Book.GetAll(b => b.Name.Contains(searchString), includeProperties: "Author,Genre,Inventory,Publisher");
                        break;
                    case "Genre":
                        bookList = _unitOfWork.Book.GetAll(b => b.Genre.Name.Contains(searchString), includeProperties: "Author,Genre,Inventory,Publisher");
                        break;
                }
            }
            else
            {
                bookList = _unitOfWork.Book.GetAll(includeProperties: "Author,Genre"); 
            }

            return View(bookList);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Book book = _unitOfWork.Book.GetFirstOrDefault(b => b.Id == id, includeProperties: "Author,Genre,Publisher,Inventory");
            return View(book);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            BookVM bookVM = new()
            {
                Book = new()
                {
                    Author = new (),
                    Genre = new ()
                },

                GenreList = _unitOfWork.Genre.GetAll().Select(u => new SelectListItem //projekcija
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                PublisherList = _unitOfWork.Publisher.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                AuthorList = _unitOfWork.Author.GetAll().Select(a => new SelectListItem
                {
                    Text = $"{a.FirstName} {a.LastName}",
                    Value = a.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return View(bookVM);
            }
            else
            {
                bookVM.Book = _unitOfWork.Book.GetFirstOrDefault(b => b.Id == id, includeProperties: "Inventory,Genre,Author,Publisher");  
            }

            return View(bookVM);
        }

        [HttpPost]
        public IActionResult Upsert(BookVM bookVM)
        {
            if (ModelState.IsValid)
            {
                if(bookVM.Book.Id == 0)
                {
                    _unitOfWork.Book.Add(bookVM.Book);
                    TempData["success"] = "Book added successfully";
                }
                else
                {
                    _unitOfWork.Book.Update(bookVM.Book);
                    TempData["success"] = "Book updated successfully";
                }

                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(bookVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var bookFromDb = _unitOfWork.Book.GetFirstOrDefault(u => u.Id == id, includeProperties: "Author,Genre");

            if (bookFromDb == null)
            {
                return NotFound();
            }

            var inventoryFromDb = _unitOfWork.Inventory.GetFirstOrDefault(i => i.Id == bookFromDb.InventoryId);

            if (inventoryFromDb == null)
            {
                return NotFound();
            }

            if (inventoryFromDb.IsAvailable == false)
            {
                TempData["warning"] = "Book is under loan and can' t be deleted.";
                return RedirectToAction("Index");
            }

            return View(bookFromDb);
        }

        //post
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var bookFromDb = _unitOfWork.Book.GetFirstOrDefault(u => u.Id == id, includeProperties: "Author,Genre,Publisher,Inventory");

            if (bookFromDb == null)
            {
                return NotFound();
            }

            //_unitOfWork.Book.Remove(bookFromDb);
            bookFromDb.IsDeleted = true; //soft delete
            _unitOfWork.Save();

            TempData["success"] = "Book deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
