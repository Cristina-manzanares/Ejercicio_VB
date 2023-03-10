using System.Net.Sockets;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net.NetworkInformation;
using System.Buffers;
using Microsoft.WindowsAPICodePack.Net;
using System.Diagnostics;
//using System.Management

namespace Ejercicio_VB
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(Ip(), Gateway(), Username(), Hostname(), Ssid(), Ssid_status(), Mac(), Int_conn(), Vb(), Vb_vers()));
        }

        public static string Ip()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No tienes un adaptador de IPv4");

        }

        public static IPAddress Gateway()
        {
            IPAddress result = null;
            var cards = NetworkInterface.GetAllNetworkInterfaces().ToList();
            if (cards.Any())
            {
                foreach (var card in cards)
                {
                    var props = card.GetIPProperties();
                    if (props == null)
                        continue;

                    var gateways = props.GatewayAddresses;
                    if (!gateways.Any())
                        continue;

                    var gateway =
                        gateways.FirstOrDefault(g => g.Address.AddressFamily.ToString() == "InterNetwork");
                    if (gateway == null)
                        continue;

                    result = gateway.Address;
                    break;
                };
            }

            return result;
        }

        public static string Username()
        {
            return Environment.UserName;
        }

        public static string Hostname()
        {
            return System.Environment.MachineName;
        }

        public static string Ssid()
        {
            if (NetworkListManager.GetNetworks(NetworkConnectivityLevels.Connected).ToArray().Length > 0)
            {
                return NetworkListManager.GetNetworks(NetworkConnectivityLevels.Connected).ToArray()[0].Name;
            }
            else { return "no net"; }


        }

        public static string Ssid_status()
        {
            if (NetworkListManager.GetNetworks(NetworkConnectivityLevels.Connected).ToArray().Length > 0)
            {
                return NetworkListManager.GetNetworks(NetworkConnectivityLevels.Connected).ToArray()[0].IsConnected ? "Connected" : "Disconnected";

            }
            else { return "Disconnected"; }
        }

        public static string Mac()
        {
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(q => q.OperationalStatus == OperationalStatus.Up);

            if (networkInterface == null)
            {
                return string.Empty;
            }

            return BitConverter.ToString(networkInterface.GetPhysicalAddress().GetAddressBytes());
            //return "dsf";

        }

        public static string Int_conn()
        {
            IPStatus[] iPStatuses = new IPStatus[4];

            try
            {
                int count = 0;

                for (int i = 0; i < 4; i++)
                {
                    PingReply reply = new Ping().Send("cloudflare.com");
                    iPStatuses[i] = reply.Status;

                    if (iPStatuses[i] == IPStatus.Success)
                    {
                        count++;
                    }

                }

                if (count == 4)
                {
                    return "Established";
                }
                else if (count > 0)
                {
                    return "Unstable";
                }
                else { return "Disconnected"; }


            }
            catch (PingException)
            {
                return "error";
            }

        }

        private static string Vb()
        {
            return File.Exists(Environment.ExpandEnvironmentVariables("%ProgramW6432%") + @"\Oracle\VirtualBox\VBoxManage.exe") ? "Yes" : "N/A";
        }

        public static string Vb_vers()
        {
            if (File.Exists(Environment.ExpandEnvironmentVariables("%ProgramW6432%") + @"\Oracle\VirtualBox\VBoxManage.exe"))
            {
                var version = FileVersionInfo.GetVersionInfo(Environment.ExpandEnvironmentVariables("%ProgramW6432%") + @"\Oracle\VirtualBox\VBoxManage.exe").FileVersion;

                return version == null ? "N/A" : version;
            }
            else
            {
                return "N/A";
            }
        }






    }
}