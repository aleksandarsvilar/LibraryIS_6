using LibraryIS.DataAccess.Repository.IRepository;
using LibraryIS.Models;
using LibraryIS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryIS_6.Controllers
{
    public class LoanController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoanController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Loan> loans = _unitOfWork.Loan.GetAll(includeProperties: "Inventory,Member");

            return View(loans);
        }

        public IActionResult Create()
         {
             LoanVM loanVM = new()
             {
                 Loan = new()
                 {
                     Member = new(),
                     Inventory = new()
                 },

                 BooksList = _unitOfWork.Book.GetAll(includeProperties: "Author,Inventory").Select(u => new SelectListItem //projekcija
                 {
                     Text = $"[{u.Name}], [{u.Author.FirstName + " " + u.Author.LastName}]",
                     Value = u.InventoryId.ToString()
                 }),

                 MembersList = _unitOfWork.Member.GetAll().Select(u => new SelectListItem
                 {
                     Text = u.FirstName + " " + u.LastName,
                     Value = u.Id.ToString()
                 }),
             };

             return View(loanVM);
         }

         //post
         [HttpPost]
         public IActionResult Create(LoanVM loanVM)
         {
             if (ModelState.IsValid)
             {
                var inventory = _unitOfWork.Inventory.GetFirstOrDefault(i => i.Id == loanVM.Loan.InventoryId);

                if (inventory.IsAvailable == true)
                {
                    inventory.IsAvailable = false;

                    _unitOfWork.Loan.Add(loanVM.Loan);
                    _unitOfWork.Save();

                    TempData["success"] = "Loan added successfully";
                }
                else
                {
                    TempData["warning"] = "Book is not available";
                }

                return RedirectToAction("Index");
             }

             return View(loanVM);
         }

        //GET
        public IActionResult Edit(int? id)
        {
            LoanVM loanVM = new()
            {
                Loan = new()
                {
                    Member = new(),
                    Inventory = new()
                },

                BooksList = _unitOfWork.Book.GetAll(includeProperties: "Author,Inventory").Select(u => new SelectListItem //projekcija
                {
                    Text = $"[{u.Name}], [{u.Author.FirstName + u.Author.LastName}]",
                    Value = u.InventoryId.ToString()
                }),

                MembersList = _unitOfWork.Member.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FirstName + " " + u.LastName,
                    Value = u.Id.ToString()
                }),
            };

            loanVM.Loan = _unitOfWork.Loan.GetFirstOrDefault(l => l.Id == id, includeProperties: "Inventory"); //edit*/

            return View(loanVM);
        }

        //post
        [HttpPost]
        public IActionResult Edit(LoanVM loanVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Loan.Update(loanVM.Loan);
                _unitOfWork.Save();

                TempData["success"] = "Loan updated successfully";

                return RedirectToAction("Index");
            }

            return View(loanVM.Loan);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var loanFromDb = _unitOfWork.Loan.GetFirstOrDefault(u => u.Id == id, includeProperties: "Member,Inventory");

            if (loanFromDb == null)
            {
                return NotFound();
            }

            return View(loanFromDb);
        }

        //post
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var loanFromDb = _unitOfWork.Loan.GetFirstOrDefault(u => u.Id == id, includeProperties: "Member,Inventory");
            if (loanFromDb == null)
            {
                return NotFound();
            }

            var inventory = _unitOfWork.Inventory.GetFirstOrDefault(i => i.Id == loanFromDb.InventoryId);
            inventory.IsAvailable = true;

            _unitOfWork.Loan.Remove(loanFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Loan removed successfully";

            return RedirectToAction("Index");
        }
    }
}
