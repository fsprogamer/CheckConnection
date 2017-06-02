using System.Activities;
using System.Windows.Forms;
using log4net;

using WorkflowLib.Methods;

namespace WorkflowLib
{

    public sealed class ShowInt : NativeActivity
    {
        StopWorkflowException stopex = new StopWorkflowException("Пользователь прервал операцию.");
        // Define an activity input argument of type string
        public InArgument<int> Value { get; set; }

        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(NativeActivityContext context)
        {
            DialogResult res;
            // Obtain the runtime value of the Text input argument
            string text = "Результат операции:"+context.GetValue(this.Value).ToString();
            ShowMessForm userform = new ShowMessForm(text);
            log.InfoFormat("Int value: {0}", text);
            if ((res = userform.ShowDialog()) == DialogResult.OK)
            {
            }
            else
            if (res == DialogResult.Cancel)
            {
                context.Abort(stopex);
            }
        }
    }
}
