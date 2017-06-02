using System.Linq;
using System.Management;
using System.Collections.Generic;
using System;

namespace CheckConnection.Methods
{
    public class WMIManagementObjectRepo : GenericWMIRepo<ManagementObject>, IWMIManagementObjectRepo
    {
        public int ret = 0;
        public WMIManagementObjectRepo(string scope, string query) : base(query, scope)
        {
            int ret = 0;
            log.InfoFormat("before WMIManagementObjectRepo constuctor, {0}", query);
            try
            {
                ManagementObjectSearcher moSearch = new ManagementObjectSearcher(scope, query);
                log.Info("after ManagementObjectSearcher");

                ManagementObjectCollection mo = moSearch.Get();
                if (mo.Count > 0)
                    Context = mo.Cast<ManagementObject>().ToList();
                log.InfoFormat("count: ", mo.Count);
                //Context = moSearch.Get();
                if (Context != null)
                    ret = Context.Count;
            }
            catch (Exception e)
            {
                log.Error("exception ManagementObjectSearcher ToList()", e);
            }

            log.Info("after WMIManagementObjectRepo constuctor");
        }

/*
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
                    System.Threading.Thread.Sleep(100);
                    log.InfoFormat("100");
                }

                if (Context != null)
                   ret = Context.Count;

                this.Reset();
            }
            catch (ManagementException e)
            {
                log.Error("Failed to run query: ", e);
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
                log.InfoFormat("obj.NewObject: " + obj.NewObject["Index"]);
            }
            catch (ManagementException e)
            {
                log.Error("Error: ",e);
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
        */
    }
}
