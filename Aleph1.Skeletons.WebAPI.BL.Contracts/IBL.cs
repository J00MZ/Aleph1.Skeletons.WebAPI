using Aleph1.Skeletons.WebAPI.Models;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.BL.Contracts
{
    public interface IBL
    {
        IQueryable<Person> GetPersons();
        Person GetPersonByID(int ID);
        Person GetPersonByName(string firstName);
        Person InsertPerson(Person person);
    }
}
