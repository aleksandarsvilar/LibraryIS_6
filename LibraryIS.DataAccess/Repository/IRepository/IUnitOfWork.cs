using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        IGenreRepository Genre { get; }
        IMemberRepository Member { get; }
        IPublisherRepository Publisher { get; }
        ILoanRepository Loan { get; }   
        IInventoryRepository Inventory { get; }
        void Save();
    }
}
