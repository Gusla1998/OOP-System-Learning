using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.DTO;
using BLL.Infrostructures;

namespace GUI
{
    public class ReportController
    {
        IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }
        public void Add(ReportViewModel item)
        {
            _service.AddReport(new BLL.DTO.ReportDto { Text = item.Text, PersonId = item.PersonId});
        }
        public void Delete(int Id)
        {
            _service.DeleteReport(Id);
        }
        public void Change(ReportViewModel item)
        {
            _service.ChangeReport(new BLL.DTO.ReportDto { Id = item.Id, Text = item.Text, PersonId = item.PersonId});
        }

        public IEnumerable<ReportViewModel> Show()
        {
            return _service.Get().Select(t => new ReportViewModel { Id = t.Id, Text = t.Text, PersonId = t.PersonId});
        }
    }
}
