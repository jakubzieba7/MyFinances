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

        public Operation Get(int id)
        {
            return _context.Operations.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Operation> Get(PaginationFilter paginationFilter)
        {
            return _context.Operations
                            .OrderBy(x => x.Name)
                            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                            .Take(paginationFilter.PageSize)
                            .ToList();
        }

        public int Count()
        {
            return _context.Operations.Count();
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
