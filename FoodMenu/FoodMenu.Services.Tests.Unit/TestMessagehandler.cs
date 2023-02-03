namespace FoodMenu.Services.Unit
{
    public class TestMessagehandler : HttpMessageHandler
    {
        private HttpResponseMessage _responseMessage;

        internal void SetResponse(HttpResponseMessage responseMessage)
        {
            _responseMessage = responseMessage;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_responseMessage);
        }
    }
}