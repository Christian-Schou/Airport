using Airport.Data.IRepository;
using Airport.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            Flight = new FlightRepository(_applicationDbContext);

        }


        public IFlightRepository Flight { get; private set; }

        public bool Complete()
        {
            return (_applicationDbContext.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}
