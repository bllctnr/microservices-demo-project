FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app
EXPOSE 80
COPY Core/*.csproj Core/
COPY Services/IdentityServer/*.csproj Services/IdentityServer/ 
RUN dotnet restore  Services/IdentityServer/*.csproj
COPY . .
RUN dotnet publish Services/IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","IdentityServer.dll" ]