FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MundiPagg.csproj", ""]
RUN dotnet restore "MundiPagg.csproj"
COPY . ./MundiPagg
WORKDIR "/src/MundiPagg"
RUN dotnet build "MundiPagg.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MundiPagg.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MundiPagg.dll"]