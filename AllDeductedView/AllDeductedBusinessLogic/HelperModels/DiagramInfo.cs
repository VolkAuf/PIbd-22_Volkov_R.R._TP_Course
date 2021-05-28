using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.HelperModels
{
    public class DiagramInfo
    {
        public List<StudyingStatusViewModel> Statuses { get; set; }

        public List<Tuple<string, int>> CountForm { get; set; }

        public List<Tuple<string, int>> CountBase { get; set; }
    }
}
