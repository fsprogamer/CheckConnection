using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public interface IWMIServiceRepo : IGenericWMIRepo<Service>
    {
        WMIManagementObjectRepo mo_repo { get; set; }
    }
}
