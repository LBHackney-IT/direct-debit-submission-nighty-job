using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
<<<<<<< Updated upstream:BaseListener/SqsFunction.cs
using BaseListener.Boundary;
using BaseListener.Gateway;
using BaseListener.Gateway.Interfaces;
using BaseListener.UseCase;
using BaseListener.UseCase.Interfaces;
=======
using DirectDebitApi.V1.UseCase.Interfaces;
using DirectDebitSubmissionNightyJob.Boundary;
using DirectDebitSubmissionNightyJob.Gateway;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using DirectDebitSubmissionNightyJob.UseCase;
using DirectDebitSubmissionNightyJob.UseCase.Interfaces;
>>>>>>> Stashed changes:DirectDebitSubmissionNightyJob/SqsFunction.cs
using Hackney.Core.DynamoDb;
using Hackney.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BaseListener
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
            services.ConfigureDynamoDB();

            services.AddHttpClient();
            services.AddScoped<IDoSomethingUseCase, DoSomethingUseCase>();

            services.AddScoped<IDbEntityGateway, DynamoDbEntityGateway>();

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
                IMessageProcessing processor = ServiceProvider.GetService<IAddDirectDebitSubmissionUseCase>();
                await processor.ProcessMessageAsync(Logger).ConfigureAwait(false);
            }
        }
    }
}