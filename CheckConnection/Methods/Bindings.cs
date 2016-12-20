using Ninject.Modules;
using log4net;

namespace CheckConnection.Methods
{

    public class Bindings : NinjectModule
    {
        public override void Load()
        {            
            Bind<IWMIConnectionManager>().To<WMIConnectionManager>();
            Bind<IConnectionManager>().To<ConnectionManager>();
            Bind<IDNSManager>().To<DNSManager>();
            Bind<IGatewayManager>().To<GatewayManager>();

            Bind<ILog>().ToMethod(context =>
             LogManager.GetLogger(context.Request.ParentContext == null ?
                                  typeof(object) :
                                  context.Request.ParentContext.Plan.Type));
        }
    }
}
