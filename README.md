# ChatPlatform

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
https://www.youtube.com/watch?v=neT7fmZ6sDE&t

.env in ./chat-platform-frontend

```
HTTPS=true
SSL_CRT_FILE=PATH_TO_CERT.PEM
SSL_KEY_FILE=PATH_TO_KEY.PEM
```
