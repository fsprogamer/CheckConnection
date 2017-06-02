using Ninject.Modules;
using log4net;
using Common;
using CheckConnection.Methods;

namespace CheckConnectionWpf.Methods
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {            
            Bind<IWMIConnectionManager>().To<WMIConnectionManager>();
            Bind<IWMINetworkAdapterManager>().To<WMINetworkAdapterManager>();
            Bind<IConnectionManager>().To<ConnectionManager>();
            Bind<IDNSManager>().To<DNSManager>();
            Bind<IGatewayManager>().To<GatewayManager>();
            //Bind<IUserManager>().To<UserManager>();

            Bind<ILog>().ToMethod(context =>
             LogManager.GetLogger(context.Request.ParentContext == null ?
                                  typeof(object) :
                                  context.Request.ParentContext.Plan.Type));
            Bind<ILogCreator>().To<LogCreator>();
        }
    }
}
