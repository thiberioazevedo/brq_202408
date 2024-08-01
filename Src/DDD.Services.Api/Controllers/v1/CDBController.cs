using DDD.Application.Interfaces;
using DDD.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Services.Api.Controllers.v1
{
    //[Authorize]
    [ApiVersion("1.0")]
    public class CDBController : ApiController
    {
        private readonly ICDBAppService _CDBAppService;

        public CDBController(
            ICDBAppService CDBAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _CDBAppService = CDBAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Calcular")]
        public IActionResult Calcular([FromQuery] decimal valor, int meses)
        {
            return Response(_CDBAppService.CalcularCollection(valor, meses));
        }
    }
}
