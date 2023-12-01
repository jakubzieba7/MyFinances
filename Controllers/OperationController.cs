using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.Models;
using MyFinances.Models.Converters;
using MyFinances.Models.Domains;
using MyFinances.Models.Dtos;
using MyFinances.Models.Response;

namespace MyFinances.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public OperationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Get operation by Id
        /// </summary>
        /// <param name="id">Operation Id</param>
        /// <returns>DataResponse - OperationDto</returns>
        [HttpGet("{id}")]
        public DataResponse<OperationDto> Get(int id)
        {
            var response = new DataResponse<OperationDto>();

            try
            {
                response.Data = _unitOfWork.Operation.Get(id)?.ToDto();
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        /// <summary>
        /// Get operations by Page number and Page size implemented in PaginationFilter
        /// </summary>
        /// <param name="paginationFilter">Page size and Page number</param>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<IEnumerable<OperationDto>> Get([FromQuery] PaginationFilter paginationFilter)
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                response.Data = _unitOfWork.Operation.Get(paginationFilter)?.ToDtos();
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }


        /// <summary>
        /// Add operation
        /// </summary>
        /// <param name="operationDto">OperationDto object</param>
        /// <returns>DataResponse - int</returns>
        [HttpPost]
        public DataResponse<int> Add(OperationDto operationDto)
        {
            var response = new DataResponse<int>();

            try
            {
                var operation = operationDto.ToDao();
                _unitOfWork.Operation.Add(operation);
                _unitOfWork.Complete();
                response.Data = operation.Id;
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }


        /// <summary>
        /// Update operation
        /// </summary>
        /// <param name="operation">OperationDto object</param>
        /// <returns>Response</returns>
        [HttpPut]
        public Response Update(OperationDto operation)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.Update(operation.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }


        /// <summary>
        /// Delete operation by Id
        /// </summary>
        /// <param name="id">Operation Id</param>
        /// <returns>Response</returns>
        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.Delete(id);
                _unitOfWork.Complete();
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }
    }
}
