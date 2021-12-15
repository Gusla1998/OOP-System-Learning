using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.DTO;
using BLL.Infrostructures;
using DAL;

namespace BLL
{
    public class PersonService : IPersonService
    {
        IRepository<DAL.Entity.Person> _repo;

        public PersonService(IRepository<DAL.Entity.Person> repo)
        {
            _repo = repo;
        }
        public void AddPerson(PersonDto item)
        {
            var dalPerson = new DAL.Entity.Person { Name = item.Name, Group = item.Boss};
            _repo.Create(dalPerson);
            item.Id = dalPerson.Id;
        }

        public void ChangePerson(PersonDto item)
        {
            var dalPerson = new DAL.Entity.Person { Id = item.Id, Name = item.Name, Group = item.Boss };
            _repo.Change(dalPerson);
        }

        public void DeletePerson(int Id)
        {
            _repo.Delete(Id);
        }

        public IEnumerable<PersonDto> Get()
        {
            return _repo.GetAll().Select(t => new PersonDto { Id = t.Id, Name = t.Name, Boss = t.Group });
        }
    }
}
