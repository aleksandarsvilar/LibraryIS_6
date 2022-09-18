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
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public InventoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Update(Inventory inventory)
        {
            _applicationDbContext.Update(inventory);
        }
    }
}
