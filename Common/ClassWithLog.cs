using System;
using log4net;
using Ninject;

namespace Common
{
    public class ClassWithLog
    {
        protected readonly ILog log;
        protected ClassWithLog ()
        {
            log = Common.NinjectProgram.Kernel.Get<ILog>();            
        }
    }

    public class ClassWithLogger<T> where T : class
    {
        protected readonly ILog log;
        protected ClassWithLogger()
        {
          ILogCreator logCreator = new LogCreator();          
          log = logCreator.GetTypeLogger<T>();
        }
    }
}
