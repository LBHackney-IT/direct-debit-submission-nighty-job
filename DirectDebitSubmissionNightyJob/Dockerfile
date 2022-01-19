FROM mcr.microsoft.com/dotnet/core/sdk:3.1

ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN
WORKDIR /app

# Copy csproj and NUGet.Config and restore as distinct layers
COPY ./DirectDebitSubmissionNightyJob/DirectDebitSubmissionNightyJob.csproj ./
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config
RUN dotnet restore ./DirectDebitSubmissionNightyJob.csproj


# Copy everything else and build
COPY ./DirectDebitSubmissionNightyJob ./
RUN dotnet build -c Release -o out ./DirectDebitSubmissionNightyJob.csproj
RUN dotnet publish -c Release -o out ./DirectDebitSubmissionNightyJob.csproj

EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/DirectDebitSubmissionNightyJob.dll
