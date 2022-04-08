#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["sp2000.API/sp2000.API.csproj", "sp2000.API/"]
COPY ["sp2000.Infrastructure/sp2000.Infrastructure.csproj", "sp2000.Infrastructure/"]
COPY ["sp2000.Application/sp2000.Application.csproj", "sp2000.Application/"]
RUN dotnet restore "sp2000.API/sp2000.API.csproj"
COPY . .
WORKDIR "/src/sp2000.API"
RUN dotnet build "sp2000.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sp2000.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sp2000.API.dll"]
