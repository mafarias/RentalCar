Outlet Rental Cars - Sistema de Búsqueda de Vehículos
Este proyecto es una solución técnica robusta para el sistema de alquiler de vehículos de Outlet Rental Cars. Implementa una arquitectura híbrida utilizando MySQL para datos transaccionales y MongoDB para configuraciones dinámicas de mercado.

Arquitectura y Decisiones Técnicas
Se ha implementado Clean Architecture para garantizar el desacoplamiento y la mantenibilidad del código. La solución se divide en:

Domain: Contiene las entidades, excepciones y eventos de dominio (VehicleReservedEvent).

Application: Maneja la lógica de negocio a través de MediatR. Implementa el patrón CQRS separando las consultas (Queries) de las acciones (Commands).

Infrastructure: Implementa el acceso a datos mediante Entity Framework Core para MySQL y el driver oficial para MongoDB.

API: Capa de presentación que expone los endpoints REST.

Por qué esta tecnología:
MySQL (Relacional): Elegido para Vehículos y Reservas debido a la necesidad de integridad referencial y manejo de transacciones complejas al validar cruces de fechas.

MongoDB (NoSQL): Utilizado para el catálogo de Mercados. Esto permite una escalabilidad horizontal y cambios rápidos en la configuración de países habilitados sin afectar el esquema transaccional.

Requerimientos Funcionales Implementados
Disponibilidad por Localidad: Validación de que el vehículo pertenece a la sede de recogida.

Disponibilidad por Mercado: Integración con MongoDB para verificar si un país está habilitado antes de mostrar resultados.

Cruce de Fechas: Lógica avanzada en el Repositorio que excluye vehículos con reservas activas en el rango solicitado.

Estado del Vehículo: Filtro automático para retornar solo vehículos con estado Disponible = true.

Cómo Ejecutar el Proyecto
Pre-requisitos:
.NET 7.0 o superior.

Instancia de MySQL (puedes usar Workbench).

Instancia de MongoDB (puedes usar Compass o Docker).

Configuración:
Clonar el repositorio.

Configurar las cadenas de conexión en appsettings.json para MySQL y MongoDB.

Ejecutar Migraciones:

Bash
dotnet ef database update --project ./RentalCar.Infrastructure --startup-project ./RentalCar
Data Seed:

En MySQL: Insertar localidades (Colombia, México, etc.) y vehículos.

En MongoDB: Crear la colección Mercados y añadir documentos (ej: { "Pais": "Colombia", "EstaHabilitado": true }).

Testing
La solución incluye:

Pruebas Unitarias (XUnit): Validación de la lógica de negocio para el cruce de fechas y estados de vehículos.

Pruebas de Integración: Verificación de los endpoints de la API y la comunicación entre capas.

Patrones Aplicados
SOLID & DRY: Código limpio y responsabilidades separadas.

CQRS: Separación de lectura y escritura.

Domain Events: Uso de VehicleReservedEvent para disparar acciones laterales tras una reserva.
