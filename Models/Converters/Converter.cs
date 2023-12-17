using MyFinances.Core.Dtos;
using MyFinances.Models.Domains;

namespace MyFinances.Models.Converters
{
    public static class Converter
    {
        public static OperationDto ToDto(this Operation model)
        {
            return new OperationDto()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Value = model.Value,
                Date = model.Date,
                CategoryId = model.CategoryId,
            };
        }

        public static IEnumerable<OperationDto> ToDtos(this IEnumerable<Operation> model)
        { 
            if(model==null)
                return Enumerable.Empty<OperationDto>();

            return model.Select(x => x.ToDto());
        }

        public static Operation ToDao(this OperationDto model)
        {
            return new Operation()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Value = model.Value,
                Date = model.Date,
                CategoryId = model.CategoryId,
            };
        }
    }
}
