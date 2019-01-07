FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Airport.Entities/Airport.Entities.csproj", "Airport.Entities/"]
RUN dotnet restore "Airport.Entities/Airport.Entities.csproj"
COPY . .
WORKDIR "/src/Airport.Entities"
RUN dotnet build "Airport.Entities.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Airport.Entities.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Airport.Entities.dll"]