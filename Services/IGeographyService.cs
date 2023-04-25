public interface IGeographyService
{
    string GetRandomCountry();
    string GetAlpha2Code(string country);
    string GetContinent(string country);
    string GetCapital(string country);
    string GetRandomEuropeanCountry();
    string GetEuropeanCapital(string country);
    string GetRandomAsianCountry();
    string GetAsianCapital(string country);
    string GetRandomAfricanCountry();
    string GetAfricanCapital(string country);
}