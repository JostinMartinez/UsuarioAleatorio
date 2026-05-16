# UsuarioAleatorio

Aplicación de consola desarrollada en C# que consume la API pública de Random User Generator para obtener usuarios aleatorios y mostrarlos en pantalla.

---

# Funcionalidades

- Obtener usuarios aleatorios desde una API.
- Mostrar información detallada de cada usuario.
- Indicador visual de carga mientras se realiza la solicitud.
- Manejo de errores en caso de fallos de conexión o respuestas inválidas.
- Reintentos automáticos cuando ocurre un error.
- Posibilidad de continuar buscando usuarios o salir de la aplicación.
- Uso de programación asíncrona con `async/await`.

---

# Tecnologías utilizadas

- C#
- .NET
- HttpClient
- Random User API

---

# API utilizada

Random User Generator API:

https://randomuser.me/

Ejemplo:

```text
https://randomuser.me/api/?results=5
```

---

# Funcionamiento

## 1. Solicitud de cantidad de usuarios

El programa solicita al usuario la cantidad de usuarios que desea obtener.

```csharp
Console.WriteLine("Cuantos Usauarios deseas obtenerr ");
```

---

## 2. Construcción de la URL

La URL se genera dinámicamente utilizando la cantidad ingresada.

```csharp
string url = $"https://randomuser.me/api/?results={cantidadInt}";
```

---

## 3. Indicador de progreso

Mientras se realiza la solicitud HTTP, la aplicación muestra un spinner para indicar que el programa sigue trabajando.

```text
Cargando usuarios... |
Cargando usuarios... /
Cargando usuarios... -
Cargando usuarios... \
```

---

## 4. Consumo de la API

La aplicación utiliza `HttpClient` junto con `GetFromJsonAsync` para obtener y deserializar la información.

```csharp
var result = await http.GetFromJsonAsync<Root>(url);
```

---

## 5. Manejo de errores

Se utiliza `try-catch` para capturar errores de conexión o problemas en la respuesta de la API.

```csharp
catch (Exception ex)
{
    Console.WriteLine($"\nOcurrio un error: {ex.Message}");
}
```

---

## 6. Reintentos automáticos

Si ocurre un error, el programa espera unos segundos y vuelve a intentar la solicitud.

```csharp
Console.WriteLine("Reintentando...\n");

await Task.Delay(2000);
```

---

## 7. Visualización de usuarios

Los usuarios obtenidos se muestran en consola con información como:

- Nombre
- Género
- Email
- Teléfono
- Ciudad
- País

---

## 8. Continuar o salir

Al finalizar, el usuario puede decidir si desea realizar otra búsqueda o cerrar la aplicación.

```csharp
Console.WriteLine("1 - Si");
Console.WriteLine("2 - No");
```

---

# Programación asíncrona

La aplicación utiliza `async/await` para evitar bloquear la ejecución mientras se realizan solicitudes HTTP.

```csharp
var result = await getUser(url);
```

---

# Principios SOLID aplicados

## SRP — Single Responsibility Principle

El método `getUser` tiene una única responsabilidad:
obtener usuarios desde la API y manejar posibles errores.

---

## OCP — Open/Closed Principle

La lógica de consumo de la API está encapsulada en un método independiente, permitiendo modificar o extender la fuente de datos sin alterar el flujo principal del programa.

---

# Ejecutar el proyecto

## Clonar el repositorio

```bash
git clone <url-del-repositorio>
```

---

## Ejecutar la aplicación

```bash
dotnet run
```

---

# Ejemplo de salida

```text
Cuantos Usuarios deseas obtener
3

Cargando usuarios... /

Nombre: John Doe
Genero: male
Email: john.doe@email.com
Telefono: 809-000-0000
Ciudad: New York
Pais: United States
--------------------------------------------------
```
