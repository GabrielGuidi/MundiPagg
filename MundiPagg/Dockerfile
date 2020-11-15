FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MundiPagg.Api.csproj", ""]
RUN dotnet restore "MundiPagg.Api.csproj"
COPY . ./MundiPagg
WORKDIR "/src/MundiPagg"
RUN dotnet build "MundiPagg.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MundiPagg.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MundiPagg.Api.dll"]