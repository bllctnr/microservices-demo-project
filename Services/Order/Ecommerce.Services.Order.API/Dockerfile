#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Order/Ecommerce.Services.Order.API/Ecommerce.Services.Order.API.csproj", "Services/Order/Ecommerce.Services.Order.API/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Services/Order/Ecommerce.Services.Order.Application/Ecommerce.Services.Order.Application.csproj", "Services/Order/Ecommerce.Services.Order.Application/"]
COPY ["Services/Order/Ecommerce.Services.Order.Infrastructure/Ecommerce.Services.Order.Infrastructure.csproj", "Services/Order/Ecommerce.Services.Order.Infrastructure/"]
COPY ["Services/Order/Ecommerce.Services.Order.Domain/Ecommerce.Services.Order.Domain.csproj", "Services/Order/Ecommerce.Services.Order.Domain/"]
COPY ["Services/Order/Ecommerce.Services.Order.Domain.Core/Ecommerce.Services.Order.Domain.Core.csproj", "Services/Order/Ecommerce.Services.Order.Domain.Core/"]
RUN dotnet restore "Services/Order/Ecommerce.Services.Order.API/Ecommerce.Services.Order.API.csproj"
COPY . .
WORKDIR "/src/Services/Order/Ecommerce.Services.Order.API"
RUN dotnet build "Ecommerce.Services.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Services.Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Services.Order.API.dll"]