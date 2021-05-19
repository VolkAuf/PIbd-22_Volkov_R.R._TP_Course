using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.ViewModels
{
    public class ReportDisciplineViewModel
    {
        public string StudentName { get; set; }

        public List<DisciplineViewModel> Disciplines { get; set; }
    }
}
