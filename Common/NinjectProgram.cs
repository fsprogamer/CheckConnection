using Ninject;

namespace Common
{
    public class NinjectProgram
    {
        /// <summary>
        /// Gets the inject kernal for the program.
        /// </summary>
        public static IKernel Kernel { get; protected set; }
    }
}
