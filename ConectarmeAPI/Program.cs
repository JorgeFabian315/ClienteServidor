

HttpClient client = new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7132/")
};

Console.WriteLine("Usuario");
string username = Console.ReadLine() ?? "";

Console.WriteLine("Contraseña");
string password = Console.ReadLine() ?? "";


var response = client.PostAsync($"api/Login?username={username}&password={password}",null).Result;


//var response =  client.GetAsync("api/Saludos").Result;

var token = response.Content.ReadAsStringAsync().Result;

Console.WriteLine(token);

HttpRequestMessage rm = new();
rm.RequestUri = new Uri(client.BaseAddress + "api/saludos");
rm.Method = HttpMethod.Get;
rm.Headers.Add("Authorization",$"Bearer{token}");

var resp = client.SendAsync(rm).Result;
resp.EnsureSuccessStatusCode();

if(resp.StatusCode == System.Net.HttpStatusCode.Forbidden)
{
    Console.WriteLine("No autorizado");
}

var Saludo = resp.Content.ReadAsStringAsync().Result;

Console.WriteLine(Saludo);

Console.ReadLine();



























