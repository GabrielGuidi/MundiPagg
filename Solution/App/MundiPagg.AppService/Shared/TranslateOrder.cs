using EnumsNET;
using MundiPagg.AppService.Models;
using MundiPagg.AppService.OrderApplicationServices.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MundiPagg.AppService.Shared
{
    public class TranslateOrder
    {
        private static Dictionary<OrderContentFormatEnum, Func<string, OrderModel>> _formats;

        static TranslateOrder()
        {
            _formats = InstantiateFormatsDictonary();
        }

        internal static OrderModel FromRequest(string order, OrderContentFormatEnum orderContent, SumulateCardValueEnum eventSimulate)
        {
            var orderModel = _formats[orderContent](order);
            if (eventSimulate != SumulateCardValueEnum.Success)
                orderModel.Pagamento.Cartao.NumeroCartao = SumulateCardValueEnum.Fail.AsString(EnumFormat.Description);

            return orderModel;
        }

        #region [Private methods]
        private static OrderModel TranslateOrderFromJson(string order)
        {
            return JsonSerializer.Deserialize<OrderModel>(order);
        }

        private static OrderModel TranslateOrderFromXml(string order)
        {
            throw new NotImplementedException("Not done yet!");
        }

        public static Dictionary<OrderContentFormatEnum, Func<string, OrderModel>> InstantiateFormatsDictonary()
        {
            return new Dictionary<OrderContentFormatEnum, Func<string, OrderModel>>()
            {
                { OrderContentFormatEnum.Json,  (x) => TranslateOrderFromJson(x)},
                { OrderContentFormatEnum.Xml,  (x) => TranslateOrderFromXml(x)}
            };
        }
        #endregion
    }
}
