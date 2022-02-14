using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DirectDebitSubmissionNightyJob.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace DirectDebitSubmissionNightyJob.Tests
{
    public class AwsIntegrationTest<TStartup> : IDisposable where TStartup : class
    {
        private const string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMTIyNDE2MjU4ODQ1MjgxMDQxNDAiLCJlbWFpbCI6ImRlbmlzZS5udWRnZUBoYWNrbmV5Lmdvdi51ayIsImlzcyI6IkhhY2tuZXkiLCJuYW1lIjoiRGVuaXNlIE51ZGdlIiwiZ3JvdXBzIjpbInNvbWUtdmFsaWQtZ29vZ2xlLWdyb3VwIl0sImlhdCI6MTYzOTQxNzE4OX0.Rai_olTwhVugBY8L8bpyhSGxX3lLB-ZLqxlSDQh96nE";
        public HttpClient Client { get; private set; }
        public DirectDebitContext DatabaseContext { get; private set; }
        private MockWebApplicationFactory _factory;
        private NpgsqlConnection _connection;
        private DbTransaction _transaction;
        private DbContextOptionsBuilder _builder;
        public AwsIntegrationTest()
        {

            EnsureEnvVarConfigured("REQUIRED_GOOGL_GROUPS", "some-valid-google-group");
            EnsureEnvVarConfigured("PTXDDSubmissionBaseUrl", "http://baseurl.com/");
            EnsureEnvVarConfigured("RegisterPTXEmail", "test@test.com");
            EnsureEnvVarConfigured("PTXPassword", "password");
            EnsureEnvVarConfigured("PTXProfile", "44445");
            EnsureEnvVarConfigured("PTXVerifyApiKey", "hdhhhdhdhdyytteDDDWDDHHAGAG");
            EnsureEnvVarConfigured("PTXVerifyBaseUrl", "http://baseurl.com/");
            //EnsureEnvVarConfigured("TenureApiUrl", "http://baseurl.com/");
            //EnsureEnvVarConfigured("TenureApiToken", "123");
            //EnsureEnvVarConfigured("TransactionApiUrl", "http://baseurl.com/");
            //EnsureEnvVarConfigured("TransactionApiToken", "123");


            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkNpgsql()
                .BuildServiceProvider();
            _connection = new NpgsqlConnection(ConnectionString.TestDatabase());
            _connection.Open();
            var npgsqlCommand = _connection.CreateCommand();
            npgsqlCommand.CommandText = "SET deadlock_timeout TO 30";
            npgsqlCommand.ExecuteNonQuery();

            _builder = new DbContextOptionsBuilder();
            _builder.UseNpgsql(_connection).UseInternalServiceProvider(serviceProvider);



            _factory = new MockWebApplicationFactory(_connection);

            Client = _factory.OutgoingRestClient;
            Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Token);
            DatabaseContext = _factory.Server.Host.Services.GetRequiredService<DirectDebitContext>();
            _transaction = _connection.BeginTransaction(IsolationLevel.RepeatableRead);
            DatabaseContext.Database.UseTransaction(_transaction);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction.Dispose();
                }

                if (_factory != null)
                    _factory.Dispose();

                if (Client != null)
                    Client.Dispose();

                if (_connection != null)
                    _connection.Dispose();

            }

        }
        private static void EnsureEnvVarConfigured(string name, string defaultValue)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
                Environment.SetEnvironmentVariable(name, defaultValue);
        }
    }
}
