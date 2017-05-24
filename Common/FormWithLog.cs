using log4net;
using Ninject;

namespace Common
{
    public class FormWithLog:BaseForm
    {
        protected readonly ILog log;

        protected FormWithLog()
        {
            log = Common.IocKernel.Get<ILog>();
        }
    }

    public class FormWithLogger<T> : BaseForm where T : class
    {
        protected readonly ILog log;
        protected FormWithLogger()
        {
            ILogCreator logCreator = IocKernel.Get<ILogCreator>();       
            log = logCreator.GetTypeLogger<T>();
        }
    }
}
