namespace RX.Gateway.Model.API.Response.Acquirer
{
    public abstract class AcquirerResponse : ResponseBase
    {
        public AcquirerResponse(object entity, EStatusResponse status, string mensage)
            : base(entity, status, mensage)
        {
        }
    }
}
