namespace CheckConnection.Model
{
    public class MediumType:INameEntity
    {
        public string Active { get; set; }
        public string Name { get; set; }
        public uint NdisPhysicalMediumType { get; set; }
    }
}
