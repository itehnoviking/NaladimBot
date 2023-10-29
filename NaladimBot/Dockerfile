#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NaladimBot/NaladimBot.csproj", "NaladimBot/"]
COPY ["NaladimBot.CQS/NaladimBot.CQS.csproj", "NaladimBot.CQS/"]
COPY ["NaladimBot.Core/NaladimBot.Core.csproj", "NaladimBot.Core/"]
COPY ["NaladkaBot.Data/NaladimBot.Data.csproj", "NaladkaBot.Data/"]
COPY ["NaladimBot.Domain/NaladimBot.Domain.csproj", "NaladimBot.Domain/"]
RUN dotnet restore "NaladimBot/NaladimBot.csproj"
COPY . .
WORKDIR "/src/NaladimBot"
RUN dotnet build "NaladimBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NaladimBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NaladimBot.dll"]