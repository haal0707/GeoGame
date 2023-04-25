using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GeoGame.Models;
using Newtonsoft.Json;

namespace GeoGame.Services
{
    public class GeographyService : IGeographyService
    {
        private List<Country> _countries;
        private List<Country> _europeanCountries;
        private List<Country> _asianCountries;
        private List<Country> _africanCountries;

        public GeographyService()
        {
            LoadDataFromApiAsync().Wait();
        }

        private async Task LoadDataFromApiAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://restcountries.com/v2/all");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                _countries = JsonConvert.DeserializeObject<List<Country>>(jsonString);
            }

            _europeanCountries = _countries.FindAll(c => c.region != null && c.region.Equals("Europe", StringComparison.OrdinalIgnoreCase));
            _asianCountries = _countries.FindAll(c => c.region != null && c.region.Equals("Asia", StringComparison.OrdinalIgnoreCase));
            _africanCountries = _countries.FindAll(c => c.region != null && c.region.Equals("Africa", StringComparison.OrdinalIgnoreCase));

        }


        public string GetRandomCountry()
        {
            var random = new Random();
            var index = random.Next(_countries.Count);
            return _countries[index].Name;
        }

        public string GetAlpha2Code(string country)
        {
            var selectedCountryCode = _countries.FirstOrDefault(c => c.alpha2code != null && c.Name.Equals(country, StringComparison.OrdinalIgnoreCase));
            return selectedCountryCode.alpha2code != null ? selectedCountryCode.alpha2code : null;
        }

        public string GetContinent(string country)
        {
            var selectedCountry = _countries.FirstOrDefault( c => c.region != null && c.Name.Equals(country, StringComparison.OrdinalIgnoreCase));

            if (selectedCountry.region == "Asia")
            {
                selectedCountry.region = "asia";
            }
            if (selectedCountry.region == "Europe")
            {
                selectedCountry.region = "europe";
            }
            if (selectedCountry.region == "Africa")
            {
                selectedCountry.region = "africa";
            }
            return selectedCountry.region != null ? selectedCountry.region : null;
        }

        public string GetCapital(string country)
        {
            var selectedCountry = _countries.FirstOrDefault(c => c.Name.Equals(country, StringComparison.OrdinalIgnoreCase));
            return selectedCountry != null ? selectedCountry.Capital : null;
        }

        public string GetRandomEuropeanCountry()
        {
            if (_europeanCountries.Count == 0)
            {
                return "Heter jag Elnour";
            }
            var random = new Random();
            var index = random.Next(_europeanCountries.Count);
            return _europeanCountries[index].Name;
        }
   
        public string GetEuropeanCapital(string country)
        {
            var selectedCountry = _europeanCountries.FirstOrDefault(c => c.Name.Equals(country, StringComparison.OrdinalIgnoreCase));
            return selectedCountry != null ? selectedCountry.Capital : null;
        }

        public string GetRandomAsianCountry()
        {
            var random = new Random();
            var index = random.Next(_asianCountries.Count);
            return _asianCountries[index].Name;
        }

        public string GetAsianCapital(string country)
        {
            var selectedCountry = _asianCountries.FirstOrDefault(c => c.Name.Equals(country, StringComparison.OrdinalIgnoreCase));
            return selectedCountry != null ? selectedCountry.Capital : null;
        }

        public string GetRandomAfricanCountry()
        {
            var random = new Random();
            var index = random.Next(_africanCountries.Count);
            return _africanCountries[index].Name;
        }

        public string GetAfricanCapital(string country)
        {
            var selectedCountry = _africanCountries.FirstOrDefault(c => c.Name.Equals(country, StringComparison.OrdinalIgnoreCase));
            return selectedCountry != null ? selectedCountry.Capital : null;
        }
    }
}