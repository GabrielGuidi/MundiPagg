using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MundiPagg.AppService.DataTransfer;
using MundiPagg.AppService.Models;
using MundiPagg.AppService.OrderApplicationServices.Interfaces;
using MundiPagg.AppService.OrderApplicationServices.ValueObjects;
using System.IO;
using System.Text.Json;

namespace MundiPagg.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderAppService orderAppService, ILogger<OrderController> logger)
        {
            _orderAppService = orderAppService;
            _logger = logger;
        }

        /// <summary>
        /// Get order by code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Order</returns>
        [HttpGet("code/{code}")]
        public ActionResult<OrderResponse> GetOrder(string code)
        {
            var order = _orderAppService.GetOrder(code);
            if (order == null)
                return new NotFoundResult();

            return new ObjectResult(order);
        }

        /// <summary>
        /// Get order by code.
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns>Order</returns>
        [HttpGet("{jobId}", Name = "Get")]
        public ActionResult<OrderResponse> GetOrderByJobId(long jobId)
        {
            var order = _orderAppService.GetOrder(jobId);
            if (order == null)
                return new NotFoundResult();

            return new ObjectResult(order);
        }

        /// <summary>
        /// Create a new order based on Json.
        /// </summary>
        /// <param name="orderRequest">For OrderContent: 1 - Json (Default), 2 - Xml.
        /// For EventSimulate: 1 - Success (Default), 2 - Fail, 3 - ProcessingThanSuccess, 4 - ProcessingThanFail, 
        /// 5 - ErrorInSecondOperation, 6 - SuccessThanPurchaserFailure, 7 - ProcessingThanCanceled, 8 - Other.</param>
        /// <returns>Created order.</returns>
        [HttpPost]
        public ActionResult<OrderResponse> CreateNewOrder([FromBody] OrderRequest orderRequest)
        {
            if (string.IsNullOrWhiteSpace(orderRequest.Order))
            {
                orderRequest.Order = GetDefaultOrderJson(orderRequest.OrderContent);
            }
            else
            {
                try
                {
                    JsonSerializer.Deserialize<OrderModel>(orderRequest.Order);
                }
                catch (System.Exception)
                {
                    _logger.LogInformation($"Fail to deserialize!");
                    orderRequest.Order = GetDefaultOrderJson(orderRequest.OrderContent);
                }
            }

            var orderResponse = _orderAppService.CreateNewOrder(orderRequest);

            return CreatedAtRoute("Get", new { jobId = orderResponse.JobId }, orderResponse);
        }

        #region [Private methods]
        private string GetDefaultOrderJson(OrderContentFormatEnum orderContent)
        {
            string patchDefault = ".//Assets//RequestJson.json";
            const string patchXML = ".//Assets//RequestXML.xml";

            if (orderContent == OrderContentFormatEnum.Xml)
                patchDefault = patchXML;

            using StreamReader fileReader = new StreamReader(patchDefault);
            _logger.LogInformation($"Using default Order json.");
            return fileReader.ReadToEnd();
        }
        #endregion
    }
}
