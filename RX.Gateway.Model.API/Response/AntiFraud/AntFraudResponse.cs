﻿namespace RX.Gateway.Model.API.Response.AntiFraud
{
    public abstract class AntFraudResponse : ResponseBase
    {
        public AntFraudResponse(object entity, EStatusResponse status, string mensage)
            : base(entity, status, mensage)
        {
        }
    }
}
