//==============================================SERVER PART======================================================

//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//class ClientInfo
//{
//    public IPEndPoint EP;
//    public string Name;
//    public bool IsBlocked;
//}
//class Program
//{
//    static void Main()
//    {
//        int serverPort = 45678;
//        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//        server.Bind(new IPEndPoint(IPAddress.Any, serverPort));

//        var clients = new List<ClientInfo>();
//        byte[] buffer = new byte[1024];

//        Console.WriteLine($"\n\t\t Chat Server : {serverPort}");

//        new Thread(() =>
//        {
//            while (true)
//            {
//                Console.WriteLine("\n\t 1 - Block ");
//                Console.WriteLine("\t 2 - UnBlock");
//                Console.WriteLine("\t 3 - List");
//                Console.WriteLine("\t 4 - Exit");
//                Console.Write("\tEnter choice : \n\n");
//                string choice = Console.ReadLine();
//                Console.Write("\n\n\n");



//                switch (choice)
//                {
//                    case "1":
//                        Console.Write("Enter name to block : ");

//                        string blockName = Console.ReadLine();
//                        var blockClient = clients.Find(c => c.Name.Equals(blockName, StringComparison.OrdinalIgnoreCase));

//                        if (blockClient != null)
//                        {
//                            blockClient.IsBlocked = true;
//                            Console.WriteLine($"{blockName} blocked !");
//                        }
//                        else
//                        {
//                            Console.WriteLine("User not found !");
//                        }

//                        break;
//                    case "2":
//                        Console.Write("Enter name to unblock : ");

//                        string unblockName = Console.ReadLine();
//                        var unblockClient = clients.Find(c => c.Name.Equals(unblockName, StringComparison.OrdinalIgnoreCase));

//                        if (unblockClient != null)
//                        {
//                            unblockClient.IsBlocked = false;
//                            Console.WriteLine($"{unblockName} unblocked !");
//                        }
//                        else
//                        {
//                            Console.WriteLine("User not found !");
//                        }

//                        break;
//                    case "3":
//                        Console.WriteLine("Online Users : ");

//                        foreach (var c in clients)
//                        {
//                            Console.WriteLine($"{c.Name} ({c.EP.Port}) {(c.IsBlocked ? "[Blocked]" : "")}");
//                        }
//                        break;

//                    case "4": 
//                        Environment.Exit(0);
//                        break;

//                    default:
//                        Console.WriteLine("Invalid choice !");
//                        break;
//                }
//            }
//        })
//        { IsBackground = true }.Start();


//        while (true)
//        {
//            EndPoint senderEP = new IPEndPoint(IPAddress.Any, 0);
//            int len = server.ReceiveFrom(buffer, ref senderEP);
//            string msg = Encoding.UTF8.GetString(buffer, 0, len);
//            var sender = (IPEndPoint)senderEP;

//            if (msg.StartsWith("/join "))
//            {
//                string name = msg.Substring(6).Trim();
//                if (!clients.Exists(c => c.EP.Equals(sender)))
//                {
//                    clients.Add(new ClientInfo { EP = sender, Name = name, IsBlocked = false });
//                    Console.WriteLine($"{name} join ({sender})");
//                }
//                continue;
//            }

//            var senderClient = clients.Find(x => x.EP.Equals(sender));

//            if (senderClient == null)
//            {
//                continue;
//            }

//            if (senderClient.IsBlocked)
//            {
//                server.SendTo(Encoding.UTF8.GetBytes("You are blocked !"), sender);
//                continue;
//            }

//            Console.WriteLine($"[{senderClient.Name}] -> {msg}");

//            foreach (var c in clients.ToArray())
//            {
//                if (!c.EP.Equals(sender) && !c.IsBlocked)
//                {
//                    server.SendTo(Encoding.UTF8.GetBytes($"\n\t\t [{senderClient.Name}] : {msg}"), c.EP);
//                }
//            }
//        }
//    }
//}

////==============================================Customer One PART======================================================

//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//class Client1
//{
//    static void Main()
//    {
//        int localPort = 45680;
//        int serverPort = 45678;

