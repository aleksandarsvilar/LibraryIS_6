using LibraryIS.DataAccess.Repository.IRepository;
using LibraryIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Models;

namespace LibraryIS.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            Book = new BookRepository(_applicationDbContext);
            Author = new AuthorRepository(_applicationDbContext);
            Genre = new GenreRepository(_applicationDbContext);
            Member = new MemberRepository(_applicationDbContext);
            Publisher = new PublisherRepository(_applicationDbContext);
            Loan = new LoanRepository(_applicationDbContext);
            Inventory = new InventoryRepository(_applicationDbContext);
        }

        public IBookRepository Book { get; private set; }

        public IAuthorRepository Author { get; private set; }

        public IGenreRepository Genre { get; private set; }

        public IMemberRepository Member { get; private set; }

        public IPublisherRepository Publisher { get; private set; }

        public ILoanRepository Loan { get; private set; }

        public IInventoryRepository Inventory { get; private set; }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
