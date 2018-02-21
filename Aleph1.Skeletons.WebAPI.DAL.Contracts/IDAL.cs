using Aleph1.Skeletons.WebAPI.Models;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.DAL.Contracts
{
    public interface IDAL
    {
        IQueryable<Person> GetPersons();
        Person GetPersonByID(int ID);
        Person InsertPerson(Person person);
    }
}
