using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TravelAlberta.Exercise.Domain.Parser.Tests
{
    [TestClass()]
    public class CsvParserTests
    {
        private ICsvParser csvParser = new CsvParser();

        private string normalText = @"Id,CategoryId,CategoryTypeId,City,Region,LocationDescription,Url,HealthPoints,GreenKeyRating,Status,RevisionNumber,OriginalPublishedDate,LinkName,ClientId,Name,IsDataProvider
8106,1,23,Crowsnest Pass,Alberta,,http://alpenglowescapes.ca/,52,0,Published,4,2017-05-23T19:52:38.5623773Z,alpenglow-escapes,cde3645cc-31da-2b31-83d4-067c8f600bcf',Alpenglow Escapes,FALSE
7116,1,18,Crowsnest Pass,Alberta,,http://www.goatmountain.ca,59.75,0,Published,10,2016-12-20T23:51:02.5316109Z,goat-mountain-get-a-way,cc88ef27d-60ee-3715-4e44-53322ebf8b91,Goat Mountain Get-A-Way,FALSE
7189,1,18,Crowsnest Pass,Alberta,,http://adanacadventures.com/,51,0,Published,4,2017-01-27T16:37:46.2222531Z,adanac-adventures,c2e39fab1-fc4a-2be5-0d5f-671ab453e200',Adanac Adventures,FALSE
";

        private string newLineInBetween = @"Id,CategoryId,CategoryTypeId,City,Region,LocationDescription,Url,HealthPoints,GreenKeyRating,Status,RevisionNumber,OriginalPublishedDate,LinkName,ClientId,Name,IsDataProvider
8106,1,23,Crowsnest Pass,Alberta,,http://alpenglowescapes.ca/,52,0,Published,4,2017-05-23T19:52:38.5623773Z,alpenglow-escapes,cde3645cc-31da-2b31-83d4-067c8f600bcf',Alpenglow Escapes,FALSE
7116,1,18,Crowsnest Pass,Alberta,,http://www.goatmountain.ca,59.75,0,Published,10,2016-12-20T23:51:02.5316109Z,goat-mountain-get-a-way,cc88ef27d-60ee-3715-4e44-53322ebf8b91,Goat Mountain Get-A-Way,FALSE
7189,1,18,Crowsnest Pass,Alberta,,http://adanacadventures.com/,51,0,Published,4,2017-01-27T16:37:46.2222531Z,adanac-adventures,c2e39fab1-fc4a-2be5-0d5f-671ab453e200',Adanac Adventures,FALSE
7082,1,18,Pincher Creek,Alberta,\""45km southwest of Pincher Creek via Hwy507 and Hwy774. 

Right at the base of Castle Mountain Resort\"",http://www.staycastle.ca,31.25,0,Published,8,2016-12-24T03:10:17.4034609Z,castle-mountain-ski-lodge,cb1b40ee3-1a1a-6f68-31af-ed3c27053190,Castle Mountain Ski Lodge,FALSE";

        private string commaInBetween = @"Id,CategoryId,CategoryTypeId,City,Region,LocationDescription,Url,HealthPoints,GreenKeyRating,Status,RevisionNumber,OriginalPublishedDate,LinkName,ClientId,Name,IsDataProvider
8106,1,23,Crowsnest Pass,Alberta,\""this is a fake, test\"",http://alpenglowescapes.ca/,52,0,Published,4,2017-05-23T19:52:38.5623773Z,alpenglow-escapes,cde3645cc-31da-2b31-83d4-067c8f600bcf',Alpenglow Escapes,FALSE
7116,1,18,Crowsnest Pass,Alberta,,http://www.goatmountain.ca,59.75,0,Published,10,2016-12-20T23:51:02.5316109Z,goat-mountain-get-a-way,cc88ef27d-60ee-3715-4e44-53322ebf8b91,Goat Mountain Get-A-Way,FALSE
7189,1,18,Crowsnest Pass,Alberta,,http://adanacadventures.com/,51,0,Published,4,2017-01-27T16:37:46.2222531Z,adanac-adventures,c2e39fab1-fc4a-2be5-0d5f-671ab453e200',Adanac Adventures,FALSE";


        [TestMethod()]
        public void ParseNormalOne()
        {
            IEnumerable<IEnumerable<string>> parsedData = csvParser.Parse(normalText);

            Assert.IsNotNull(parsedData);
            int numerOfLinesParsed = 0;

            foreach (IEnumerable<string> innerLine in parsedData)
            {
                Assert.IsNotNull(innerLine);
                Assert.AreEqual(16, innerLine.Count());

                numerOfLinesParsed++;
            }

            Assert.AreEqual(4, numerOfLinesParsed);
        }

        [TestMethod()]
        public void ParseOneWithCommaInBetween()
        {
            IEnumerable<IEnumerable<string>> parsedData = csvParser.Parse(commaInBetween);

            Assert.IsNotNull(parsedData);
            int numerOfLinesParsed = 0;

            foreach (IEnumerable<string> innerLine in parsedData)
            {
                Assert.IsNotNull(innerLine);
                Assert.AreEqual(16, innerLine.Count());

                numerOfLinesParsed++;
            }

            Assert.AreEqual(4, numerOfLinesParsed);
        }

        [TestMethod()]
        public void ParseOneWithNewLineInBetween()
        {
            IEnumerable<IEnumerable<string>> parsedData = csvParser.Parse(newLineInBetween);

            Assert.IsNotNull(parsedData);
            int numerOfLinesParsed = 0;

            foreach (IEnumerable<string> innerLine in parsedData)
            {
                Assert.IsNotNull(innerLine);
                Assert.AreEqual(16, innerLine.Count());

                numerOfLinesParsed++;
            }

            Assert.AreEqual(5, numerOfLinesParsed);
        }
    }
}