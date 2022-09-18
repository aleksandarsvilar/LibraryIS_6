using LibraryIS.DataAccess.Repository.IRepository;
using LibraryIS.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryIS_6.Controllers
{
    public class MemberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchString = null)
        {
            IEnumerable<Member> items = null;

            ViewData["SearchString"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                items = _unitOfWork.Member.GetAll(m => m.FirstName.Contains(searchString) || m.LastName.Contains(searchString));
            }
            else
            {
                items = _unitOfWork.Member.GetAll();
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
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Member.Add(member);
                _unitOfWork.Save();

                TempData["success"] = "Member added successfully";

                return RedirectToAction("Index");
            }

            return View(member);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var memberFromDb = _unitOfWork.Member.GetFirstOrDefault(u => u.Id == id);

            if (memberFromDb == null)
            {
                return NotFound();
            }

            return View(memberFromDb);
        }

        //post
        [HttpPost]
        public IActionResult Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Member.Update(member);
                _unitOfWork.Save();

                TempData["success"] = "Member updated successfully";

                return RedirectToAction("Index");
            }

            return View(member);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var loanFromDb = _unitOfWork.Loan.GetFirstOrDefault(l => l.MemberId == id);

            if (loanFromDb != null)
            {
                TempData["warning"] = "Member loaned book/s and can' t be deleted";
                return RedirectToAction("Index");
            }

            var memberFromDb = _unitOfWork.Member.GetFirstOrDefault(u => u.Id == id);

            if (memberFromDb == null)
            {
                return NotFound();
            }

            return View(memberFromDb);
        }

        //post
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var memberFromDb = _unitOfWork.Member.GetFirstOrDefault(u => u.Id == id);

            if (memberFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Member.Remove(memberFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Member deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
