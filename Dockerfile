FROM mcr.microsoft.com/dotnet/sdk:8.0 as sdk
WORKDIR /src
COPY . /src
RUN dotnet publish -c Release -o api src/Presentation

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=sdk src/api /app
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "HealthMed.Presentation.dll"]