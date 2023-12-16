using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MyFinances.Models;
using MyFinances.Models.Converters;
using MyFinances.Models.Domains;
using MyFinances.Models.Dtos;
using MyFinances.Models.Helpers;
using MyFinances.Models.Response;
using MyFinances.Models.Services;

namespace MyFinances.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UriService _uriService;

        public OperationController(UnitOfWork unitOfWork, UriService uriService)
        {
            _unitOfWork = unitOfWork;
            _uriService = uriService;
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
        public PagedResponse<IEnumerable<OperationDto>> Get([FromQuery] PaginationFilter paginationFilter)
        {
            var totalRecords = _unitOfWork.Operation.Count();
            var pagedData=_unitOfWork.Operation.Get(paginationFilter)?.ToDtos();
            var route = Request.Path.Value;
            var validFilter= new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            var response = new PagedResponse<IEnumerable<OperationDto>>();

            try
            {
                response = PaginationHelper.CreatePagedReponse<OperationDto>(pagedData, validFilter, totalRecords, _uriService, route);
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
