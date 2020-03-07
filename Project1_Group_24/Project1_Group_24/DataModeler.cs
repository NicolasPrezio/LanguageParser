using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;

namespace Project1_Group_24
{
    class DataModeler  
    {
        //properties
        Dictionary<string, List<CityInfo>> cityCollection = new Dictionary<string, List<CityInfo>>();
        List<CityInfo> cityList = new List<CityInfo>();

        //ParserMethods
        #region parsexml
        //ParseXML
        //iterates through the xml file and creates a new city object 
        //for each child in the file, then adds that city to the list of cities
        public void ParseXML(string filename)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filename);

                //iterate through each child in the document
                //get the values and create a new city object 
                //then add the city object to the dictionary
                foreach (XmlNode node in xmldoc.DocumentElement.ChildNodes)
                {
                    string cityid = node["id"].InnerText;
                    string cityname = node["city"].InnerText;
                    string cityascii = node["city_ascii"].InnerText;
                    string population = node["population"].InnerText;
                    string province = node["admin_name"].InnerText;
                    string latitude = node["lat"].InnerText;
                    string longitude = node["lng"].InnerText;

                    CityInfo city = new CityInfo(cityid, cityname, cityascii, population, province, latitude, longitude);
                    cityList.Add(city);
                }

                //add the city list to the dictionary
                cityCollection.Add("Cities", cityList);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region csvparse
        //CSVParse
        public void ParseCSV(string filename)
        {
            try
            {
                //Arrylist of Objects to holds the field data
                ArrayList holders = new ArrayList();
                //TextFieldParser parser = new TextFieldParser("D:\\Canadacities.csv");
                TextFieldParser parser = new TextFieldParser(filename);
                parser.SetDelimiters(",");//delimiters

                string[] cols;//String declaration to ReadFields();

                //While loop tp reach end of the data 
                while (!parser.EndOfData)
                {
                    cols = parser.ReadFields();
                    foreach (string col in cols)
                    {
                        holders.Add(col);//Loading array COL with CSV file Data per line
                    }
                    if (holders[8].ToString() != "id")
                    {
                        CityInfo city = new CityInfo(holders[8].ToString(), holders[0].ToString(), holders[1].ToString(), holders[7].ToString(), holders[5].ToString(), holders[2].ToString(), holders[3].ToString());
                        cityList.Add(city);
                    }
                    holders.Clear();//Clearing The Array 
                }
                //add the city list to the dictionary
                cityCollection.Add("Cities", cityList);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region jsonparse
        //JSONParse
        public void ParseJSON(string filename)
        {
            try
            {
                string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                string path = File.ReadAllText(wanted_path + "\\debug\\netcoreapp3.1\\" + filename);

                dynamic jResults = JsonConvert.DeserializeObject(path);

                // Loop through each object in jResults
                foreach (var cityInfo in jResults)
                {
                    // Create a new CityInfo object with the values from jResults
                    CityInfo city = new CityInfo((string)cityInfo.id, (string)cityInfo.city, (string)cityInfo.city_ascii, (string)cityInfo.population, (string)cityInfo.admin_name, (string)cityInfo.lat, (string)cityInfo.lng);

                    // Add the city to the List
                    cityList.Add(city);
                }

                //remove the last value in Javascript because it is a blank object
                if (cityList.Count == 251)
                    cityList.RemoveAt(250);

                // Add the List to the Dictionary
                cityCollection.Add("Cities", cityList);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion


        //ParseFile
        //creates a collection from the file that the user inputted
        public Dictionary<string, List<CityInfo>> ParseFile(string filename, string filetype)
        {
            switch (filetype)
            {
                case "xml":
                    ParseXML(filename);
                    return cityCollection;
                case "csv":
                    ParseCSV(filename);
                    return cityCollection;
                case "json":
                    ParseJSON(filename);
                    return cityCollection;
                default:
                    Console.WriteLine("Something went wrong");
                    return null;
            }
        }
    }
}
