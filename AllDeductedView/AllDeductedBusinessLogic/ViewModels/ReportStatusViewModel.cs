using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.ViewModels
{
    public class ReportStatusViewModel
    {
        public string ThreadName { get; set; }

        public List<StudyingStatusViewModel> Status { get; set; }
    }
}
