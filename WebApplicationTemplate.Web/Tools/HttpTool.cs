using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace WebApplicationTemplate.Web.Tools
{
    public static class HttpTool
    {
        private const int DEFAULT_TIMEOUT_SECONDS = 30;

        public static void Redirect(string url)
        {
            HttpContext.Current.Response.Redirect(url, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static BasicHttpBinding CreateBinding(string url)
        {
            BasicHttpBinding binding = new BasicHttpBinding();

            binding.CloseTimeout = new TimeSpan(0, 0, DEFAULT_TIMEOUT_SECONDS);
            binding.OpenTimeout = new TimeSpan(0, 0, DEFAULT_TIMEOUT_SECONDS);
            binding.ReceiveTimeout = new TimeSpan(0, 0, DEFAULT_TIMEOUT_SECONDS);
            binding.SendTimeout = new TimeSpan(0, 0, DEFAULT_TIMEOUT_SECONDS);
            binding.MaxReceivedMessageSize = int.MaxValue;

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.Certificate;
            }

            return binding;
        }

        public static EndpointAddress CreateEndpointAddress(string url)
        {
            EndpointAddress remoteAddress = new EndpointAddress(url);

            return remoteAddress;
        }
    }
}