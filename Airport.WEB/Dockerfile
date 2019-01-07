FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Airport/Airport.csproj", "Airport/"]
RUN dotnet restore "Airport/Airport.csproj"
COPY . .
WORKDIR "/src/Airport"
RUN dotnet build "Airport.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Airport.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Airport.dll"]