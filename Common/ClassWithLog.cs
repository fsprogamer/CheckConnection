using log4net;
using Ninject;

namespace Common
{
    public abstract class ClassWithLog
    {
        protected readonly ILog log;

        protected ClassWithLog ()
        {
            log = Common.NinjectProgram.Kernel.Get<ILog>();
        }
    }
}
