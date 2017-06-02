using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Windows.Forms;

namespace CheckConnection.Workflow.Methods
{
    public sealed class ReadInt : NativeActivity<int>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            string name = BookmarkName.Get(context);

            if (name == string.Empty)
            {
                throw new ArgumentException("BookmarkName cannot be an Empty string.",
                    "BookmarkName");
            }

            UserForm userform = new UserForm();
            if (userform.ShowDialog() == DialogResult.OK)
            {                
                this.Result.Set(context, userform.Checked);
            }

            //context.CreateBookmark(name, new BookmarkCallback(OnReadComplete));
        }

        // NativeActivity derived activities that do asynchronous operations by calling 
        // one of the CreateBookmark overloads defined on System.Activities.NativeActivityContext 
        // must override the CanInduceIdle property and return true.
        //protected override bool CanInduceIdle
        //{
        //    get { return true; }
        //}

        //void OnReadComplete(NativeActivityContext context, Bookmark bookmark, object state)
        //{
        //    this.Result.Set(context, Convert.ToInt32(state));
        //}
    }
}
