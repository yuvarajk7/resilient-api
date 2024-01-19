using Domain.DTOs;
using resilientapi.Models;

namespace resilientapi;

public class CountryMapper : ICountryMapper
{
    public CountryDto Map(Country country)
    {
        return country != null ? new CountryDto
        {
            Id = country.Id,
            Name = country.Name,
            Description = country.Description,
            FlagUri = country.FlagUri,
        } : null;
    }

    public Country Map(CountryDto country)
    {
        return country != null ? new Country
        {
            Id = country.Id,
            Name = country.Name,
            Description = country.Description,
            FlagUri = country.FlagUri,
        } : null;
    }

    public List<Country> Map(List<CountryDto> countries)
    {
        return countries.Select(Map).ToList();
    }
}
