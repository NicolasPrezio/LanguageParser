using System;
using System.Collections.Generic;
using System.Text;

namespace Project1_Group_24
{
    class CityInfo
    {
        //City object that will hold information for cities from the files *Req-1
        //*Nicolas

        //properties
        private string CityId { get; set; }
        private string CityName { get; set; }
        private string CityAscii { get; set; }
        private string Population { get; set; }
        private string Province { get; set; }
        private string Latitude { get; set; }
        private string Longitude { get; set; }

        //constructor
        public CityInfo(string id, string name, string ascii, string pop, string prov, string lat, string lon)
        {
            this.CityId = id;
            this.CityName = name;
            this.CityAscii = ascii;
            this.Population = pop;
            this.Province = prov;
            this.Latitude = lat;
            this.Longitude = lon;
        }

        //Methods:
        //GetName
        //returns the city name
        public string GetName()
        {
            return this.CityName;
        }

        //GetProvince
        //returns the province
        public string GetProvince()
        {
            return this.Province;
        }

        //GetPopulation
        //returns the population
        public string GetPopulation()
        {
            return this.Population;
        }

        //GetLocation
        //returns the Latitude and Longitude
        public string GetLocation()
        {
            return this.Latitude + " " + this.Longitude;
        }

        public double GetLongitude()
        {
            return Convert.ToDouble(this.Longitude);
        }
        public double GetLatitude()
        {
            return Convert.ToDouble(this.Latitude);
        }
    }
}
