using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AllDeductedBusinessLogic.ViewModels
{
    public class ThreadViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Факультет")]
        public string Faculty { get; set; }

        public List<StudyingStatusViewModel> Statuses { get; set; }
    }
}
