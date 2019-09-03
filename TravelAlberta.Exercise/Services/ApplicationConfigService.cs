using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace TravelAlberta.Exercise.Services
{
    /// <summary>
    /// Holds applicaiton configuration values parsed from the Web.config file. 
    /// </summary>
    public class ApplicationConfigService : IServerInfo, IConfigInfo, IVersionInfo
    {
        public string CsvReadUrl { get; protected set; }
        
        public string Server { get; protected set; }
        public string IpAddress { get; protected set; }

        public string Version { get; protected set; }

        public ApplicationConfigService()
        {
            this.GetServerInfo();
            this.GetConfigInfo();
            this.GetVersionInfo();
        }

        private void GetConfigInfo()
        {
            CsvReadUrl = Get("CsvReadUrl");
        }

        private void GetVersionInfo()
        {
            Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        }

        private void GetServerInfo()
        {
            //Init to empty and then populate.
            this.IpAddress = this.Server = string.Empty;

            var hostname = Dns.GetHostName();
            var addresses = Dns.GetHostEntry(hostname);

            foreach (var ip in addresses.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    this.IpAddress = ip.ToString();

                    var segments = ip.ToString().Split('.');
                    if (segments.FirstOrDefault() == "192")
                    {
                        this.Server = segments.Last();
                    }
                }
            }
        }

        #region ConfigurationManager mutators

        private string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private void Set(string key, string value)
        {
            ConfigurationManager.AppSettings[key] = value;
        }

        #endregion

        
    }
}