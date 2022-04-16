# BookStoreWeb
## Caracteristicas
  1. Para este proyecto se usó con una herramienta llamada [Telerik UI for ASP.NET MVC](https://demos.telerik.com/aspnet-mvc/)
  2. Se consume la API desarrollada en el [Proyecto 1](https://github.com/santiago-hear/BookStore.git)
  3. Se implementó el patrón repositorio y arquitectura MVC con las tecnlogías de .NET MVC, Razor, Bootstrap y Kendo

## Uso
  1. Se debe configurar el servicio de la api en el campo "ApiService" del archivo appsettings.json
```json
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "ApiService": "https://localhost:44306/api"
}
```

## Pruebas funcionales
Las pruebas funcionales se muestran en el siguiente [video](https://drive.google.com/file/d/1hNrwEksbYaVFg6rImQCQgpDNsDS4cRQn/view?usp=sharing)
