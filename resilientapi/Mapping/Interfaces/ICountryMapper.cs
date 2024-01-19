using Domain.DTOs;
using resilientapi.Models;

namespace resilientapi;

public interface ICountryMapper
{
    public CountryDto Map(Country country);
    Country Map(CountryDto country);
    List<Country> Map(List<CountryDto> countries);
}
