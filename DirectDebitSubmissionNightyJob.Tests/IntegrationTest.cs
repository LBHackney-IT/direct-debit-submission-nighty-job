using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DirectDebitSubmissionNightyJob.Infrastructure;
using Hackney.Core.Testing.Shared;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace DirectDebitSubmissionNightyJob.Tests
{
    public class IntegrationTest
    {
        private const string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMTIyNDE2MjU4ODQ1MjgxMDQxNDAiLCJlbWFpbCI6ImRlbmlzZS5udWRnZUBoYWNrbmV5Lmdvdi51ayIsImlzcyI6IkhhY2tuZXkiLCJuYW1lIjoiRGVuaXNlIE51ZGdlIiwiZ3JvdXBzIjpbInNvbWUtdmFsaWQtZ29vZ2xlLWdyb3VwIl0sImlhdCI6MTYzOTQxNzE4OX0.Rai_olTwhVugBY8L8bpyhSGxX3lLB-ZLqxlSDQh96nE";
        public HttpClient Client { get; private set; }
        public DirectDebitContext DatabaseContext { get; private set; }
        private readonly MockWebApplicationFactory _factory;
        private NpgsqlConnection _connection;
        private IDbContextTransaction _transaction;
        private DbContextOptionsBuilder _builder;

        private readonly IHost _host;

        public IntegrationTest()
        {
            _connection = new NpgsqlConnection(ConnectionString.TestDatabase());
            _connection.Open();
            var npgsqlCommand = _connection.CreateCommand();
            npgsqlCommand.CommandText = "SET deadlock_timeout TO 30";
            npgsqlCommand.ExecuteNonQuery();

            _builder = new DbContextOptionsBuilder();
            _builder.UseNpgsql(_connection);
            _factory = new MockWebApplicationFactory(_connection);

            DatabaseContext = new DirectDebitContext(_builder.Options);
            DatabaseContext.Database.EnsureCreated();
            _transaction = DatabaseContext.Database.BeginTransaction();
            _host = _factory.CreateHostBuilder(null).Build();
            _host.Start();


            LogCallAspectFixture.SetupLogCallAspect();
        }
        public void Dispose()
        {
            Dispose(true);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (null != _host)
                {
                    _host.StopAsync().GetAwaiter().GetResult();
                    _host.Dispose();
                }
                _disposed = true;
            }
        }

    }
}
