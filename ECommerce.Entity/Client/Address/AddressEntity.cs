namespace ECommerce.Entity.Client.Address
{
    public class AddressEntity
    {
        public int Id { get; set; } = 0;
        public long UserId { get; set; } = 0;
        public string FullName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string AlternateNumber { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string Landmark { get; set; } = string.Empty;
        public int CityId { get; set; } = 0;
        public int StateId { get; set; } = 0;
        public string PinCode { get; set; } = string.Empty;
        public string AddressType { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false;
        public string StateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;

    }
    public class AddressGridEntity
    {
        public List<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();
        public int TotalRecords { get; set; }
    }

    public class AddressParameterEntity
    {
        public int Id { get; set; } = 0;
        public long UserId { get; set; } = 0;
    }

}
