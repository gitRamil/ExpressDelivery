FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["libs/Domain.Core.dll", "../libs/"]
COPY ["CourierService.Application/CourierService.Application.csproj", "CourierService.Application/"]
COPY ["CourierService.Domain/CourierService.Domain.csproj", "CourierService.Domain/"]
COPY ["CourierService.Infrastructure/CourierService.Infrastructure.csproj", "CourierService.Infrastructure/"]
COPY ["CourierService.WebApi/CourierService.WebApi.csproj", "CourierService.WebApi/"]
RUN dotnet restore "CourierService.WebApi/CourierService.WebApi.csproj"
COPY . .
WORKDIR "/src/CourierService.WebApi"
RUN dotnet build "CourierService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CourierService.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "CourierService.WebApi.dll"]









