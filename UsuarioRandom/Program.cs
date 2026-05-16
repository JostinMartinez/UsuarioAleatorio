using System.Net.Http.Json;

bool contuniar = true;

while (contuniar)
{
    Console.WriteLine("Cuantos Usauarios deseas obtenerr ");

    string cantidad = Console.ReadLine();

    if (!int.TryParse(cantidad, out int cantidadInt) || cantidadInt <= 0)
    {
        Console.WriteLine("Debes ingresar un numero valido.");
        continue;
    }

    string url = $"https://randomuser.me/api/?results={cantidadInt}";

    bool cargando = true;

    // Indicador de progreso
    var loadingTask = Task.Run(async () =>
    {
        string[] spinner = { "|", "/", "-", "\\" };
        int index = 0;

        while (cargando)
        {
            Console.Write($"\rCargando usuarios... {spinner[index++ % spinner.Length]}");
            await Task.Delay(100);
        }
    });

    try
    {
        var result = await getUser(url);

        cargando = false;

        await loadingTask;

        Console.WriteLine("\n");

        foreach (var item in result.results)
        {
            Console.WriteLine($"Nombre: {item.name.first} {item.name.last}");
            Console.WriteLine($"Genero: {item.gender}");
            Console.WriteLine($"Email: {item.email}");
            Console.WriteLine($"Telefono: {item.phone}");
            Console.WriteLine($"Ciudad: {item.location.city}");
            Console.WriteLine($"Pais: {item.location.country}");
            Console.WriteLine("--------------------------------------------------");
        }
    }
    catch (Exception ex)
    {
        cargando = false;

        await loadingTask;

        Console.WriteLine($"\nOcurrio un error: {ex.Message}");
    }

    Console.WriteLine("\nDeseas buscar mas usuarios?");
    Console.WriteLine("1 - Si");
    Console.WriteLine("2 - No");

    string opcion = Console.ReadLine();

    contuniar = opcion == "1";



    async Task<Root> getUser(string url)
    {
        while (true)
        {
            try
            {
                var http = new HttpClient();

                var result = await http.GetFromJsonAsync<Root>(url);

                if (result == null)
                {
                    throw new Exception("La respuesta vino vacia.");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nOcurrio un error: {ex.Message}");

                Console.WriteLine("Reintentando...\n");

                await Task.Delay(2000);
            }
        }
    }
}