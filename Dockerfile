FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["TinkerJems.Web2/TinkerJems.Web2.csproj", "TinkerJems.Web2/"]
COPY ["TinkerJems.Core/TinkerJems.Core.csproj", "TinkerJems.Core/"]
RUN dotnet restore "TinkerJems.Web2/TinkerJems.Web2.csproj"
COPY . .
WORKDIR "/src/TinkerJems.Web2"
RUN dotnet build "TinkerJems.Web2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinkerJems.Web2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet "TinkerJems.Web2.dll"