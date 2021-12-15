using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.DTO;
using BLL.Infrostructures;
using DAL;

namespace BLL
{
    public class ReportService : IReportService
    {
        IRepository<DAL.Entity.Group> _repo;

        public ReportService(IRepository<DAL.Entity.Group> repo)
        {
            _repo = repo;
        }
        public void AddReport(ReportDto item)
        {
            var dalReport = new DAL.Entity.Group { GroupName = item.Text, StudentsId = item.PersonId};
            _repo.Create(dalReport);
            item.Id = dalReport.Id;
        }

        public void ChangeReport(ReportDto item)
        {
            var dalReport = new DAL.Entity.Group { Id = item.Id, GroupName = item.Text, StudentsId = item.PersonId};
            _repo.Change(dalReport);
        }

        public void DeleteReport(int Id)
        {
            _repo.Delete(Id);
        }

        public IEnumerable<ReportDto> Get()
        {
            return _repo.GetAll().Select(t => new ReportDto { Id = t.Id, Text = t.GroupName, PersonId = t.StudentsId});
        }
    }
}
