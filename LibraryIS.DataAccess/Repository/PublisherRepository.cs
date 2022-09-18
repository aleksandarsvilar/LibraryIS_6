using LibraryIS.DataAccess.Repository.IRepository;
using LibraryIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.DataAccess.Repository
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PublisherRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Update(Publisher publisher)
        {
            _applicationDbContext.Update(publisher);
        }
    }
}
