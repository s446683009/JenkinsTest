FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Category/Category.Api/Category.Api.csproj", "src/Services/Category/Category.Api/"]
RUN dotnet restore "src/Services/Category/Category.Api/Category.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Category/Category.Api"
RUN dotnet build "Category.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Category.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Category.Api.dll"]
