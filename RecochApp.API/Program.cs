using RecochApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


/// Crea una instancia del constructor de la aplicación web, que se utiliza para configurar los servicios y el middleware de la aplicación. El constructor toma los argumentos de la línea de comandos, lo que permite personalizar el comportamiento de la aplicación en función de las opciones proporcionadas al iniciar la aplicación.
var builder = WebApplication.CreateBuilder(args);

/// CONFIGURACIÓN DE SERVICIOS
// 🔗 DB
/// Configuración del contexto de la base de datos utilizando SQL Server, obteniendo la cadena de conexión desde la configuración de la aplicación. Esto permite que la aplicación se conecte a la base de datos y realice operaciones de lectura y escritura utilizando Entity Framework Core, lo que facilita el acceso a los datos y la gestión de las entidades en la aplicación.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/// CORS (ACTIVO)
// 🔐 JWT (ACTIVO)
/// Convierte la clave secreta de JWT almacenada en la configuración en un arreglo de bytes utilizando UTF-8, lo que es necesario para crear una instancia de SymmetricSecurityKey que se utilizará para validar la firma de los tokens JWT. Esta clave es fundamental para garantizar la integridad y autenticidad de los tokens emitidos por la API, asegurando que solo los tokens firmados con esta clave sean considerados válidos durante el proceso de autenticación.
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

/// Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    /// Establece el esquema de autenticación predeterminado para la aplicación, indicando que se utilizará JWT Bearer para autenticar las solicitudes entrantes. Esto asegura que todas las solicitudes que requieran autenticación serán procesadas utilizando tokens JWT, lo que proporciona una capa de seguridad basada en tokens para proteger los endpoints de la API.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    /// Establece el esquema de desafío predeterminado para la aplicación, lo que significa que si una solicitud no proporciona credenciales válidas, se responderá con un desafío de autenticación utilizando el esquema JWT Bearer. Esto es esencial para garantizar que los clientes sepan que deben proporcionar un token JWT válido para acceder a los recursos protegidos de la API.
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

