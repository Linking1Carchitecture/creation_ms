#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1-focal AS base
WORKDIR /app
EXPOSE 2307
ENV ASPNETCORE_URLS=http://+:2307
ENV ASPNETCORE_ENVIRONMENT=Development


FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["creation_ms.csproj", "."]
RUN dotnet restore "creation_ms.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "creation_ms.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "creation_ms.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "creation_ms.dll"]
