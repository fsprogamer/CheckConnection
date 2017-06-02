using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace Common
{
    public class NinjectProgram
    {
        /// <summary>
        /// Gets the inject kernal for the program.
        /// </summary>
        public static IKernel Kernel { get; protected set; }
    }

    public static class IocKernel
    {
        private static StandardKernel _kernel;

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }
        public static T Get<T>(IParameter parameter)
        {
            return _kernel.Get<T>(parameter);
        }
        public static void Initialize(params INinjectModule[] modules)
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel(modules);
            }
        }
    }
}
