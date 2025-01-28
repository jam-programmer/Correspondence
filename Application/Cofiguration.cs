using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Service;

namespace Application;
public static class Cofiguration
{
    public static IServiceCollection Application
           (this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMediatR(cfg =>
          cfg.RegisterServicesFromAssembly(typeof(FrameworkReference).Assembly));


        #region AddSerilog
        var columnOption = new ColumnOptions();
        columnOption.Store.Remove(StandardColumn.Properties);
        columnOption.Store.Remove(StandardColumn.MessageTemplate);
        columnOption.AdditionalColumns = new Collection<SqlColumn>()
            {
                new SqlColumn()
                {
                    AllowNull = true,
                    DataType=System.Data.SqlDbType.NVarChar,
                    DataLength=900,
                    ColumnName="Source",
                    PropertyName="SourceContext"
                },
                new SqlColumn()
                {
                    AllowNull = true,
                    DataType=System.Data.SqlDbType.NVarChar,
                    DataLength=900,
                    ColumnName="RequestPath",
                    PropertyName="RequestPath"
                }
            };

        Log.Logger = new LoggerConfiguration()
          .WriteTo.MSSqlServer(
          connectionString: configuration.
          GetConnectionString("SqlServerConnectionString")
          , sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
          {
              TableName = "LogSystem",
              AutoCreateSqlTable = true
          }, columnOptions: columnOption
              ).MinimumLevel.Warning().CreateLogger();


        service.AddSerilog();
        #endregion

        service.AddScoped<IJanService, JanService>();
 

        return service;

    }
}