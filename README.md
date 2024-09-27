# API Usuario CRUD 

Este proyecto es una API para la gestión de usuarios, que incluye autenticación mediante JWT y almacenamiento de contraseñas encriptadas. Está diseñada para ejecutarse completamente en Docker, incluyendo tanto la API como la base de datos MySQL.

#### Requisitos
- Docker
- Docker Compose

#### Instalación y Ejecución

##### 1. Clonar el repositorio



    git clone https://github.com/mjtrachta/UsuarioCrud092024.git
	cd UsuarioCrud092024

    
##### 2. Configurar Docker
El archivo `docker-compose.yml` ya está configurado para crear y conectar la API y la base de datos. También se ejecutará un script SQL de inicialización que crea la base de datos y su estructura.

##### 3. Levantar los servicios
Desde la raíz del proyecto, ejecuta:

    docker-compose up --build

Esto creará las imágenes de la API y de MySQL, y ejecutará ambos servicios. La base de datos MySQL tendrá una tabla llamada `usuarios` lista para su uso.

##### 4. Acceder a la API
Una vez que los contenedores estén en funcionamiento, puedes acceder a la API y ver la documentación de Swagger en:

    http://localhost:8080/swagger/index.html

#### Endpoints
La API incluye los siguientes endpoints para la gestión de usuarios y autenticación:

- **POST** `/api/auth/register`: Registrar un nuevo usuario.
- **POST** `/api/auth/login`: Iniciar sesión y obtener un token JWT.
- **GET** `/api/users`: Obtener todos los usuarios (requiere autenticación).
- **GET** `/api/users/{id}`: Obtener un usuario por ID.
- **PUT** `/api/users/{id}`: Actualizar un usuario existente (requiere autenticación).
- **DELETE** `/api/users/{id}`: Eliminar un usuario (requiere autenticación).

#### Notas
El archivo `init.sql` es ejecutado automáticamente al levantar el servicio MySQL para crear la estructura de la base de datos.
Asegúrate de tener Docker instalado y configurado correctamente en tu sistema antes de ejecutar los comandos.

#### Tecnologías Utilizadas
- .NET 8
- MySQL
- Docker
- JWT para autenticación
- BCrypt para encriptar contraseñas
