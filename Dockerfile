FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dev
WORKDIR /app

COPY . ./

RUN dotnet restore

RUN dotnet build

CMD [ "dotnet", "watch", "run", "--project", "CleanArchMvc.WebUI/CleanArchMvc.WebUI.csproj" ]
