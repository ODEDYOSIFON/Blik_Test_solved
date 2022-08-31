using Blik_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blik_Test
{
    internal static class HelperMethods
    {
        private static readonly Random _rnd = new();

        internal static List<Hobby> GenerateHobbiesList(List<Hobby> hobbies)
        {
            var result = new List<Hobby>();

            int numberOfHobbies = _rnd.Next(2, 4);

            for (int i = 0; i < numberOfHobbies; i++)
            {
                int index = _rnd.Next(0, 5);
                while (result.Contains(hobbies[index]))
                {
                    index = _rnd.Next(0, 5);
                }

                result.Add(hobbies[index]);
            }

            return result;
        }
        /// <summary>
        /// gets list of persons and filter it by hobby name
        /// </summary>
        /// <param name="personsList"> the persons list to be filtered</param>
        /// <param name="hobbyName"> the hobby Name  to filter with</param>
        /// <returns></returns>
        internal static List<Person> FilterPersonsBySingleHobby(List<Person> personsList, string hobbyName)
        {
            return new List<Person>(from per in personsList
                                    where (per.Hobbies.Any(h => h.Name.Equals(hobbyName)))
                                    select per);
        }

        /// <summary>
        /// gets list of persons and filter it by 2 hobby names
        /// </summary>
        /// <param name="personsList"> the persons list to be filtered</param>
        /// <param name="firstHobbyName"> the 1st hobby Name  to filter with</param>
        /// /// <param name="secondHobbyName"> the 2nd hobby Name  to filter with</param>
        /// <returns></returns>
        internal static List<Person> FilterPersonsByTwoHobbies(List<Person> personsList, string firstHobbyName, string secondHobbyName)
        {
            return new List<Person>(from per in personsList
                                    where (per.Hobbies.Any(h => h.Name.Equals(firstHobbyName)) && per.Hobbies.Any(h => h.Name.Equals(secondHobbyName)))
                                    select per);
        }

    }
}
