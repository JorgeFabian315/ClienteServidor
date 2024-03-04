// tcpListener, TCPCLient

using System.Net;
using System.Net.Sockets;
using System.Text;



TcpListener server = new(System.Net.IPAddress.Any, 8000);
server.Start();










var ips = Dns.GetHostAddresses(Dns.GetHostName());
Console.WriteLine("Mi ip es: " + ips.Where(x => x.AddressFamily == AddressFamily.InterNetwork)
    .Select(x => x.ToString()).FirstOrDefault() ?? "0.0.0.0");


while (true)
{
    TcpClient client = server.AcceptTcpClient();

    Console.WriteLine("Clinete aceptado " + client.Client.RemoteEndPoint?.ToString());

    Thread hilo = new(() =>
    {
        AtenderCliente(client);
    });
    hilo.IsBackground = true;
    hilo.Start();
}


void AtenderCliente(TcpClient client)
{
    while (true)
    {
        if (client.Available > 0)
        {
            var ns = client.GetStream();

            byte[] buffer = new byte[client.Available];
            ns.Read(buffer, 0, buffer.Length);

            Console.WriteLine("La ip es " + client.Client.RemoteEndPoint?.ToString() + ": " 
                + Encoding.UTF8.GetString(buffer));
        }
    }
}