//        Console.Write("Enter name : ");
//        string Name = Console.ReadLine();

//        Console.Clear();

//        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//        socket.Bind(new IPEndPoint(IPAddress.Loopback, localPort));

//        socket.SendTo(Encoding.UTF8.GetBytes("/join " + Name), new IPEndPoint(IPAddress.Loopback, serverPort));
//        Console.WriteLine($"[{Name}] enter message : ");

//        new Thread(() =>
//        {
//            byte[] buffer = new byte[1024];
//            EndPoint ep = new IPEndPoint(IPAddress.Any, 0);

//            while (true)
//            {
//                int len = socket.ReceiveFrom(buffer, ref ep);
//                string msg = Encoding.UTF8.GetString(buffer, 0, len);
//                Console.ForegroundColor = ConsoleColor.Cyan;
//                Console.WriteLine(msg);
//                Console.ResetColor();
//            }
//        })
//        { IsBackground = true }.Start();

//        while (true)
//        {
//            string msg = Console.ReadLine();
//            if (!string.IsNullOrWhiteSpace(msg))
//            {
//                socket.SendTo(Encoding.UTF8.GetBytes(msg), new IPEndPoint(IPAddress.Loopback, serverPort));
//            }
//        }
//    }
//}

////==============================================Customer Two PART======================================================

//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;

//class Client2
//{
//    static void Main()
//    {
//        int localPort = 45681;
//        int serverPort = 45678;

//        Console.Write("Enter name : ");
//        string Name = Console.ReadLine();

//        Console.Clear();

//        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//        socket.Bind(new IPEndPoint(IPAddress.Loopback, localPort));

//        socket.SendTo(Encoding.UTF8.GetBytes("/join " + Name), new IPEndPoint(IPAddress.Loopback, serverPort));
//        Console.WriteLine($"[{Name}] enter message : ");

//        new Thread(() =>
//        {
//            byte[] buffer = new byte[1024];
//            EndPoint ep = new IPEndPoint(IPAddress.Any, 0);

//            while (true)
//            {
//                int len = socket.ReceiveFrom(buffer, ref ep);
//                string msg = Encoding.UTF8.GetString(buffer, 0, len);
//                Console.ForegroundColor = ConsoleColor.Cyan;
//                Console.WriteLine(msg);
//                Console.ResetColor();
//            }
//        })
//        { IsBackground = true }.Start();

//        while (true)
//        {
//            string msg = Console.ReadLine();
//            if (!string.IsNullOrWhiteSpace(msg))
//            {
//                socket.SendTo(Encoding.UTF8.GetBytes(msg), new IPEndPoint(IPAddress.Loopback, serverPort));
//            }
//        }
//    }
//}

////==============================================Customer Three PART====================================================

//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;

//class Client3
//{
//    static void Main()
//    {
//        int localPort = 45682;
//        int serverPort = 45678;

//        Console.Write("Enter name : ");
//        string Name = Console.ReadLine();

//        Console.Clear();

//        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//        socket.Bind(new IPEndPoint(IPAddress.Loopback, localPort));

//        socket.SendTo(Encoding.UTF8.GetBytes("/join " + Name), new IPEndPoint(IPAddress.Loopback, serverPort));
//        Console.WriteLine($"[{Name}] enter message : ");

//        new Thread(() =>
//        {
//            byte[] buffer = new byte[1024];
//            EndPoint ep = new IPEndPoint(IPAddress.Any, 0);

//            while (true)
//            {
//                int len = socket.ReceiveFrom(buffer, ref ep);
//                string msg = Encoding.UTF8.GetString(buffer, 0, len);
//                Console.ForegroundColor = ConsoleColor.Cyan;
//                Console.WriteLine(msg);
//                Console.ResetColor();
//            }
//        })
//        { IsBackground = true }.Start();

//        while (true)
//        {
//            string msg = Console.ReadLine();
//            if (!string.IsNullOrWhiteSpace(msg))
//            {
//                socket.SendTo(Encoding.UTF8.GetBytes(msg), new IPEndPoint(IPAddress.Loopback, serverPort));
//            }
//        }
//    }
//}
