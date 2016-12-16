namespace CheckConnection.Model
{
    public class MediumType:INameEntity
    {
        public string ServiceName { get; set; }
        public string MACAddress { get; set; }
        public string AdapterType { get; set; }
        public string DeviceID { get; set; }
        public string Name { get; set; }
        public uint NdisPhysicalMediumType { get; set; }
    }
}
