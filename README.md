# U-Mandaditos Backend - Entity Framework Core
Aplicación Backend de Sistema U-Mandaditos

---

# Comandos iniciales

- Ejecutar solo una vez el siguiente comando para restaurar la configuracion

  dotnet tool restore

- Agregar un archivo .env basado en example.env en el proyecto API

- Crear las migraciones, en la consola de comandos ejecutar el siguiente comando
  
  dotnet ef migrations add InitialMigration --project Infrastructure --startup-project API
  
- Actualizar cambios en la base de datos
  
  dotnet ef database update --project Infrastructure --startup-project API


