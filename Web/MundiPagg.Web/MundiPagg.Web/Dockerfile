#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MundiPagg.Web/MundiPagg.Web.csproj", "MundiPagg.Web/"]
RUN dotnet restore "MundiPagg.Web/MundiPagg.Web.csproj"
COPY . .
WORKDIR "/src/MundiPagg.Web"
RUN dotnet build "MundiPagg.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MundiPagg.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MundiPagg.Web.dll"]