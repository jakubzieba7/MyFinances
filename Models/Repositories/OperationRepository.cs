using MyFinances.Models.Domains;

namespace MyFinances.Models.Repositories
{
    public class OperationRepository
    {

        private readonly MyFinancesContext _context;

        public OperationRepository(MyFinancesContext context)
        {
            _context = context;
        }

        public IEnumerable<Operation> Get()
        {
            return _context.Operations;
        }

        public Operation Get(int id)
        {
            return _context.Operations.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Operation operation)
        {
            operation.Date = DateTime.Now;
            _context.Operations.Add(operation);
        }

        public void Update(Operation operation)
        {
            var operationToUpdate = _context.Operations.FirstOrDefault(x => x.Id == operation.Id);
            operationToUpdate.Value = operation.Value;
            operationToUpdate.CategoryId = operation.CategoryId;
            operationToUpdate.Name = operation.Name;
            operationToUpdate.Description = operation.Description;
        }

        public void Delete(int id)
        {
            var operationToDelete = _context.Operations.FirstOrDefault(x => x.Id == id);

            _context.Operations.Remove(operationToDelete);
        }
    }
}
