using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.Models;

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
    }
}
