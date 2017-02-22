using System;


namespace WorkflowLib.Methods
{
    public class StopWorkflowException : Exception
    {
        public StopWorkflowException() : base() { }
        public StopWorkflowException(string message) : base(message) { }
    }

}
