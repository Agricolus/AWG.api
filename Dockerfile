FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /AWGAPI/src

# Copy csproj and restore as distinct layers
COPY AWG.api ./AWG.api
COPY AWG.Common ./AWG.Common
COPY DataModels ./DataModels
COPY Modules ./Modules
WORKDIR /AWGAPI/src/AWG.api
RUN dotnet restore

# Copy everything else and build
WORKDIR /AWGAPI/src/AWG.api
RUN dotnet publish -o /AWGAPI/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /AWGAPI/out
COPY --from=build /AWGAPI/out .
ENTRYPOINT ["dotnet", "AWG.api.dll"]