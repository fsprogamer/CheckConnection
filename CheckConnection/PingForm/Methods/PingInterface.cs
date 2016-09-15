using System.Net.NetworkInformation;

namespace PingForm.Methods
{
    public interface PingInterface
    {
        PingReply GetPing(string destination);
    }
}
