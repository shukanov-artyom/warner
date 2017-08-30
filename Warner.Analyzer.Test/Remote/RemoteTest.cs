using System;
using Warner.Api.Gateway;
using Warner.Domain;
using Xunit;

namespace Warner.Analyzer.Test.Remote
{
    public class RemoteTest
    {
        // private const string ServiceUrlHardcoded = "http://warner.azurewebsites.net";
        private const string ServiceUrlHardcoded = "http://localhost:53025";

        // private const string ServiceUrlHardcoded = "http://localhost:5000";
//        [Fact]
//        public void RetrieveProjectTest()
//        {
//            string serviceUrl = ServiceUrlHardcoded;
//            WebApiWarnerService service = new WebApiWarnerService(serviceUrl);
//            Project result = service.GetProject("TransVault");
//            Assert.NotNull(result);
//        }
    }
}
