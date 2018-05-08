using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4NetConfig.xml", Watch = true)]
namespace SolarEdgeService
{
    static class Program
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        static void Main()
        {
            Assembly CurrentAssembly = typeof(Program).Assembly;

            log.Info("");
            log.Info("**********************************************************************************************************************************************************************************************************************************");
            log.Info("**********************************************************************************************************************************************************************************************************************************");
            log.Info($"**   {CurrentAssembly.GetName().Name}");
            log.Info($"**-------------------------------------------------------------------------------------");
            log.Info($"**   Version: {CurrentAssembly.GetName().Version}");
            log.Info($"**   Location: {CurrentAssembly.Location}");
            log.Info("**********************************************************************************************************************************************************************************************************************************");
            log.Info("**********************************************************************************************************************************************************************************************************************************");

            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;



            ServiceBase[] ServicesToRun = CurrentAssembly.GetTypes().Where(t => !t.IsAbstract && !t.IsInterface && t != typeof(ServiceBase) && typeof(ServiceBase).IsAssignableFrom(t)).Select(t => (ServiceBase)Activator.CreateInstance(t)).ToArray();
            log.Info($"Discovered {ServicesToRun.Length} service(s) in assembly {CurrentAssembly.GetName().Name}: {string.Join(",", ServicesToRun.Select(SVC => SVC.ServiceName))}");

            //Check if program has been started in interactive mode. If yes, launch the front end. If no, launch the service.
            if (Environment.UserInteractive)
            {
                //Start GUI mode
                log.Info("Launching interactive GUI mode");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ServiceForm(ServicesToRun));

            }
            else
            {
                //Start service mode
                log.Info("Launching Service mode");
                ServiceBase.Run(ServicesToRun);
            }

            log.Info($"{CurrentAssembly.GetName().Name} terminates................");
            log.Info("**********************************************************************************************************************************************************************************************************************************");
            log.Info("");
        }
    }
}
