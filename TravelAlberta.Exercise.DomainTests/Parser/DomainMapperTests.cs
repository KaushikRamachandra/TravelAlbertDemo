using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAlberta.Exercise.Domain.Parser.Tests
{
    [TestClass()]
    public class DomainMapperTests
    {
        private IDomainMapper<PlacesToStay> placeToStayMapper = new DomainMapper<PlacesToStay>();

        [TestMethod()]
        public void MapRandomTest()
        {
            IEnumerable<string> randomPlaceToStay = this.MakeRandomPlaceToStay();
            IEnumerable<IEnumerable<string>> data = new List<IEnumerable<string>>() { randomPlaceToStay, randomPlaceToStay };

            List<PlacesToStay> placesToStays = placeToStayMapper.Map(data);

            Assert.IsNotNull(placesToStays);
            Assert.AreEqual(1, placesToStays.Count);

            PlacesToStay toStay = placesToStays.Single();

            string[] arrayedPlaceToStay = randomPlaceToStay.ToArray();

            Assert.AreEqual(arrayedPlaceToStay[0], toStay.Id.ToString());
            Assert.AreEqual(arrayedPlaceToStay[1], toStay.CategoryId.ToString());
            Assert.AreEqual(arrayedPlaceToStay[2], toStay.CategoryTypeId.ToString());
            Assert.AreEqual(arrayedPlaceToStay[3], toStay.City);
            Assert.AreEqual(arrayedPlaceToStay[4], toStay.Region);
            Assert.AreEqual(arrayedPlaceToStay[5], toStay.LocationDescription);
            Assert.AreEqual(arrayedPlaceToStay[6], toStay.Url);
            Assert.AreEqual(arrayedPlaceToStay[7], toStay.HealthPoints.ToString());
            Assert.AreEqual(arrayedPlaceToStay[8], toStay.GreenKeyRating.ToString());
            Assert.AreEqual(arrayedPlaceToStay[9], toStay.Status);
            Assert.AreEqual(arrayedPlaceToStay[10], toStay.RevisionNumber.ToString());
            Assert.AreEqual(arrayedPlaceToStay[11], toStay.OriginalPublishedDate.ToString());
            Assert.AreEqual(arrayedPlaceToStay[12], toStay.LinkName);
            Assert.AreEqual(arrayedPlaceToStay[13], toStay.ClientId);
            Assert.AreEqual(arrayedPlaceToStay[14], toStay.Name);
            Assert.AreEqual(arrayedPlaceToStay[15], toStay.IsDataProvider.ToString());
        }

        [TestMethod()]
        public void MapTestForIgnoreHeaderRow()
        {
            IEnumerable<string> randomPlaceToStay = this.MakeRandomPlaceToStay();
            IEnumerable<IEnumerable<string>> data = new List<IEnumerable<string>>() { randomPlaceToStay };

            List<PlacesToStay> placesToStays = placeToStayMapper.Map(data);

            Assert.IsNotNull(placesToStays);
            Assert.AreEqual(0, placesToStays.Count);
        }

        [TestMethod()]
        public void MapRandomTestForDefaults()
        {
            IEnumerable<string> randomPlaceToStay = this.MakeRandomOutOfOrderPlaceToStay();
            IEnumerable<IEnumerable<string>> data = new List<IEnumerable<string>>() { randomPlaceToStay, randomPlaceToStay };

            List<PlacesToStay> placesToStays = placeToStayMapper.Map(data);

            Assert.IsNotNull(placesToStays);
            Assert.AreEqual(1, placesToStays.Count);

            PlacesToStay toStay = placesToStays.Single();

            string[] arrayedPlaceToStay = randomPlaceToStay.ToArray();

            Assert.AreEqual(0, toStay.Id);
            Assert.AreEqual(0, toStay.CategoryId);
            Assert.AreEqual(0, toStay.CategoryTypeId);
            Assert.AreEqual(arrayedPlaceToStay[3], toStay.City);
            Assert.AreEqual(arrayedPlaceToStay[4], toStay.Region);
            Assert.AreEqual(arrayedPlaceToStay[5], toStay.LocationDescription);
            Assert.AreEqual(arrayedPlaceToStay[6], toStay.Url);
            Assert.AreEqual(0f, toStay.HealthPoints);
            Assert.AreEqual(0, toStay.GreenKeyRating);
            Assert.AreEqual(arrayedPlaceToStay[9], toStay.Status);
            Assert.AreEqual(0, toStay.RevisionNumber);
            Assert.AreEqual(DateTime.MinValue, toStay.OriginalPublishedDate);
            Assert.AreEqual(arrayedPlaceToStay[12], toStay.LinkName);
            Assert.AreEqual(arrayedPlaceToStay[13], toStay.ClientId);
            Assert.AreEqual(arrayedPlaceToStay[14], toStay.Name);
            Assert.AreEqual(false, toStay.IsDataProvider);
        }

        [TestMethod(), ExpectedException(typeof(ArgumentException))]
        public void MapTestForIncorrectInput()
        {
            IEnumerable<string> randomPlaceToStay = new List<string>() { "Hoax", "InvalidInput" };
            IEnumerable<IEnumerable<string>> data = new List<IEnumerable<string>>() { randomPlaceToStay };

            List<PlacesToStay> placesToStays = placeToStayMapper.Map(data);
        }

        private IEnumerable<string> MakeRandomPlaceToStay()
        {
            Random random = new Random();

            List<string> lineData = new List<string>();
            lineData.Add(random.Next().ToString());
            lineData.Add(random.Next().ToString());
            lineData.Add(random.Next().ToString());
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
            lineData.Add($"http://{Guid.NewGuid().ToString()}.ca/");
            lineData.Add(this.NextFloat(random.NextDouble()).ToString());
            lineData.Add(random.Next().ToString());
            lineData.Add("Published");
            lineData.Add(random.Next().ToString());
            lineData.Add(new DateTime().ToString());
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add((random.NextDouble() >= 0.5).ToString());

            return lineData;
        }

        private IEnumerable<string> MakeRandomOutOfOrderPlaceToStay()
        {
            Random random = new Random();

            List<string> lineData = new List<string>();
            lineData.Add("NotANumber");
            lineData.Add("NotANumber");
            lineData.Add("NotANumber");
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
            lineData.Add($"http://{Guid.NewGuid().ToString()}.ca/");
            lineData.Add("NotANumber");
            lineData.Add("NotANumber");
            lineData.Add("Published");
            lineData.Add("NotANumber");
            lineData.Add("NotADate");
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add(Guid.NewGuid().ToString());
            lineData.Add("NotABool");

            return lineData;
        }

        private float NextFloat(double nextDouble)
        {
            return (float)(float.MaxValue * 2.0 * (nextDouble - 0.5));
        }
    }
}