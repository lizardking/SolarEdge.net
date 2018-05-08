using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;



namespace SolarEdgeService
{
    /// <summary>
    /// The ServiceForm is a frontend for the service if the service is beeing started like a normal exe not not like a service.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ServiceForm : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private class ServiceInfo
        {
            public ServiceBase Service { get; set; } = null;

            public string ServiceName
            {
                get
                {
                    return Service?.ServiceName;
                }
            }

            public ServiceCommands LastCommand { get; set; } = ServiceCommands.NoCommand;

            public string LastCommandResult { get; set; } = "<no command executed>";
        }

        List<ServiceInfo> ServiceInfos = new List<ServiceInfo>();


        public ServiceForm(ServiceBase[] ServicesToRun)
        {
            this.ServiceInfos = ServicesToRun.Select(SVC => new ServiceInfo() { Service = SVC }).ToList();

            InitializeComponent();

            Assembly CurrentAssembly = Assembly.GetEntryAssembly();
            string CurrentAssemblyName = CurrentAssembly.GetName().Name;
            this.Text = $"{this.Text}: {CurrentAssemblyName} at {CurrentAssembly.Location}";
            ServicesLabel.Text = $"Services in {CurrentAssemblyName} at {CurrentAssembly.Location}:";

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                if (SolarEdgeServiceSettings.Default.AutorunServicesInGUIMode)
                {
                    log.Info("Autostart services in GUI mode is enabled. Will launch service(s)");
                    AutoStartServicesCheckBox.Checked = true;
                    ExecuteServiceCommand(ServiceCommands.Start);
                }
                else
                {
                    AutoStartServicesCheckBox.Checked = false;
                }
            }
            else
            {
                log.Debug("Debugger detected. Hidding AutoStartServicesCheckBox");
                AutoStartServicesCheckBox.Visible = false;
            }
            PopulateServicesDataGrid();
            UpdateCommandButtons();
        }


        private void PopulateServicesDataGrid()
        {
            var GridData = ServiceInfos.Select(SI => new { ServiceName = SI.ServiceName, LastCommand = (SI.LastCommand != ServiceCommands.NoCommand ? SI.LastCommand.ToString() : ""), LastCommandResult = SI.LastCommandResult }).ToList();
            ServicesDataGridView.DataSource = GridData;
        }


        private enum ServiceCommands
        {
            NoCommand,
            Start,
            Stop
        }

        private void ExecuteServiceCommand(ServiceCommands ServiceCommand)
        {
            log.Info($"Will execute {ServiceCommand} for {ServiceInfos.Count()} service(s): {string.Join(", ", ServiceInfos.Select(SI => SI.ServiceName))}");


            object[] Arguments = null;
            string ServiceCommandName;
            switch (ServiceCommand)
            {
                case ServiceCommands.Start:
                    ServiceCommandName = "OnStart";
                    Arguments = new object[] { new string[] { "" } };
                    break;
                case ServiceCommands.Stop:
                    ServiceCommandName = "OnStop";
                    Arguments = null;

                    break;
                default:

                    Exception ex = new Exception($"Unknown {nameof(ServiceCommand)} {ServiceCommand}. Cant get name of method to invoke.");
                    log.Error(ex.Message, ex);
                    throw ex;
            }


            foreach (ServiceInfo ServiceInfo in ServiceInfos)
            {
                log.Debug($"Trying to {ServiceCommand.ToString().ToLowerInvariant()} service {ServiceInfo.ServiceName}");

                if (ServiceInfo.LastCommand != ServiceCommand)
                {
                    Type ServiceType = ServiceInfo.Service.GetType();

                    ServiceInfo.LastCommand = ServiceCommand;
                    ServiceInfo.LastCommandResult = null;
                    try
                    {
                        ServiceType.InvokeMember(
                            ServiceCommandName,
                            System.Reflection.BindingFlags.Instance
                            | System.Reflection.BindingFlags.InvokeMethod
                            | System.Reflection.BindingFlags.NonPublic
                            | System.Reflection.BindingFlags.Public,
                            null,
                            ServiceInfo.Service,
                            Arguments
                        );
                    }
                    catch (Exception E)
                    {
                        ServiceInfo.LastCommandResult = $"{E.GetType().Name}: {E.Message}";
                        log.Warn($"A exception occured while trying to {ServiceCommand.ToString().ToLowerInvariant()} service {ServiceInfo.ServiceName}. {E.Message}", E);
                    }
                }
                else
                {
                    log.Warn($"Command {ServiceCommand.ToString().ToLowerInvariant()} not executed from service {ServiceInfo.ServiceName} since the same command has been executed last.");
                    ServiceInfo.LastCommandResult = "Command not executed. Same command was the last command";
                }

            }
            if (ServiceInfos.Any(SI => SI.LastCommandResult != null))
            {
                log.Warn($"Executed {ServiceCommand.ToString().ToLowerInvariant()} for {ServiceInfos.Count()} service(s). Failed for {ServiceInfos.Where(SI => SI.LastCommandResult != null).Count()} service(s):\n {string.Join("\n", ServiceInfos.Where(SI => SI.LastCommandResult != null).Select(SI => $"{SI.ServiceName} -> {SI.LastCommandResult}"))}");
            }
            else
            {
                log.Info($"Executed {ServiceCommand.ToString().ToLowerInvariant()} for {ServiceInfos.Count()} service(s): {string.Join(", ", ServiceInfos.Select(SI => SI.ServiceName))}");
            }

            ServiceInfos.ForEach(SI => SI.LastCommandResult = SI.LastCommandResult ?? $"{ServiceCommand} executed");


        }

        private void UpdateCommandButtons()
        {
            StartAllButton.Enabled = ServiceInfos.All(SI => SI.LastCommand != ServiceCommands.Start);
            StopAllButton.Enabled = ServiceInfos.All(SI => SI.LastCommand != ServiceCommands.Stop);
        }


        private void StartAllButton_Click(object sender, EventArgs e)
        {
            ExecuteServiceCommand(ServiceCommands.Start);
            PopulateServicesDataGrid();
            UpdateCommandButtons();

        }

        private void StopAllButton_Click(object sender, EventArgs e)
        {
            ExecuteServiceCommand(ServiceCommands.Stop);

            PopulateServicesDataGrid();
            UpdateCommandButtons();
        }

        private void AutoStartServicesCheckBox_Click(object sender, EventArgs e)
        {

            SolarEdgeServiceSettings.Default.AutorunServicesInGUIMode = AutoStartServicesCheckBox.Checked;
            SolarEdgeServiceSettings.Default.Save();
            log.Info($"Updated user setting AutorunServicesInGUIMode to {AutoStartServicesCheckBox.Checked}");
        }

        private void ServiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ServiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServiceInfos.Any(SI => SI.LastCommand == ServiceCommands.Start))
            {
                if (MessageBox.Show(this, "Closing this form will stop all listed services.\nDo you want to continue?", "Stop services", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    e.Cancel = true;
                }
                else
                {
                    ExecuteServiceCommand(ServiceCommands.Stop);
                    PopulateServicesDataGrid();
                    UpdateCommandButtons();
                }

            }
        }
    }
}
