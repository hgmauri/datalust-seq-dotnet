#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Sample.Seq.WebApi/Sample.Seq.WebApi.csproj", "src/Sample.Seq.WebApi/"]
COPY ["src/Sample.Seq.WebApi.Core/Sample.Seq.WebApi.Core.csproj", "src/Sample.Seq.WebApi.Core/"]
RUN dotnet restore "src/Sample.Seq.WebApi/Sample.Seq.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Sample.Seq.WebApi"
RUN dotnet build "Sample.Seq.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Seq.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Seq.WebApi.dll"]