/// Configuración de JWT Bearer
.AddJwtBearer(options =>
{
    /// Convierte la clave secreta de JWT almacenada en la configuración en un arreglo de bytes utilizando UTF-8, lo que es necesario para crear una instancia de SymmetricSecurityKey que se utilizará para validar la firma de los tokens JWT. Esta clave es fundamental para garantizar la integridad y autenticidad de los tokens emitidos por la API, asegurando que solo los tokens firmados con esta clave sean considerados válidos durante el proceso de autenticación.
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
    
    /// Configura los parámetros de validación del token JWT, asegurando que el emisor, la audiencia, la vida útil y la clave de firma sean validados correctamente. Esto es crucial para garantizar que solo los tokens válidos emitidos por la fuente confiable puedan acceder a los recursos protegidos de la API, proporcionando así una capa de seguridad robusta contra accesos no autorizados
    options.TokenValidationParameters = new TokenValidationParameters
    {
        /// Habilita la validación del emisor del token, asegurando que el token provenga de una fuente confiable y autorizada. Esto ayuda a prevenir ataques de suplantación de identidad, donde un atacante podría intentar usar un token emitido por una fuente no autorizada para acceder a los recursos protegidos de la API.
        ValidateIssuer = true,

        /// Habilita la validación de la audiencia del token, asegurando que el token esté destinado a ser utilizado por esta API específica. Esto es importante para evitar que un token emitido para otra API o servicio sea utilizado para acceder a los recursos de esta API, lo que podría resultar en un acceso no autorizado.
        ValidateAudience = true,

        /// Habilita la validación de la vida útil del token, asegurando que el token no sea utilizado después de su fecha de expiración. Esto es crucial para garantizar que los tokens caducados no puedan ser reutilizados por atacantes para acceder a los recursos protegidos de la API, proporcionando así una capa adicional de seguridad contra el uso indebido de tokens antiguos.
        ValidateLifetime = true,

        /// Habilita la validación de la firma del token, asegurando que el token no haya sido alterado y que provenga de una fuente confiable. Esto es esencial para garantizar la integridad y autenticidad de los tokens JWT, ya que solo los tokens firmados con la clave secreta configurada serán considerados válidos durante el proceso de autenticación.
        ValidateIssuerSigningKey = true,

        /// Especifica el emisor válido del token, que debe coincidir con el valor configurado en la aplicación. Esto ayuda a garantizar que solo los tokens emitidos por la fuente confiable y autorizada sean aceptados por la API, proporcionando así una capa de seguridad adicional contra ataques de suplantación de identidad.
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        /// Especifica la audiencia válida del token, que debe coincidir con el valor configurado en la aplicación. Esto es importante para garantizar que solo los tokens destinados a esta API específica sean aceptados, evitando así que tokens emitidos para otras APIs o servicios sean utilizados para acceder a los recursos de esta API.
        ValidAudience = builder.Configuration["Jwt:Issuer"],

        /// Proporciona la clave de firma que se utilizará para validar la firma de los tokens JWT. Esto es fundamental para garantizar la integridad y autenticidad de los tokens emitidos por la API, asegurando que solo los tokens firmados con esta clave sean considerados válidos durante el proceso de autenticación.
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    /// Configura los eventos de JWT Bearer para extraer el token de la cabecera Authorization en cada solicitud entrante. Esto es necesario para permitir que la API procese correctamente los tokens JWT enviados por los clientes, asegurando que el token se extraiga y valide adecuadamente durante el proceso de autenticación.
    options.Events = new JwtBearerEvents
    {

        /// Este evento se ejecuta cada vez que se recibe una solicitud que requiere autenticación. En este caso, se extrae el token JWT de la cabecera Authorization de la solicitud, verificando que comience con "Bearer " y luego asignando el token a context.Token para que pueda ser validado posteriormente por el middleware de autenticación. Esto es esencial para garantizar que los tokens JWT enviados por los clientes sean procesados correctamente durante el proceso de autenticación.
        OnMessageReceived = context =>
        {
            /// Extrae el valor de la cabecera "Authorization" de la solicitud entrante, buscando el primer valor que comience con
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            /// Verifica que la cabecera no esté vacía y que comience
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                /// Si la cabecera es válida, extrae el token JWT eliminando el prefijo "Bearer " y asignándolo a context.Token para que pueda ser validado posteriormente por el middleware de autenticación.
                context.Token = authHeader.Substring("Bearer ".Length).Trim();
            }

            /// Completa la tarea sin realizar ninguna acción adicional, ya que la extracción del token se ha manejado en el bloque anterior. Esto permite que el proceso de autenticación continúe normalmente, utilizando el token extraído para validar la autenticidad de la solicitud.
            return Task.CompletedTask;
        }
    };
});

