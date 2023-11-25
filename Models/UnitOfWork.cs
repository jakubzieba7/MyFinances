using MyFinances.Models.Domains;
using MyFinances.Models.Repositories;

namespace MyFinances.Models
{
    public class UnitOfWork
    {
        private readonly MyFinancesContext _context;

        public UnitOfWork(MyFinancesContext context)
        {
            _context = context;
            Operation = new OperationRepository(context);
        }

        public OperationRepository Operation { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}
