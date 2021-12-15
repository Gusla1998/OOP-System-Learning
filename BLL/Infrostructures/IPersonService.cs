using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Infrostructures
{
    public interface IPersonService
    {
        void AddPerson(PersonDto item);
        void DeletePerson(int Id);
        void ChangePerson(PersonDto item);
        IEnumerable<PersonDto> Get();
    }
}