/// CORS (ACTIVO)
// Servicios
/// Configuración de controladores y opciones de JSON para evitar ciclos de referencia al serializar objetos relacionados, lo que es común en modelos con relaciones de navegación en Entity Framework. Esto asegura que la API pueda devolver respuestas JSON sin problemas de serialización, incluso cuando los modelos contienen referencias circulares.
builder.Services.AddControllers()

    /// Configura las opciones de JSON para evitar ciclos de referencia al serializar objetos relacionados, lo que es común en modelos con relaciones de navegación en Entity Framework. Esto asegura que la API pueda devolver respuestas JSON sin problemas de serialización, incluso cuando los modelos contienen referencias circulares.
    .AddJsonOptions(options =>
    {
        /// Configura el manejador de referencias para JSON para ignorar los ciclos de referencia, lo que es común en modelos con relaciones de navegación en Entity Framework. Esto asegura que la API pueda devolver respuestas JSON sin problemas de serialización, incluso cuando los modelos contienen referencias circulares, evitando así excepciones de serialización y permitiendo que los clientes reciban respuestas JSON válidas.
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

/// Configuración de Swagger para la documentación de la API, incluyendo la definición de un esquema de seguridad para JWT Bearer, lo que permite a los desarrolladores probar los endpoints protegidos con tokens JWT directamente desde la interfaz de Swagger. Esto mejora la experiencia de desarrollo y facilita la integración con clientes que requieren autenticación basada en tokens.
builder.Services.AddEndpointsApiExplorer();

///
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "RecochApp API", Version = "v1" });

    // 🔐 Swagger con JWT
    /// Agrega una definición de seguridad para JWT Bearer en Swagger, lo que permite a los desarrolladores probar los endpoints protegidos con tokens JWT directamente desde la interfaz de Swagger. Esto mejora la experiencia de desarrollo y facilita la integración con clientes que requieren autenticación basada en tokens, proporcionando una forma sencilla de incluir el token JWT en las solicitudes de prueba.
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {

        /// Define el nombre del esquema de seguridad como "Authorization
        Name = "Authorization",

        /// Especifica que el tipo de esquema de seguridad es HTTP, lo que indica que se utilizará un esquema de autenticación basado en tokens, como JWT Bearer. Esto es esencial para configurar correctamente la autenticación en Swagger y permitir a los desarrolladores probar los endpoints protegidos con tokens JWT directamente desde la interfaz de Swagger.
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,

        /// Especifica que el esquema de autenticación es "bearer", lo
        Scheme = "bearer",

        /// Especifica que el formato del token es JWT, lo que indica que se espera que los tokens de autenticación sean JSON Web Tokens. Esto es importante para configurar correctamente la autenticación en Swagger y permitir a los desarrolladores probar los endpoints protegidos con tokens JWT directamente desde la interfaz de Swagger.
        BearerFormat = "JWT",

        /// Especifica que el token JWT se debe enviar en la cabecera de la solicitud, lo que es común para los esquemas de autenticación basados en tokens. Esto es esencial para configurar correctamente la autenticación en Swagger y permitir a los desarrolladores probar los endpoints protegidos con tokens JWT directamente desde la interfaz de Swagger, asegurando que el token se incluya en las solicitudes de prueba.
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,

        /// Proporciona una descripción del esquema de seguridad, indicando que se debe incluir un token JWT válido en la cabecera de la solicitud para acceder a los endpoints protegidos. Esto es útil para informar a los desarrolladores sobre cómo utilizar el esquema de seguridad en Swagger y asegurarse de que comprendan la necesidad de incluir un token JWT válido al probar los endpoints protegidos.
        Description = "Ejemplo: Bearer tu_token"
    });

    /// Agrega un requisito de seguridad para JWT Bearer en Swagger, lo que indica que los endpoints protegidos requieren un token JWT válido para acceder. Esto es esencial para configurar correctamente la autenticación en Swagger y permitir a los desarrolladores probar los endpoints protegidos con tokens JWT directamente desde la interfaz de Swagger, asegurando que comprendan la necesidad de incluir un token JWT válido al probar los endpoints protegidos.
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            /// Define una referencia a la definición de seguridad "Bearer
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                /// Especifica que el esquema de seguridad es una referencia a la definición de seguridad "Bearer", lo que indica que los endpoints protegidos requieren un token JWT válido para acceder. Esto es esencial para configurar correctamente la autenticación en Swagger y permitir a los desarrolladores probar los endpoints protegidos con tokens JWT directamente desde la interfaz de Swagger, asegurando que comprendan la necesidad de incluir un token JWT válido al probar los endpoints protegidos.
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    /// Especifica que el tipo de referencia es un esquema de seguridad, lo que indica que se está haciendo referencia a una definición de seguridad en Swagger. Esto es esencial para configurar correctamente la autenticación en Swagger y permitir a los desarrolladores probar los endpoints protegidos con tokens JWT directamente desde la interfaz de Swagger, asegurando que comprendan la necesidad de incluir un token JWT válido al probar los endpoints protegidos.
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,

                    /// Especifica el Id de la referencia como "Bearer", lo que indica que se está haciendo referencia a la definición de seguridad "Bearer
                    Id = "Bearer"
                }
            },
            /// Especifica que no se requieren scopes adicionales para este esquema de seguridad, lo que es común para los esquemas de autenticación basados en tokens como JWT Bearer. Esto es esencial para configurar correctamente la autenticación en Swagger y permitir a los desarrolladores probar los endpoints protegidos con tokens JWT directamente desde la interfaz de Swagger, asegurando que comprendan la necesidad de incluir un token JWT válido al probar los endpoints protegidos.
            Array.Empty<string>()
        }
    });
});

