using ManDI.command.conception.add;
using ManDI.command.conception.del;
using ManDI.command.conception.sel;
using ManDI.command.conception.upd;
using ManDI.composite.entities.conception;
using ManDI.executor;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using XUnit.Test.config;

namespace XUnit.Test.UnitTest
{
    [CollectionDefinition("ManDiTestCollection", DisableParallelization = true)]
    [Order(1)]
    [TestCaseOrderer(
    ordererTypeName: "XUnit.Test.config.PriorityOrderer",
    ordererAssemblyName: "XUnit.Test")]
    public partial class ManDiUnitTest
    {
        private static ServiceProvider _serviceProvider;

        public ManDiUnitTest()
        {
            var serviceCollection = new ServiceCollection();
            new TestStartup().ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact, TestPriority(1)]
        public void ConceptionTest()
        {
            var executor = _serviceProvider.GetRequiredService<ICommandExecutor>();

            conception_add conception_add = new conception_add()
            
            {
                iname = "�������� ��������� �3",
                idesc = "�������� ��������� �3"
            };
            long ConceptionId = (long)executor.ExecuteScalar(conception_add);

            Assert.True(ConceptionId != 0, "ConceptionId �� ������ ���� ����� ����");

            conception_upd conception_upd = new conception_upd()
            {
                iid = ConceptionId,
                idefault = true,
                iname = "�������� ��������� �30",
                idesc = "�������� ��������� �30",
                ion = true
            };
            executor.ExecuteNonQuery(conception_upd);

            conception_by_id conception_by_id = new conception_by_id()
            {
                iid = ConceptionId
            };
            vconception conception = (vconception)executor.ExecuteScalar(conception_by_id);

            Assert.True(conception.name == "�������� ��������� �30", "�������� ��������� �� ��������");

            conception_by_all conception_by_all = new conception_by_all();
            DataTable table = executor.Fill(conception_by_all);

            List<vconception> ListConception = new List<vconception>();
            foreach (DataRow row in table.Rows)
            {
                ListConception.Add((vconception)row[0]);
            }

            Assert.True(ListConception.Count > 0, "���������� ��������� ������ ���� ������ 0");

            conception_del conception_del = new conception_del()
            {
                iid = ConceptionId
            };
            executor.ExecuteNonQuery(conception_del);
        }

        [Fact, TestPriority(2)]
        public void Test2()
        {
            
        }

        [Fact, TestPriority(3)]
        public void Test4()
        {

        }

        [Fact, TestPriority(5)]
        public void Test5()
        {

        }
    }
}