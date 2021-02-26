using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLab.ApiClient.Test;
using MyLab.Log.Ctx;
using MyLab.Log.Ctx.Trace;
using TestServer;
using Xunit;
using Xunit.Abstractions;

namespace FunctionTest
{
    public class TraceIdLogCtxBehavior : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly TestApi<Startup, ITestService> _api;

        public TraceIdLogCtxBehavior(ITestOutputHelper output)
        {
            _output = output;
            _api = new MyLab.ApiClient.Test.TestApi<Startup, ITestService>
            {
                Output = output
            };
        }

        [Fact]
        public async Task ShouldLogHttpContext()
        {
            //Arrange
            var logStringBuilder = new StringBuilder();
            var srv = _api.StartWithProxy(services => 
                services.AddLogging(b => b
                            .AddXUnit(_output)
                            .AddProvider(new StringBuilderLoggerProvider(logStringBuilder))
                    )
                    .AddLogCtx(r => r.Register<HttpTraceIdLogCtx>())
                );

            //Act
            await srv.Echo("foo");
            var logStr = logStringBuilder.ToString();

            //Assert
            Assert.Contains("Message: Echo", logStr);
            Assert.Contains(TraceLogContextExtensionConstants.TraceIdLabelName + ":", logStr);
        }

        public void Dispose()
        {
            _api?.Dispose();
        }
    }
}
