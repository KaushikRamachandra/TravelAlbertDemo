using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TravelAlberta.Exercise.Domain.Parser
{
    public interface ICsvParser
    {
        IEnumerable<IEnumerable<string>> Parse(string csvContent);
    }

    public class CsvParser : ICsvParser
    {
        private const char FieldDelimiter = ',';

        public IEnumerable<IEnumerable<string>> Parse(string csvContent)
        {
            StringReader reader = new StringReader(csvContent);

            while (reader.Peek() != -1)
                yield return parseLine(reader);
        }
        
        protected virtual IEnumerable<string> parseLine(TextReader reader)
        {
            bool insideQuotes = false;
            StringBuilder item = new StringBuilder();

            while (reader.Peek() != -1)
            {
                char ch = (char)reader.Read();
                char? nextCh = reader.Peek() > -1 ? (char)reader.Peek() : (char?)null;

                //This will save us when the text itself contains a comma..
                if (!insideQuotes && ch == FieldDelimiter)
                {
                    yield return item.ToString();
                    item.Length = 0;
                }
                //wish there was a way to use Environment.NewLine or an alternative instead of \r and \n ... well, i suppose this will not change.
                //So should be alright to hardcode
                //This will save us when the text itself contains a new line or carriage return - that way we won't end up with incoorect line endings
                else if (!insideQuotes && ch == '\r' && nextCh == '\n') //CRLF
                {
                    reader.Read(); // skip LF
                    break;
                }
                //Actual line break..
                else if (!insideQuotes && ch == '\n') //LF for *nix-style line endings
                    break;
                else if (ch == '"' && nextCh == '"') // escaped quotes ""
                {
                    item.Append('"');
                    reader.Read(); // skip next "
                }
                else if (ch == '"')
                    insideQuotes = !insideQuotes;
                else
                    item.Append(ch);
            }

            // last one
            yield return item.ToString();
        }

        
    }
}
