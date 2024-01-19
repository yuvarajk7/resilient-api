using Microsoft.AspNetCore.Mvc;

namespace resilientapi.Models;

public class Address
{
    [FromRoute]
    public int StreetNumber { get; set; }

    [FromForm]
    public string StreetName { get; set; }

    [FromForm]
    public string StreetType { get; set; }

    [FromForm]
    public string City { get; set; }

    [FromForm]
    public string Country { get; set; }

    [FromForm]
    public int PostalCode { get; set; }
}
