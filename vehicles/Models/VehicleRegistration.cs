namespace vehicles.Models;

public class VehicleRegistration
{
    public string OwnerID { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Vehicle { get; set; } = string.Empty;
    public DateTime DateOfRegistration { get; set; }
}
