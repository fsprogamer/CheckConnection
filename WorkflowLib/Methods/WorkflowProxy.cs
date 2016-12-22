using CheckConnection.Methods;

namespace WorkflowLib
{
    public class WorkflowProxy
    {
        IWMIConnectionManager cmgr = new WMIConnectionManager();
        public int Count()
        {
            return cmgr.GetItems().Count;
        }
    }
}
