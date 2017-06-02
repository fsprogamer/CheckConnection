using System;
using System.Activities;
using System.Windows.Forms;

namespace WorkflowLib
{
    public sealed class ReadInt : NativeActivity<int>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        [RequiredArgument]
        public InArgument<string[]> Question { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            string name = BookmarkName.Get(context);

            string[] question = Question.Get(context);

            if (name == string.Empty)
            {
                throw new ArgumentException("BookmarkName cannot be an Empty string.",
                    "BookmarkName");
            }

            MakeChoiceForm userform = new MakeChoiceForm(question);
            if (userform.ShowDialog() == DialogResult.OK)
            {
                this.Result.Set(context, userform.Checked);
            }

            //context.CreateBookmark(name, new BookmarkCallback(OnReadComplete));
        }

        // NativeActivity derived activities that do asynchronous operations by calling 
        // one of the CreateBookmark overloads defined on System.Activities.NativeActivityContext 
        // must override the CanInduceIdle property and return true.
        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        void OnReadComplete(NativeActivityContext context, Bookmark bookmark, object state)
        {
            //this.Result.Set(context, Convert.ToInt32(state));
        }
    }
}
