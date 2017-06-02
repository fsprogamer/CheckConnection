using log4net;

namespace Common
{
    public class ClassWithLog
    {
        protected readonly ILog log;
        protected ClassWithLog ()
        {
            log = IocKernel.Get<ILog>();            
        }
    }

    public class ClassWithLogger<T> where T : class
    {
        protected readonly ILog log;
        protected ClassWithLogger()
        {
          ILogCreator logCreator = IocKernel.Get<ILogCreator>();         
          log = logCreator.GetTypeLogger<T>();
        }
    }
}
