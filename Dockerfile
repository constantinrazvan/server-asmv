# Folosește o imagine de bază .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5235

# Folosește o imagine pentru construirea aplicației
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ServerAsmv.csproj", "."]
RUN dotnet restore "ServerAsmv.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ServerAsmv.csproj" -c Release -o /app/build

# Publică aplicația
FROM build AS publish
RUN dotnet publish "ServerAsmv.csproj" -c Release -o /app/publish

# Aici adăugăm etapa pentru a actualiza baza de date
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS ef-update
WORKDIR /app
COPY --from=publish /app/publish . 
RUN dotnet ef database update --project /src/ServerAsmv.csproj  # Asigură-te că specifici calea corectă

# Configurează imaginea finală
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerAsmv.dll"]  # Asigură-te că pornești aplicația
