using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnit.Test.config
{
    public class OrderAttribute : Attribute
    {
        public int Order { get; private set; }

        public OrderAttribute(int order) => Order = order;
    }

}