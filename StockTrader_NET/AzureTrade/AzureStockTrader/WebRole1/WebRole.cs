//  .NET StockTrader Sample WCF Application for Benchmarking, Performance Analysis and Design Considerations for Service-Oriented Applications
//                   4/10/2011: Updated to version 5.0, with notable enhancements for optional Windows Azure hosting, cross-browser and mobile-browser compatibility, and 
//                   new performance ehancements  See: 
//                                  1. Technical overview paper: http://download.microsoft.com/download/7/C/9/7C9F7B89-8AF0-4433-AB3A-B615C8EF9484/Trade5Overview.pdf
//                                  2. MSDN Site with downloads, additional information: http://msdn.microsoft.com/stocktrader
//                                  3. Discussion Forum: http://social.msdn.microsoft.com/Forums/en-US/dotnetstocktradersampleapplication
//                                  4. Live on Windows Azure: https://azurestocktrader.cloudapp.net
//                                   
//
//  Configuration Service 5 Notes:
//                      The application implements Configuration Service 5.0, for which the source code also ships in the sample. However, the .NET StockTrader 5
//                      sample is a general-purpose performance sample application for Windows Server and Windows Azure even if you are not implementing the Configuration Service. 
//                      
//

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Threading;
using System.Diagnostics;

namespace Trade_WebWebRole
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            //This thread is started to ensure the role entry class remains available for role event handling and shutdown. The same
            //logic could be embedded in the override Run() method.  When running a Web Role using Full IIS (which is typical and mostly desired),
            //the Role Entry class (this class) is in a different process space than the IIS process itself. We are working around the fact that
            //Windows Azure does not as of this code release, stop the IIS service first when a Web Role is shutdown--it stops the role, and shutsdown the
            //instance, but global.asax Application Stop is not called, and Configuration Service has no way to shutdown the running node, notifying its peers that it
            //is no longer active, and removing itself from the active hosts tables in the repository. 
            //This is a convenient workaround, that essentially ensures if a Web Role is being restarted, or stopped based on a topology change (scale down, for example),
            //IIS is properly stopped, and the lifecycle is completed with Application_End (global.asax) fired. Note we capture the OnChanging event AND the
            //Stopping event. The stopping event logic here, I believe (but not yet sure), may be superfulous. The problem with issuing commands on the OnStopping event or OnStop method is (I believe)
            //that the node has already had its IPs pulled out from it by Windows Azure, and network calls to peers (and DB calls) are impossible at this point.  At any rate, this 
            //workaround seems to work well.  The StartupTask.exe runs in an elevated priveledge, which is required to issue a Net Stop W3SVC command.  Hence, use of
            //local semaphore to trigger the shutdown from the StartUpTask.exe, which is always waiting for this trigger on a background thread and runs with elevated
            //privledges, unlike this logic and IIS itself.  StartupTask.exe also
            //does some other important but not required tasks--setting the IIS worker pools to highest performance settings possible, and turning off
            //app pool recycling.  By default, otherwise, app pools are recycled every 20 minutes.  A properly designed Web app running under IIS should
            //never have to be recycled in such a manner.  Its just pure overhead, slowing app node down every 20 minutes.  So for gosh sakes, lets turn it off.
            //None of this logic is required in a Worker Role, since there is not a separate IIS process space involved.
            Thread t = new Thread(new ThreadStart(WorkerRun));
            t.Start();
            return base.OnStart();
        }

        public override void Run()
        {
            base.Run();
        }

        //Event Handler
        public void RoleInstanceShutdown(object sender, RoleEnvironmentStoppingEventArgs e)
        {
            const string semaphoreName = "SemaphoreShutDown";
            Semaphore sem = null;

            // Attempt to open the named semaphore.
            try
            {
                sem = Semaphore.OpenExisting(semaphoreName);
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                return;
            }
            sem.Release(1);
            Thread.Sleep(15000);
        }

        //Event Handler
        public void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            var changes = from ch in e.Changes.OfType<RoleEnvironmentTopologyChange>()
                          select ch;
            if (changes.Any())
            {
                const string semaphoreName = "SemaphoreShutDown";
                Semaphore sem = null;

                // Attempt to open the named semaphore.
                try
                {
                    sem = Semaphore.OpenExisting(semaphoreName);
                }
                catch (WaitHandleCannotBeOpenedException)
                {
                    return;
                }
                sem.Release(1);

                //Note this may be superfulous, as this event may be running on a non-blocking thread.  To be investigated.
                //If a blocking thread, this gives some time for IIS to complete its shutdown, based on the semaphore release
                //above.  
                Thread.Sleep(20000);
            }
        }

        //Override of OnStop.
        public override void OnStop()
        {
            base.OnStop();
        }


        //Background thread, always active.
        public void WorkerRun()
        {
            //Wire up our key event handlers, to ensure proper lifecycle management of the IIS app pool.
            RoleEnvironment.Stopping += RoleInstanceShutdown;
            RoleEnvironment.Changing += RoleEnvironmentChanging;
            while (true)
            {
                Thread.Sleep(10000);
            }
        }
    }
}
