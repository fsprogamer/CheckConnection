using System.Windows.Forms;

using log4net;
using Ninject;

namespace Common
{
    public class FormWithLog:Form
    {
        protected readonly ILog log;

        protected FormWithLog()
        {
            log = Common.NinjectProgram.Kernel.Get<ILog>();
        }
    }
}
