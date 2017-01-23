using System.Activities;
using System.Windows.Forms;
using log4net;

namespace WorkflowLib
{
    public sealed class ShowMess : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }

        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
            log.InfoFormat("Text value: {0}", text);
            ShowMessForm userform = new ShowMessForm(text);

            if (userform.ShowDialog() == DialogResult.OK)
            {
            //this.Result.Set(context, userform.Checked);
            }
        }

    }
}
