using EnumsNET;
using MundiPagg.AppService.Models;
using MundiPagg.AppService.OrderApplicationServices.ValueObjects;
using MundiPagg.AppService.OrderAppServices.Models.XML;
using MundiPagg.Domain.CreateOrders.Entities.NewOrders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace MundiPagg.AppService.Shared
{
    public class TranslateOrder
    {
        private static Dictionary<OrderContentFormatEnum, Func<string, NewOrder>> _formats;

        static TranslateOrder()
        {
            _formats = InstantiateFormatsDictonary();
        }

        internal static NewOrder FromRequest(string order, OrderContentFormatEnum orderContent, SumulateCardValueEnum eventSimulate)
        {
            var newOrder = _formats[orderContent](order);
            if (eventSimulate != SumulateCardValueEnum.Success)
                newOrder.Payments.FirstOrDefault().CreditCard.Card.Number = SumulateCardValueEnum.Fail.AsString(EnumFormat.Description);

            return newOrder;
        }

        #region [Private methods]
        private static NewOrder TranslateOrderFromJson(string order)
        {
            var orderModel = JsonSerializer.Deserialize<OrderModel>(order);
            
            return (NewOrder)orderModel;
        }

        private static NewOrder TranslateOrderFromXml(string order)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PedidoModelXml));

            PedidoModelXml pedido;
            using (Stream reader = new MemoryStream(Encoding.ASCII.GetBytes(order)))
            {
                pedido = (PedidoModelXml)serializer.Deserialize(reader);
            }

            return (NewOrder)pedido;
        }

        public static Dictionary<OrderContentFormatEnum, Func<string, NewOrder>> InstantiateFormatsDictonary()
        {
            return new Dictionary<OrderContentFormatEnum, Func<string, NewOrder>>()
            {
                { OrderContentFormatEnum.Json,  (x) => TranslateOrderFromJson(x)},
                { OrderContentFormatEnum.Xml,  (x) => TranslateOrderFromXml(x)}
            };
        }
        #endregion
    }
}
