using Microsoft.AspNetCore.Mvc;
using MundiPagg.AppService.DataTransfer;
using MundiPagg.AppService.OrderApplicationServices.Interfaces;
using System.IO;

namespace MundiPagg.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        /// <summary>
        /// Create new order.
        /// </summary>
        /// <returns>Create a new order based on Json.</returns>
        [HttpPost]
        [Route("json/create")]
        public IActionResult CreateNewOrder([FromBody] OrderRequest orderRequest)
        {
            if (string.IsNullOrWhiteSpace(orderRequest.Order))
            {
                using StreamReader fileReader = new StreamReader(".//Assets//RequestJson.json");
                orderRequest.Order = fileReader.ReadToEnd();
            }

            var orderResponse = _orderAppService.CreateNewOrder(orderRequest);

            return Ok(orderResponse);
            //return CreatedAtAction("RecuperarComandasPorCodigo", new { codigo = comandaResponse.CodigoComanda }, comandaResponse);
        }
    }
}
