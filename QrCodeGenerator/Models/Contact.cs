using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace QrCodeGenerator.Models;
public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Emails { get; set; } = new List<string>();
    public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
    
    public string QrCode { get; set; }
    public List<string>? SocialMediaLinks { get; set; } = new List<string>();

}
