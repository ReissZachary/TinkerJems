FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["TinkerJems.Web2.csproj", "TinkerJems.Web2/"]
RUN dotnet restore "TinkerJems.Web2/TinkerJems.Web2.csproj"
COPY . "./TinkerJems.Web2"
WORKDIR "/src/TinkerJems.Web2"
RUN dotnet build "TinkerJems.Web2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TinkerJems.Web2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD dotnet TinkerJems.Web2.dll