﻿namespace MundiPagg.Infra.Shared.Interfaces
{
    public interface IMessageBroker
    {
        void SendMessage(string message, string exchange, string routingKey, string exchangeResponse = null, string id = null);
    }
}
