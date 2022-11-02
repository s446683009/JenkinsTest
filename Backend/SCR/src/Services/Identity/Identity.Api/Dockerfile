FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Identity/Identity.Api/Identity.Api.csproj", "Identity.Api/"]
COPY ["src/Services/Identity/Identity.Application/Identity.Application.csproj", "Identity.Application/"]
COPY ["src/Infrastructure/SCR.Common/SCR.Common.csproj", "SCR.Common/"]
COPY ["src/Services/Identity/Identity.Domain/Identity.Domain.csproj", "Identity.Domain/"]
COPY ["src/Infrastructure/EventBus/EventBus/EventBus.csproj", "EventBus/"]
COPY ["src/Services/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "Identity.Infrastructure/"]
RUN dotnet restore "src/Services/Identity/Identity.Api/Identity.Api.csproj"
COPY . .
WORKDIR "/src/Identity.Api"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]
