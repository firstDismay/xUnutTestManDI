using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUnit.Test.config;
using ExcelDataReader;
using ManDI.executor;
using Microsoft.Extensions.DependencyInjection;
using ManDI.command.properties.vprop_enum.val;

namespace XUnit.Test.UnitTest
{
    public partial class ManDiUnitTest
    {
        [Fact, TestPriority(10)]
        public void UploadExcel()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, @"upload\Data.xlsx");
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    int sort = 1;
                    while (reader.Read()) // Читаем каждую строку
                    {
                        var executor = _serviceProvider.GetRequiredService<ICommandExecutor>();
                        prop_enum_val_add prop_enum_val_add = new prop_enum_val_add()

                        {
                            iid_prop_enum = 98,
                            ival_varchar = reader.GetString(0),
                            isort = sort
                        };
                        //executor.ExecuteScalar(prop_enum_val_add);
                        sort++;
                    }
                }
            }

        }
    }
}
