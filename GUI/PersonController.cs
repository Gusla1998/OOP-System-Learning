using BLL.Infrostructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI
{
    public class PersonController
    {
        IPersonService _service;
        public PersonController(IPersonService service)
        {
            _service = service;
        }

        public void Add(PersonViewModel item)
        {
            _service.AddPerson(new BLL.DTO.PersonDto { Name = item.Name, Boss = item.Boss });
        }
        public void Change(PersonViewModel item)
        {
            _service.ChangePerson(new BLL.DTO.PersonDto { Id = item.Id, Name = item.Name, Boss = item.Boss });
        }
        public void Delete(int Id)
        {
            _service.DeletePerson(Id);
        }

        public IEnumerable<PersonViewModel> Show()
        {
            IEnumerable<PersonViewModel> list = _service.Get().Select(t => new PersonViewModel { Id = t.Id, Name = t.Name, Boss = t.Boss });

            PersonViewModel TeamLead = new PersonViewModel();  
            foreach (var person in list)
            {
                if (person.Boss == 0)
                {
                    TeamLead = person;
                    Console.WriteLine($"{TeamLead.Id} {TeamLead.Name}");
                }
            }
            
            List<PersonViewModel> leaders = new List<PersonViewModel>();
            foreach (var person in list)
            {
                if (person.Boss == TeamLead.Id)
                {
                    leaders.Add(person);
                }
            }

            foreach (var leader in leaders)
            {
                Console.WriteLine($"\t{leader.Id} {leader.Name}");
                foreach (var person in list)
                {
                    if (person.Boss == leader.Id)
                    {
                        Console.WriteLine($"\t\t{person.Id} {person.Name}");
                    }
                }
            }

            return list;
        }

        public IEnumerable<PersonViewModel> Show_Task(int Id, IReportService report_service)
        {
            IEnumerable<PersonViewModel> list = _service.Get().Select(t => new PersonViewModel { Id = t.Id, Name = t.Name, Boss = t.Boss });
            IEnumerable<ReportViewModel> list_report = report_service.Get().Select(t => new ReportViewModel { Id = t.Id, Text = t.Text, PersonId = t.PersonId });

            PersonViewModel TeamLead = new PersonViewModel();
            foreach (var person in list)
            {
                if (person.Boss == 0)
                {
                    TeamLead = person;
                    if (TeamLead.Id == Id)
                    {
                        Console.WriteLine($"{TeamLead.Name}");
                        foreach (var report in list_report)
                        {
                            if (report.PersonId == TeamLead.Id)
                            {
                                Console.WriteLine($"  {report.Id} {report.Text}");
                            }
                        }
                    }
                }
            }

            List<PersonViewModel> leaders = new List<PersonViewModel>();
            foreach (var person in list)
            {
                if (person.Boss == TeamLead.Id)
                {
                    leaders.Add(person);
                }
            }

            foreach (var leader in leaders)
            {
                if ((leader.Id == Id) || (leader.Boss == Id))
                {
                    Console.WriteLine($"\t{leader.Name}");
                    foreach (var report in list_report)
                    {
                        if (report.PersonId == leader.Id)
                        {
                            Console.WriteLine($"\t  {report.Id} {report.Text}");
                        }
                    }
                }
                foreach (var person in list)
                {
                    if (person.Boss == leader.Id)
                    {
                        if ((person.Boss == Id) || (leader.Boss == Id))
                        {
                            Console.WriteLine($"\t\t{person.Name}");
                            foreach (var report in list_report)
                            {
                                if (report.PersonId == person.Id)
                                {
                                    Console.WriteLine($"\t\t  {report.Id} {report.Text}");
                                }
                            }
                        }
                    }
                }
            }

            return list;
        }
    }
}
