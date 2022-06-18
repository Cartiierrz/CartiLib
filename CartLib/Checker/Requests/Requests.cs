using CartiLib.Checker.Proxies;
using CartiLib.Configuration;
using Leaf.xNet;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CartiLib.Checker.Requests
{
    internal class Requests
    {
        public static void SetEssentials(HttpRequest httpRequest, bool IgnoreProtocolErrors, bool KeepAlive) //quickly sets the base of for requests in a module
        {
            switch (SettingsModel.UseProxyAuth)
            {
                case true when CheckerVariables.ProxyProtocol != 4:
                    switch (SettingsModel.ProxyAuthType)
                    {
                        case "IP:PORT:USER:PASS":
                            var pArray = ProxyGrabber.Init().Split(':');
                            var aProxy = ProxyClient.Parse(CheckerVariables.Proxytype, $"{pArray[0]}:{pArray[1]}");
                            aProxy.Username = pArray[2];
                            aProxy.Password = pArray[3];
                            httpRequest.Proxy = aProxy;
                            break;
                        case "USER:PASS:IP:PORT":
                            var pArray1 = ProxyGrabber.Init().Split(':');
                            var aProxy1 = ProxyClient.Parse(CheckerVariables.Proxytype, $"{pArray1[2]}:{pArray1[3]}");
                            aProxy1.Username = pArray1[0];
                            aProxy1.Password = pArray1[1];
                            httpRequest.Proxy = aProxy1;
                            break;
                    }
                    break;
                case false when CheckerVariables.ProxyProtocol != 4:
                    httpRequest.Proxy = ProxyClient.Parse(CheckerVariables.Proxytype, ProxyGrabber.Init());
                    break;
            }
            httpRequest.SslCertificateValidatorCallback = (RemoteCertificateValidationCallback)Delegate.Combine(httpRequest.SslCertificateValidatorCallback, new RemoteCertificateValidationCallback((obj, cert, ssl, error) => ((X509Certificate2)cert).Verify()));
            httpRequest.ConnectTimeout = SettingsModel.ProxyTimeout;
            httpRequest.ReadWriteTimeout = SettingsModel.ProxyTimeout;
            httpRequest.KeepAliveTimeout = SettingsModel.ProxyTimeout;
            httpRequest.AllowEmptyHeaderValues = true;
            switch (IgnoreProtocolErrors)
            {
                case true:
                    httpRequest.IgnoreProtocolErrors = true;
                    break;
                case false:
                    httpRequest.IgnoreProtocolErrors = false;
                    break;
            }
            switch (KeepAlive)
            {
                case true:
                    httpRequest.KeepAlive = true;
                    break;
                case false:
                    httpRequest.KeepAlive = false;
                    break;
            }
        }
    }
}
