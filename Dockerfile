#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat 
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019
WORKDIR /src
COPY *.sln .

# copy and restore all projects
COPY PaymentService.API/*.csproj PaymentService.API/
COPY PaymentService.Application/*.csproj PaymentService.Application/
COPY PaymentService.Domain/*.csproj PaymentService.Domain/
COPY PaymentService.Infrastructure/*.csproj PaymentService.Infrastructure/
COPY PPaymentService.Persistence/*.csproj PaymentService.Persistence/

# Copy everything else
COPY . .

ENTRYPOINT ["dotnet", "PaymentService.API.dll"]