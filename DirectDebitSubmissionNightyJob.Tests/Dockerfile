FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'
ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./DirectDebitSubmissionNightyJob.sln ./
COPY ./DirectDebitSubmissionNightyJob/DirectDebitSubmissionNightyJob.csproj ./DirectDebitSubmissionNightyJob/
COPY ./DirectDebitSubmissionNightyJob.Tests/DirectDebitSubmissionNightyJob.Tests.csproj ./DirectDebitSubmissionNightyJob.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./DirectDebitSubmissionNightyJob/DirectDebitSubmissionNightyJob.csproj
RUN dotnet restore ./DirectDebitSubmissionNightyJob.Tests/DirectDebitSubmissionNightyJob.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out DirectDebitSubmissionNightyJob.Tests/DirectDebitSubmissionNightyJob.Tests.csproj

CMD dotnet test
