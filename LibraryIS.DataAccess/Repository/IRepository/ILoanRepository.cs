using LibraryIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.DataAccess.Repository.IRepository
{
    public interface ILoanRepository : IRepository<Loan>
    {
        void Update(Loan loan);
    }
}
