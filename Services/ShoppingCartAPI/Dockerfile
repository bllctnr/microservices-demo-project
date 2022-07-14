#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/ShoppingCartAPI/Ecommerce.Services.ShoppingCart.API.csproj", "Services/ShoppingCartAPI/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "Services/ShoppingCartAPI/Ecommerce.Services.ShoppingCart.API.csproj"
COPY . .
WORKDIR "/src/Services/ShoppingCartAPI"
RUN dotnet build "Ecommerce.Services.ShoppingCart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Services.ShoppingCart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Services.ShoppingCart.API.dll"]