/// Construcción de la aplicación
var app = builder.Build();

// Swagger
/// Configura el middleware de Swagger para la documentación de la API, habilitando la interfaz de usuario de Swagger solo en entornos de desarrollo. Esto permite a los desarrolladores acceder a la documentación interactiva de la API y probar los endpoints directamente desde la interfaz de Swagger durante el desarrollo, mientras que en entornos de producción se mantiene deshabilitada para mejorar la seguridad.
if (app.Environment.IsDevelopment())
{
    /// Habilita el middleware de Swagger para generar la documentación de la API, lo que permite a los desarrolladores acceder a la documentación interactiva de la API y probar los endpoints directamente desde la interfaz de Swagger durante el desarrollo. Esto es esencial para mejorar la experiencia de desarrollo y facilitar la integración con clientes que requieren autenticación basada en tokens, proporcionando una forma sencilla de incluir el token JWT en las solicitudes de prueba.
    app.UseSwagger();

    /// Habilita la interfaz de usuario de Swagger, lo que permite a los desarrolladores acceder a la documentación interactiva de la API y probar los endpoints directamente desde la interfaz de Swagger durante el desarrollo. Esto es esencial para mejorar la experiencia de desarrollo y facilitar la integración con clientes que requieren autenticación basada en tokens, proporcionando una forma sencilla de incluir el token JWT en las solicitudes de prueba.
    app.UseSwaggerUI();
}

/// Configuración del middleware de la aplicación, incluyendo HTTPS redirection, autenticación y autorización. El orden correcto de estos middlewares es crucial para garantizar que las solicitudes sean procesadas de manera segura y eficiente, asegurando que las solicitudes se redirijan a HTTPS antes de aplicar la autenticación y autorización, lo que protege los datos en tránsito y garantiza que solo los usuarios autenticados y autorizados puedan acceder a los recursos protegidos de la API.
app.UseHttpsRedirection();

// 🔐 ORDEN CORRECTO
/// Configura el middleware de autenticación, lo que permite a la aplicación procesar las solicitudes entrantes y validar los tokens JWT para autenticar a los usuarios. Esto es esencial para garantizar que solo los usuarios autenticados puedan acceder a los recursos protegidos de la API, proporcionando así una capa de seguridad basada en tokens.
app.UseAuthentication();

/// Configura el middleware de autorización, lo que permite a la aplicación aplicar las políticas de autorización y controlar el acceso a los recursos protegidos de la API en función de los roles o claims del usuario autenticado. Esto es esencial para garantizar que solo los usuarios autorizados puedan acceder a ciertos endpoints o realizar ciertas acciones en la API, proporcionando así una capa adicional de seguridad y control de acceso.
app.UseAuthorization();

/// Configura el enrutamiento de la aplicación para mapear las solicitudes entrantes a los controladores y acciones correspondientes. Esto es esencial para que la aplicación pueda procesar las solicitudes HTTP y devolver las respuestas adecuadas, permitiendo a los clientes interactuar con los endpoints de la API de manera eficiente.
app.MapControllers();

/// Ejecuta la aplicación, iniciando el servidor web y permitiendo que la API esté disponible para recibir solicitudes entrantes. Esto es esencial para que la aplicación pueda atender las solicitudes de los clientes y proporcionar las funcionalidades definidas en los controladores de la API.
app.Run();