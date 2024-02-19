using Microsoft.Extensions.DependencyInjection;

namespace xUnitTestManDI
{
    public class UnitTestManDI
    {
        private static ServiceProvider _serviceProvider;

        public UnitTestManDI()
        {
            var serviceCollection = new ServiceCollection();
            new TestStartup().ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}