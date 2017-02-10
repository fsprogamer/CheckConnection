using System.Activities;
using System.Windows.Forms;
using log4net;

using WorkflowLib.Methods;

namespace WorkflowLib
{

    public sealed class ShowMess : NativeActivity<string>//CodeActivity
    {
        StopWorkflowException stopex = new StopWorkflowException("Пользователь прервал операцию.");
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }

        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(NativeActivityContext/*CodeActivityContext */context)
        {
            DialogResult res;
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
            log.InfoFormat("Text value: {0}", text);
            ShowMessForm userform = new ShowMessForm(text);

            if ((res = userform.ShowDialog()) == DialogResult.OK)
            {
            //this.Result.Set(context, userform.Checked);
            }
            else
            if (res == DialogResult.Cancel)
                {
                 context.Abort(stopex);
                }
        }

    }
}
