using System.Activities;
using System.Windows.Forms;

namespace WorkflowLib
{

    public sealed class ShowInt : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<int> Value { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string text = "Результат операции:"+context.GetValue(this.Value).ToString();
            ShowMessForm userform = new ShowMessForm(text);
            if (userform.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}
