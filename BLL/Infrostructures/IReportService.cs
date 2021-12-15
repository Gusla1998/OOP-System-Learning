using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Infrostructures
{
    public interface IReportService
    {
        void AddReport(ReportDto item);
        void DeleteReport(int Id);
        void ChangeReport(ReportDto item);
        IEnumerable<ReportDto> Get();
    }
}
