#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /src
COPY *.sln .

# copy and restore all projects
COPY FacilityManagement.Services.Test/*.csproj FacilityManagement.Services.Test/
COPY FacilityManagement.Common.Utilities/*.csproj FacilityManagement.Common.Utilities/
