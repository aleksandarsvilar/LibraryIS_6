using LibraryIS.DataAccess.Repository.IRepository;
using LibraryIS.Models;
using LibraryIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.DataAccess.Repository
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LoanRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Update(Loan loan)
        {
            _applicationDbContext.Update(loan);
        }
    }
}
