using System.Activities;
using System.Windows.Forms;
using log4net;
using System;

namespace WorkflowLib
{
    public sealed class ShowLog : NativeActivity<string>
    {
        // Define an activity input argument of type string
        //public InArgument<string> In { get; set; }

        //public InOutArgument<string[]> InOut { get; set; }

        readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }
        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(NativeActivityContext context)
        {

            string name = BookmarkName.Get(context);

            if (name == string.Empty)
            {
                throw new ArgumentException("BookmarkName cannot be an Empty string.",
                    "BookmarkName");
            }
            // Obtain the runtime value of the Text input argument
            //string text = context.GetValue(this.In);

            //string[] arrtext = context.GetValue(this.InOut);

            //System.Array.Resize(ref arrtext, arrtext.Length + 1);
            //arrtext[arrtext.Length - 1] = "new string";
            //context.SetValue(InOut, arrtext);

            //LogForm logform = new LogForm(text);
            //logform.ShowDialog();

            context.CreateBookmark(name, new BookmarkCallback(OnReadComplete));
        }
        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        void OnReadComplete(NativeActivityContext context, Bookmark bookmark, object state)
        {
            this.Result.Set(context, state.ToString());
        }
    }
}
