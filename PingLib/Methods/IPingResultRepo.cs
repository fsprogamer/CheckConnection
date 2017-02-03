using System.Net.NetworkInformation;

namespace PingLib.Methods
{
    public interface IPingResultRepo
    {
        PingReply GetPing(string destination);
    }
}
