FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["AllStars.API/AllStars.API.csproj", "AllStars.API/"]
RUN dotnet restore "AllStars.API/AllStars.API.csproj"

COPY . .

WORKDIR "/src/AllStars.API"
RUN dotnet build "AllStars.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AllStars.API.csproj" -c Release -o /app/publish

FROM base
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

ENTRYPOINT ["dotnet", "AllStars.API.dll"]
