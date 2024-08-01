using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

namespace OnlineBookShop.Infrastructure.Persistance.Extensions
{
    public static class MigrationBuilderExtensions
    {
        public static OperationBuilder<SqlOperation> CreateViewFromFile(this MigrationBuilder migrationBuilder, string viewFileName)
        {
            var executingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            var executingAssemblyOutputDirectory = Path.GetDirectoryName(executingAssemblyLocation);
            var sqlFile = executingAssemblyOutputDirectory + $@"\Persistance\Scripts\Views\{viewFileName}";
            var spText = File.ReadAllText(sqlFile);
            //we wrap each creation of view in EXECUTE statement because we generate scripts with --idempotent parameter in build pipeline
            var fullText = "EXECUTE ('" + spText.Replace("'", "''") + "')";

            return migrationBuilder.Sql(fullText);
        }
    }
}
