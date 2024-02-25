using Xunit;
using Xunit.Abstractions;
using System.Reflection;
using System.Linq;

namespace XUnit.Test.config
{
    public class OrderCollectionOrderer : ITestCollectionOrderer
    {
        public IEnumerable<ITestCollection> OrderTestCollections(
            IEnumerable<ITestCollection> testCollections)
        {
            var sortedCollections = testCollections
                .Select(tc => new
                {
                    Collection = tc,
                    Order = GetOrder(tc.CollectionDefinition)
                })
                .OrderBy(tc => tc.Order)
                .Select(tc => tc.Collection);

            return sortedCollections;
        }

        private static int GetOrder(ITypeInfo collectionDefinition)
        {
            // Преобразование ITypeInfo в Type
            Type type = collectionDefinition.ToRuntimeType();
            var orderAttribute = type.GetCustomAttribute<OrderAttribute>();
            return orderAttribute?.Order ?? 0;
        }
    }
}