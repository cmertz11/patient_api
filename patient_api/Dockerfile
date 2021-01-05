#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["patient_api/patient_api.csproj", "patient_api/"]
RUN dotnet restore "patient_api/patient_api.csproj"
COPY . .
WORKDIR "/src/patient_api"
RUN dotnet build "patient_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "patient_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "patient_api.dll"]