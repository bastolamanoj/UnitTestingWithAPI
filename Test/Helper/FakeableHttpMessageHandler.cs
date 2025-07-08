using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helper
{
    public abstract class FakeableHttpMessageHandler : HttpMessageHandler
    {
        public abstract Task<HttpResponseMessage> FakeSendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
        protected sealed override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => this.FakeSendAsync(request, cancellationToken);
    }
}
