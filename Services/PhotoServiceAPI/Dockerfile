#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/PhotoServiceAPI/Ecommerce.Services.PhotoService.API.csproj", "Services/PhotoServiceAPI/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "Services/PhotoServiceAPI/Ecommerce.Services.PhotoService.API.csproj"
COPY . .
WORKDIR "/src/Services/PhotoServiceAPI"
RUN dotnet build "Ecommerce.Services.PhotoService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Services.PhotoService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Services.PhotoService.API.dll"]