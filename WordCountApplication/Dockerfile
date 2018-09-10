FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY WordCountApplication/WordCountApplication.csproj WordCountApplication/
RUN dotnet restore WordCountApplication/WordCountApplication.csproj
COPY . .
WORKDIR /src/WordCountApplication
RUN dotnet build WordCountApplication.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WordCountApplication.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WordCountApplication.dll"]
