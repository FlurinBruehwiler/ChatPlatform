# ChatPlatform

## Dokumentation
[Projekt Planung](https://github.com/FlurinBruehwiler/ChatPlatform/blob/main/Dokumentation/ProjektPlanung.pdf)

## Development
appsettings.json in ./ChatPlatformBackend
````
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
````
appsettings.Development.json in ./ChatPlatformBackend
````
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionString": "",
  "JwtSecret": ""
}
````
