using System.Activities;
using log4net;
using System;

namespace WorkflowLib
{
    public sealed class ShowLog : NativeActivity<string>
    {
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

            context.CreateBookmark(name/*, new BookmarkCallback(OnReadComplete)*/);
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
