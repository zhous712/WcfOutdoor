using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace WcfOutdoor.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            Installers.Clear();

            ServiceInstaller serviceInstaller = new ServiceInstaller();
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = Program.SVC_NAME;
            serviceInstaller.DisplayName = Program.SVC_DSNAME;
            serviceInstaller.Description = Program.SVC_DESC;
            serviceInstaller.ServicesDependedOn = new string[] { "MSMQ" };
            Installers.Add(serviceInstaller);

            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
            processInstaller.Account = ServiceAccount.LocalSystem;
            processInstaller.Password = null;
            processInstaller.Username = null;

            Installers.Add(processInstaller);
        }

        private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController controller = null;
            ServiceController[] controllers = ServiceController.GetServices();
            for (int i = 0; i < controllers.Length; i++)
            {
                if (controllers[i].ServiceName == Program.SVC_NAME)
                {
                    controller = controllers[i];
                    break;
                }
            }
            if (controller == null)
            {
                return;
            }

            // if the service is not active, start it
            //if (controller.Status != ServiceControllerStatus.Running)
            //{
            //    string[] args = { "-install" };
            //    controller.Start(args);
            //}
        }
    }
}
