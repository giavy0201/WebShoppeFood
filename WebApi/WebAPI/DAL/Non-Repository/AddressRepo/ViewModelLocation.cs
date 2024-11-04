namespace DAL.Non_Repository.AddressRepo
{
    public class ViewModelLocation
    {
        public string? Ward { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Address
        {
            get
            {
                var address = new List<string>();

                if (!string.IsNullOrEmpty(Ward))
                {
                    address.Add(Ward);
                }
                if (!string.IsNullOrEmpty(District))
                {
                    address.Add(District);
                }

                if (!string.IsNullOrEmpty(City))
                {
                    address.Add(City);
                }

                if (address.Count > 0)
                {
                    return string.Join(" , ", address);
                }

                return null;
            }
        }
    }
}
