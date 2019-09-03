using Newtonsoft.Json;
using System;

namespace TravelAlberta.Exercise.Domain
{
    public class PlacesToStay : IDomainBase
    {
        public long Id { get; set; }

        public int CategoryId { get; set; }

        public int CategoryTypeId { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string LocationDescription { get; set; }

        [JsonIgnore]
        public string Url { get; set; }

        public float HealthPoints { get; set; }

        public int GreenKeyRating { get; set; }

        public string Status { get; set; }

        public int RevisionNumber { get; set; }

        public DateTime OriginalPublishedDate { get; set; }

        public string LinkName { get; set; }

        public string ClientId { get; set; }

        public string Name { get; set; }

        public bool IsDataProvider { get; set; }

        public void Map(string[] rawLineAsArray)
        {
            if (rawLineAsArray.Length != 16)
                throw new ArgumentException($"Unable to map the domain object {this.GetType().ToString()}. Mapping input received incorrect number of strings in the array.");

            this.Id = this.ParseLong(rawLineAsArray[0]);
            this.CategoryId = this.ParseInt(rawLineAsArray[1]);
            this.CategoryTypeId = this.ParseInt(rawLineAsArray[2]);
            this.City = rawLineAsArray[3];
            this.Region = rawLineAsArray[4];
            this.LocationDescription = rawLineAsArray[5];
            this.Url = rawLineAsArray[6];
            this.HealthPoints = this.ParseFloat(rawLineAsArray[7]);
            this.GreenKeyRating = this.ParseInt(rawLineAsArray[8]);
            this.Status = rawLineAsArray[9];
            this.RevisionNumber = this.ParseInt(rawLineAsArray[10]);
            this.OriginalPublishedDate = this.ParseDateTime(rawLineAsArray[11]);
            this.LinkName = rawLineAsArray[12];
            this.ClientId = rawLineAsArray[13];
            this.Name = rawLineAsArray[14];
            this.IsDataProvider = ParseBoolean(rawLineAsArray[15]);
        }

        private int ParseInt(string rawValue)
        {
            int outputvalue = 0;

            //I don't care of it worked or not...if it's a junk value, it would result in a default value of 0
            int.TryParse(rawValue, out outputvalue);
            return outputvalue;
        }

        private float ParseFloat(string rawValue)
        {
            float outputvalue = 0f;

            //I don't care of it worked or not...if it's a junk value, it would result in a default value of 0
            float.TryParse(rawValue, out outputvalue);
            return outputvalue;
        }

        private long ParseLong(string rawValue)
        {
            long outputvalue = 0;

            //I don't care of it worked or not...if it's a junk value, it would result in a default value of 0
            long.TryParse(rawValue, out outputvalue);
            return outputvalue;
        }

        private bool ParseBoolean(string rawValue)
        {
            bool outputvalue = false;

            //I don't care of it worked or not...if it's a junk value, it would result in a default value of 0
            bool.TryParse(rawValue, out outputvalue);
            return outputvalue;
        }

        private DateTime ParseDateTime(string rawValue)
        {
            DateTime outputvalue = DateTime.MinValue;

            //I don't care of it worked or not...if it's a junk value, it would result in a default value of 0
            DateTime.TryParse(rawValue, out outputvalue);
            return outputvalue;
        }
    }
}
