FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Hbsis.Library.Api/Hbsis.Library.Api.csproj", "Hbsis.Library.Api/"]
COPY ["Hbsis.Library.Application/Hbsis.Library.Application.csproj", "Hbsis.Library.Application/"]
COPY ["Hbsis.Library.Business/Hbsis.Library.Business.csproj", "Hbsis.Library.Business/"]
COPY ["Hbsis.Library.Data/Hbsis.Library.Data.csproj", "Hbsis.Library.Data/"]
COPY ["Hbsis.Library.CrossCutting/Hbsis.Library.CrossCutting.csproj", "Hbsis.Library.CrossCutting/"]
COPY ["Hbsis.Library.Domain/Hbsis.Library.Domain.csproj", "Hbsis.Library.Domain/"]
COPY ["Hbsis.Library.Ioc/Hbsis.Library.Ioc.csproj", "Hbsis.Library.Ioc/"]
RUN dotnet restore "Hbsis.Library.Api/Hbsis.Library.Api.csproj"
COPY . .
WORKDIR "/src/Hbsis.Library.Api"
RUN dotnet build "Hbsis.Library.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hbsis.Library.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hbsis.Library.Api.dll"]
