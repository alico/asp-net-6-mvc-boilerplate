using BoilerPlate.Utils;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace BoilerPlate.Bootstrapper
{
    public static class LogExtension
    {
        public static IServiceCollection AddLogging(this IServiceCollection services, ConnectionStrings connectionStrings)
        {
            try
            {
                var sinkOptions = new MSSqlServerSinkOptions();
                sinkOptions.AutoCreateSqlTable = false;
                sinkOptions.TableName = "ApplicationLogs";

                var columnOption = new ColumnOptions();
                columnOption.Store.Remove(StandardColumn.MessageTemplate);
                columnOption.Level.NonClusteredIndex = true;
                columnOption.AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn
                        {ColumnName = "RequestId", PropertyName = "RequestId", DataType = SqlDbType.NVarChar, DataLength = 64},
                };

                Log.Logger = new LoggerConfiguration()
                                .Enrich.WithCorrelationId()
                                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                                .MinimumLevel.Error()
                                .WriteTo.MSSqlServer(connectionStrings.MainConnection, sinkOptions, columnOptions: columnOption)
                                .CreateLogger();
            }
            catch (Exception ex)
            {
            }
            return services;
        }
    }
}
