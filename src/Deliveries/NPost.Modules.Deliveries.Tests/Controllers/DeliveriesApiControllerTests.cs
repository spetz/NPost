using NPost.Modules.Deliveries.Controllers;
using NPost.Shared;
using NSubstitute;
using Shouldly;
using Xunit;

namespace NPost.Modules.Deliveries.Tests.Controllers
{
    public class DeliveriesApiControllerTests
    {
        private readonly IDispatcher _dispatcher;
        private readonly DeliveriesApiController _controller;
        
        public DeliveriesApiControllerTests()
        {
            _dispatcher = Substitute.For<IDispatcher>();
            _controller = new DeliveriesApiController(_dispatcher);
        }

        [Fact]
        public void meta_should_return_message()
        {
            var response = _controller.Meta();
            
            response.ShouldNotBeNull();
            response.Value.ShouldBe("Deliveries module");
        }
    }
}