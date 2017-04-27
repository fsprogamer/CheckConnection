namespace CheckConnection.Model
{
    public class MediumType:NameEntity
    {
        public string Active { get; set; }        
        public uint NdisPhysicalMediumType { get; set; }
    }
}
