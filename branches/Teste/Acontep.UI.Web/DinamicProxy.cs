using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Security;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using System.Globalization;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Xml.Serialization;

namespace Acontep.UI.Web
{
    /// <summary>
    /// Classe que executa webservices genericos.
    /// Para a execucao ser um sucesso, faz-se necessario o conhecimento da ordem dos parametros do metodo a ser executado.
    /// </summary>
    public class DinamicProxy
    {
        Uri _uri;
        Type _serviceType = null;
        object _instance = null;

        public string Url
        {
            get
            {
                return _uri == null ? String.Empty :_uri.AbsoluteUri;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DinamicProxy"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public DinamicProxy(string url)
            : this(new Uri(url + "?WSDL"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DinamicProxy"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public DinamicProxy(Uri uri)
        {
            _uri = uri;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(SSLResult);
        }
        /// <summary>
        /// SSLs the result.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="c">The c.</param>
        /// <param name="chain">The chain.</param>
        /// <param name="sslPolicyErrors">The SSL policy errors.</param>
        /// <returns></returns>
        private bool SSLResult(Object sender, System.Security.Cryptography.X509Certificates.X509Certificate c,
                                System.Security.Cryptography.X509Certificates.X509Chain chain,
                                 System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        /// <summary>
        /// Parses the WSDL.
        /// </summary>
        /// <returns></returns>
        private ServiceDescription ParseWSDL()
        {
            WebRequest webReq = WebRequest.Create(_uri);
            Stream reqStrm = webReq.GetResponse().GetResponseStream();
            return ServiceDescription.Read(reqStrm, false);            
        }


        /// <summary>
        /// Creates the service description importer.
        /// </summary>
        /// <param name="serviceDesc">The service desc.</param>
        /// <returns></returns>
        private ServiceDescriptionImporter CreateServiceDescriptionImporter(ServiceDescription serviceDesc)
        {
            ServiceDescriptionImporter servImport = new ServiceDescriptionImporter();
            servImport.AddServiceDescription(serviceDesc, String.Empty, String.Empty);
            servImport.ProtocolName = "Soap";
            servImport.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;
            return servImport;
        }


        /// <summary>
        /// Gets the compiled assemble.
        /// </summary>
        /// <param name="servImport">The serv import.</param>
        /// <returns></returns>
        private Assembly GetCompiledAssemble(ServiceDescriptionImporter servImport)
        {
            CodeNamespace ns  = new CodeNamespace();
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(ns);
            servImport.Import(ns, ccu);
        

            StringWriter sw = new StringWriter(CultureInfo.CurrentCulture);
            CSharpCodeProvider prov = new CSharpCodeProvider();
            prov.GenerateCodeFromNamespace(ns, sw, null);


            CompilerParameters param = new CompilerParameters(new String[]{ "System.dll", "System.Xml.dll", "System.Web.Services.dll", "System.Data.dll"});
            param.GenerateExecutable = false;
            param.GenerateInMemory = true;
            param.TreatWarningsAsErrors = false;
            param.WarningLevel = 4;
            CompilerResults results = new CompilerResults(null);
            results = prov.CompileAssemblyFromSource(param, sw.ToString());
            return results.CompiledAssembly;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:DinamicProxy"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public bool Connected
        {
            get { return _instance != null; }
        }
        /// <summary>
        /// Criars the proxy.
        /// </summary>
        public void CriarProxy()
        {
            if (!Connected)
            {
                ServiceDescription serviceDescription = ParseWSDL();
                ServiceDescriptionImporter servImport = CreateServiceDescriptionImporter(serviceDescription);
                Assembly dll = GetCompiledAssemble(servImport);
                _serviceType = dll.GetType(serviceDescription.Services[0].Name);
                _instance = Activator.CreateInstance(_serviceType);
            }

        }
        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public MethodInfo GetMethod(string methodName)
        {
            return _serviceType.GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public);
        }


        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public ParameterInfo[] GetParameters(string methodName)
        {
            return _serviceType.GetMethod(methodName).GetParameters();
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            _serviceType = null;
            _instance = null;
            CriarProxy();
        }

        /// <summary>
        /// Invokes the specified method name.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public object Invoke(string methodName, params object[] obj)
        {
            CriarProxy();            
            MethodInfo methodInfo = _serviceType.GetMethod(methodName);
            return methodInfo.Invoke(_instance, obj);
        }

    }
}