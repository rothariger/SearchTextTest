FROM mcr.microsoft.com/powershell AS downloadnodejs
ENV NODE_VERSION=14.16.0
SHELL ["pwsh", "-Command", "$ErrorActionPreference = 'Stop';$ProgressPreference='silentlyContinue';"]
RUN Invoke-WebRequest -OutFile nodejs.zip -UseBasicParsing "https://nodejs.org/dist/v$($env:NODE_VERSION)/node-v$($env:NODE_VERSION)-win-x64.zip"; `
    Expand-Archive nodejs.zip -DestinationPath C:\; `
    Rename-Item "C:\node-v$($env:NODE_VERSION)-win-x64" c:\nodejs

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=downloadnodejs C:\nodejs\ C:\Windows\system32\

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SearchTextTest/SearchTextTest.csproj", "SearchTextTest/"]
RUN dotnet restore "SearchTextTest/SearchTextTest.csproj"
COPY . .
WORKDIR "/src/SearchTextTest"
RUN dotnet build "SearchTextTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SearchTextTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchTextTest.dll"]

