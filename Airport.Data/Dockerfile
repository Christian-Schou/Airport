FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Airport.Data/Airport.Data.csproj", "Airport.Data/"]
RUN dotnet restore "Airport.Data/Airport.Data.csproj"
COPY . .
WORKDIR "/src/Airport.Data"
RUN dotnet build "Airport.Data.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Airport.Data.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Airport.Data.dll"]