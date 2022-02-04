using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using DirectDebitSubmissionNightyJob.UseCase.Interfaces;
using Hackney.Core.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Extension;
using DirectDebitSubmissionNightyJob.Infrastructure;
using DirectDebitSubmissionNightyJob.Services.Concrete;
using DirectDebitSubmissionNightyJob.Services.Interfaces;
using Hackney.Core.DynamoDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DirectDebitSubmissionNightyJob.UseCase;
using DirectDebitSubmissionNightyJob.Gateways;
using DirectDebitSubmissionNightyJob.Gateways.Interfaces;
using AutoMapper;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DirectDebitSubmissionNightyJob
{
    /// <summary>
    /// Lambda function triggered by an SQS message
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SqsFunction : BaseFunction
    {
        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public SqsFunction()
        { }

        /// <summary>
        /// Use this method to perform any DI configuration required
        /// </summary>
        /// <param name="services"></param>
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGenerateWeeklySubmissionFileUseCase, GenerateWeeklySubmissionFileUseCase>();
            services.AddScoped<IDirectDebitSubmissionGateway, DirectDebitSubmissionGateway>();
            services.AddScoped<IDirectDebitGateway, DirectDebitGateway>();
            services.AddScoped<IPTXPaymentApiService, PTXPaymentApiService>();
            services.AddScoped<ICreateTransactionRecordUseCase, CreateTransactionRecordUseCase>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddLazyCache();

            services.ConfigureDynamoDB();

            services.AddHttpClient();
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            services.AddDbContext<DirectDebitContext>(opt =>
                opt.UseNpgsql(connectionString));

            services.AddTransient<IRestClient, JsonRestClient>();
            services.ConfigureTenureApiClient(Configuration);
            services.ConfigureTransactionApiClient(Configuration);

            base.ConfigureServices(services);
        }

        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            await ProcessMessageAsync(context).ConfigureAwait(false);
        }

        /// <summary>
        /// Method called to process every distinct message received.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [LogCall(LogLevel.Information)]
        private async Task ProcessMessageAsync(ILambdaContext context)
        {
            using (Logger.BeginScope("Start processing event for " + DateTime.UtcNow))
            {
                IMessageProcessing processor = ServiceProvider.GetService<IGenerateWeeklySubmissionFileUseCase>();
                await processor.ProcessMessageAsync(Logger).ConfigureAwait(false);
            }
        }
    }
}
