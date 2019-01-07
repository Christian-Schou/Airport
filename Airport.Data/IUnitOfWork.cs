using Airport.Data.IRepository;

namespace Airport.Data
{
    public interface IUnitOfWork
    {
        IFlightRepository Flight { get; }
        bool Complete();
    }
}