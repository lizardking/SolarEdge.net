using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SolarEdgeService.Communication;

namespace SolarEdgeDataFetcher
{
    /// <summary>
    /// This class handles the WCF services.
    /// </summary>
    public class CommunicationManager
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Singleton implementation
        private static readonly Lazy<CommunicationManager> SingletonInstance = new Lazy<CommunicationManager>(() => new CommunicationManager());

        public static CommunicationManager Instance { get { return SingletonInstance.Value; } }

        private CommunicationManager()
        {
        }

        #endregion



        /// <summary>
        /// Gets a value indicating whether the WCF services are activated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the WCF services are activated; otherwise, <c>false</c>.
        /// </value>
        public bool IsActivated { get; private set; }

        ServiceHost SolarEdgeServiceHost = null;
        private object ServiceLocker = new object();



        /// <summary>
        /// Activates the WCF services.
        /// </summary>
        /// <exception cref="System.Exception">Could not activate WCF SolarEdgeService</exception>
        public void ActivateServices()
        {
            lock (ServiceLocker)
            {

                if (IsActivated)
                {
                    DeactivateServices();
                }

                SolarEdgeServiceHost = new ServiceHost(typeof(SolarEdgeWCFService))
                {
                    OpenTimeout = TimeSpan.FromSeconds(5),
                    CloseTimeout = TimeSpan.FromSeconds(2),
                   
                    
                };

                try
                {
                    SolarEdgeServiceHost.Open();
                }
                catch (Exception E)
                {
                    SolarEdgeServiceHost.Abort();
                    SolarEdgeServiceHost = null;
                    Log.Error("Could not activate WCF SolarEdgeService", E);
                    throw new Exception("Could not activate WCF SolarEdgeService", E);
                }


                IsActivated = true;

                DataFetcher.Instance.SolarEdgeDataUpdated += Instance_SolarEdgeDataUpdated;
                DataFetcher.Instance.SolarEdgeDataIsValidChanged += Instance_SolarEdgeDataIsValidChanged;
            }
            Log.Debug("Servicehost for SolarEdgeService opend.");
        }



        /// <summary>
        /// Deactivates the WCF services.
        /// </summary>
        /// <exception cref="System.Exception">Could not deactivate WCF SolarEdgeService</exception>
        public void DeactivateServices()
        {
            if (!IsActivated) return;

            if (SolarEdgeServiceHost != null)
            {
                DataFetcher.Instance.SolarEdgeDataUpdated -= Instance_SolarEdgeDataUpdated;
                DataFetcher.Instance.SolarEdgeDataIsValidChanged -= Instance_SolarEdgeDataIsValidChanged;
                try
                {
                    SolarEdgeServiceHost.Close(TimeSpan.FromSeconds(2));
                }
                catch (TimeoutException E)
                {
                    Log.Warn($"Could not close ServiceHost for the SolarEdgeService within {SolarEdgeServiceHost.CloseTimeout.TotalSeconds} seconds. Will abort service now.", E);
                    SolarEdgeServiceHost.Abort();
                }
                catch (Exception E)
                {

                    SolarEdgeServiceHost.Abort();
                    Log.Error("Could not deactivate WCF SolarEdgeService", E);
                    throw new Exception("Could not deactivate WCF SolarEdgeService", E);
                }
                finally
                {
                    SolarEdgeServiceHost = null;
                }
            }


            Log.Debug("ServiceHost for SolarEdgeService closed.");
        }

        /// <summary>
        /// Handles the SolarEdgeDataUpdated event of the DataFetcher.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Instance_SolarEdgeDataUpdated(object sender, EventArgs e)
        {
            SolarEdgeWCFService.SendBaseUpdates();
            SolarEdgeWCFService.SendFullDataUpdates();
        }


        /// <summary>
        /// Handles the SolarEdgeDataIsValidChanged event of the DataFetcher.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Instance_SolarEdgeDataIsValidChanged(object sender, EventArgs e)
        {
            SolarEdgeWCFService.SendDataIsValidUpdate(DataFetcher.Instance.SolarEdgeDataIsValid);
        }
    }
}
