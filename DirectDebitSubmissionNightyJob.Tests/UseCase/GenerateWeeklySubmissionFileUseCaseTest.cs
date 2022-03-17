using AutoFixture;
using AutoMapper;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Boundary.Response;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Factories;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using DirectDebitSubmissionNightyJob.UseCase;
using DirectDebitSubmissionNightyJob.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DirectDebitSubmissionNightyJob.Tests.UseCase
{
    public class GenerateWeeklySubmissionFileUseCaseTest
    {
        private readonly Mock<IDirectDebitSubmissionGateway> _mockGateway;
        private readonly Mock<IPTXPaymentApiService> _iPTXFileUploadService;
        private readonly Mock<IDirectDebitGateway> _exportGateway;
        private readonly IMapper _mapper;
        private readonly GenerateSubmissionFileUseCase _classUnderTest;
        private readonly Fixture _fixture;
        private readonly Mock<ICreateTransactionRecordUseCase> _createTransactionRecordsUseCase;
        private readonly Mock<ILogger<GenerateSubmissionFileUseCase>> _logger;

        public GenerateWeeklySubmissionFileUseCaseTest()
        {
            _mockGateway = new Mock<IDirectDebitSubmissionGateway>();
            _iPTXFileUploadService = new Mock<IPTXPaymentApiService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = new Mapper(config);
            _exportGateway = new Mock<IDirectDebitGateway>();
            _createTransactionRecordsUseCase = new Mock<ICreateTransactionRecordUseCase>();
            _classUnderTest = new GenerateSubmissionFileUseCase(_mockGateway.Object, _iPTXFileUploadService.Object, _exportGateway.Object, _mapper, _createTransactionRecordsUseCase.Object);
            _fixture = new Fixture();
            _logger = new Mock<ILogger<GenerateSubmissionFileUseCase>>();
        }

        [Fact]
        public async Task ShouldAddDirectDebitSubmission()
        {
            // DirectDebit
            var accountDetails = _fixture.Build<AccountHolder>()
                                        .With(x => x.Name, "Brown Box")
                                        .CreateMany(1);

            var directDebit = _fixture.Build<DirectDebit>()
                                       .With(x => x.PreferredDate, DateTime.UtcNow.Day)
                                       .With(x => x.AccountNumber, "123456743")
                                       .With(x => x.BranchSortCode, "123456")
                                       .With(x => x.BankAccountNumber, "12345674")
                                       .With(x => x.Status, "New")
                                       .With(x => x.AccountHolders, accountDetails.ToList())
                                       .With(x => x.PaymentReference, "1923493402")
                                      .Create();

            var ptxData = SubmissionFileDataRequest();

            _exportGateway.Setup(_ => _.GetAllDirectDebitsByQueryAsync(It.IsAny<DirectDebitSubmissionRequest>())).ReturnsAsync(new List<DirectDebit>() { directDebit });
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<DirectDebit, PTXSubmissionFileData>(It.IsAny<DirectDebit>())).Returns(ptxData);
            var expectedReturnType = "123546";
            ; _iPTXFileUploadService.Setup(_ => _.SubmitDirectDebitFile(It.IsAny<byte[]>(), It.IsAny<string>())).ReturnsAsync(expectedReturnType);
            _iPTXFileUploadService.Setup(_ => _.GetResultSummaryByFileIdAsync(It.IsAny<string>())).ReturnsAsync(new ResultSummaryResponse { Status = "SUCCESS" });

            await _classUnderTest.ProcessMessageAsync(_logger.Object).ConfigureAwait(false);

            _mockGateway.Verify(_ => _.UploadFileAsync(It.IsAny<DirectDebitSubmission>()), Times.Once);
            _exportGateway.Verify(_ => _.GetAllDirectDebitsByQueryAsync(It.IsAny<DirectDebitSubmissionRequest>()), Times.Once);
        }

        [Fact]
        public void ProcessMessageShouldThrowException()
        {
            Func<Task> func = async () => await _classUnderTest.ProcessMessageAsync(null).ConfigureAwait(false);
            func.Should().ThrowAsync<ArgumentNullException>();
        }

        private PTXSubmissionFileData SubmissionFileDataRequest()
        {
            return new PTXSubmissionFileData
            {
                Amount = "00000002000",
                Name = "Brown Box",
                Number = "12345678",
                Sort = "123456",
                Ref = "1987446602",
                Type = "17"
            };
        }
    }
}
