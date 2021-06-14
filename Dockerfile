FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR "../Kwetter.UserGateway"

COPY ["Kwetter.UserGateway/Kwetter.UserGateway.csproj", "Kwetter.UserGateway/"]
#COPY ["Kwetter.Business/Kwetter.Business.csproj", "Kwetter.Business/"]

RUN dotnet restore "Kwetter.UserGateway/Kwetter.UserGateway.csproj"
#RUN dotnet restore "Kwetter.Business/Kwetter.UserGateway.csproj"
COPY . .
WORKDIR "Kwetter.UserGateway"
RUN dotnet build "Kwetter.UserGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kwetter.UserGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.UserGateway.dll"]
