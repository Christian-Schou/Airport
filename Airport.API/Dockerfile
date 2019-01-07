FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Airport.API/Airport.API.csproj", "Airport.API/"]
RUN dotnet restore "Airport.API/Airport.API.csproj"
COPY . .
WORKDIR "/src/Airport.API"
RUN dotnet build "Airport.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Airport.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Airport.API.dll"]