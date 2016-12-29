using System.Linq;
using System.Management;
using System.Collections.Generic;
using System;

namespace CheckConnection.Methods
{
    public class WMIManagementObjectRepo : GenericWMIRepo<ManagementObject>, IWMIManagementObjectRepo
    {
        public int ret = 0;
        //public WMIManagementObjectRepo(string scope, string query) : base(query, scope)
        //{
        //    int ret = 0;
        //    log.Info("before WMIManagementObjectRepo constuctor");
        //    try
        //    {
        //        ManagementObjectSearcher moSearch = new ManagementObjectSearcher(scope, query);
        //        log.Info("after ManagementObjectSearcher");

        //        Context = moSearch.Get().Cast<ManagementObject>().ToList();

        //        //Context = moSearch.Get();
        //        if (Context != null)
        //            ret = Context.Count;
        //    }
        //    catch(Exception e)
        //    {
        //        log.Error("exception ManagementObjectSearcher ToList()", e);
        //    }            
            
        //    log.Info("after WMIManagementObjectRepo constuctor");
        //}


        public WMIManagementObjectRepo(string scope, string query) : base(query, scope)
        {
            log.Info("before WMIManagementObjectRepo constuctor");
            Context = new List<ManagementObject>(10);
            try
            {
                // Instantiate an object searcher with the query.
                ManagementObjectSearcher moSearch = new ManagementObjectSearcher(scope, query);

                // Create a results watcher object
                // and handler for results and completion.
                ManagementOperationObserver results = new
                    ManagementOperationObserver();

                // Attach handler to events for results and completion.
                results.ObjectReady += new
                    ObjectReadyEventHandler(this.NewObject);
                results.Completed += new
                    CompletedEventHandler(this.Done);

                // Call the asynchronous overload of Get()
                // to start the enumeration.
                moSearch.Get(results);

                // Do something else while results
                // arrive asynchronously.
                while (!this.Completed)
                {
                    System.Threading.Thread.Sleep(300);
                }

                if (Context != null)
                   ret = Context.Count;

                this.Reset();
            }
            catch (ManagementException e)
            {
                Console.WriteLine("Failed to run query: " + e.Message);
                throw;
            }

            log.Info("after WMIManagementObjectRepo constuctor");
        }

        private bool isCompleted = false;

        private void NewObject(object sender,
            ObjectReadyEventArgs obj)
        {
            try
            {
                Context.Add((ManagementObject)obj.NewObject);
            }
            catch (ManagementException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }

        private bool Completed
        {
            get
            {
                return isCompleted;
            }
        }

        private void Reset()
        {
            isCompleted = false;
        }

        private void Done(object sender,
                 CompletedEventArgs obj)
        {
            isCompleted = true;
        }
    }
}
