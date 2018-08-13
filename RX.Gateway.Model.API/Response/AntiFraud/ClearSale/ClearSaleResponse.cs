namespace RX.Gateway.Model.API.Response.AntiFraud.ClearSale
{
    public abstract class ClearSaleResponse : AntFraudResponse
    {
        public ClearSaleResponse(object entity, EStatusResponse status, string mensage)
            : base(entity, status, mensage)
        {
        }
    }
}
