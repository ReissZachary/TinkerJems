FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TinkerJems.csproj", "TinkerJems/"]
RUN dotnet restore "TinkerJems/TinkerJems.csproj"
COPY . "./TinkerJems"
WORKDIR "/src/TinkerJems"
RUN dotnet build "TinkerJems.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TinkerJems.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD dotnet TinkerJems.dll