using System.Collections.Generic;
using System.Linq;

namespace TravelAlberta.Exercise.Domain.Parser
{
    public interface IDomainMapper<T> where T : IDomainBase, new()
    {
        List<T> Map(IEnumerable<IEnumerable<string>> lines);
    }

    public class DomainMapper<T> : IDomainMapper<T> where T : IDomainBase, new()
    {
        public List<T> Map(IEnumerable<IEnumerable<string>> lines)
        {
            ushort lineCounter = 0;
            List<T> listOfASpecificDomainObject = new List<T>();

            foreach (IEnumerable<string> line in lines)
            {
                T newT = this.ExtractInstanceFromRow(line);

                if (lineCounter != 0)
                    listOfASpecificDomainObject.Add(newT);

                lineCounter++;
            }

            return listOfASpecificDomainObject;
        }

        private T ExtractInstanceFromRow(IEnumerable<string> line)
        {
            //Convert to JSON Object format 
            T newT = new T();
            newT.Map(line.ToArray());
            return newT;
        }
    }
}
