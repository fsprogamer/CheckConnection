using System.Activities;
using System.Windows.Forms;
using log4net;

namespace WorkflowLib
{
    public sealed class ShowLog : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> In { get; set; }

        public InOutArgument<string[]> InOut { get; set; }

        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.In);

            string[] arrtext = context.GetValue(this.InOut);

            System.Array.Resize(ref arrtext, arrtext.Length + 1);
            arrtext[arrtext.Length - 1] = "new string";

            context.SetValue(InOut, arrtext);
            //LogForm logform = new LogForm(text);
            //logform.ShowDialog();
        }
    }
}
