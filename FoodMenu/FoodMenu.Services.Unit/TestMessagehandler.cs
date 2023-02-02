using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.Services.Unit
{
    public class TestMessagehandler : HttpMessageHandler
    {
        private Queue<HttpResponseMessage> _responseMessages = new();

        internal void SetResponse(HttpResponseMessage responseMessage)
        {
            _responseMessages.Enqueue(responseMessage);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_responseMessages.Dequeue());
        }
    }
}
