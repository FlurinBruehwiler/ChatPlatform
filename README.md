# ChatPlatform

## Dokumentation

[Projekt Planung](https://github.com/FlurinBruehwiler/ChatPlatform/blob/main/Dokumentation/ProjektPlanung.pdf)

## Development

appsettings.json in ./ChatPlatformBackend

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
  "CookieName" : "X-Access-Token"
}
```

appsettings.Development.json in ./ChatPlatformBackend

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionString": "DataSource=PATH_TO_DB_FILE",
  "JwtSecret": "32_CHARACTERS"
}
```

### HTTPS

.env in ./chat-platform-frontend
HTTPS=true
SSL_CRT_FILE=PATH_TO_CERT.PEM
SSL_KEY_FILE=PATH_TO_KEY.PEM
