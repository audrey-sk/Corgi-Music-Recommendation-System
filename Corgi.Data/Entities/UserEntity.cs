namespace Corgi.Data.Entities;

public class UserEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DisplayName { get; set; } = string.Empty;
    public double HomeLatitude { get; set; }
    public double HomeLongitude { get; set; }
    public string CountryCode { get; set; } = "US";
}
