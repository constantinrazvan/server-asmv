# Folosește o imagine de bază pentru rulare
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ServerAsmv.csproj", "./"]
RUN dotnet restore "ServerAsmv.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "ServerAsmv.csproj" -c Release -o /app/build

# Etapa de publicare
FROM build AS publish
RUN dotnet publish "ServerAsmv.csproj" -c Release -o /app/publish

# Etapa de actualizare a bazei de date
FROM publish AS ef-update
WORKDIR /app/publish
ENV ConnectionStrings__DefaultConnection="Host=postgres;Database=AsmvDatabase;Username=postgres;Password=postgres"
RUN dotnet ef database update

# Imaginea finală
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerAsmv.dll"]
