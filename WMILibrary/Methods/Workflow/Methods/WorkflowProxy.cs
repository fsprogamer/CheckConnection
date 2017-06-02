using CheckConnection.Methods;

namespace CheckConnection.Workflow.Methods
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
