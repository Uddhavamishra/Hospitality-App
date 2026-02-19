# -------- Base runtime image --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# -------- Build image --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution + project folder
COPY "datagami-devops.sln" .
COPY GrandHotel.API/ GrandHotel.API/

# Restore dependencies
RUN dotnet restore "GrandHotel.API/GrandHotel.API.csproj"

# Publish
RUN dotnet publish "GrandHotel.API/GrandHotel.API.csproj" \
    -c Release -o /app/publish /p:UseAppHost=false

# -------- Final image --------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GrandHotel.API.dll"]