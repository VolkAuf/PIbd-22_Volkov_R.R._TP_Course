using AllDeductedBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.BindingModels
{
    public class StudyingStatusBindingModel
    {
        public int? Id { get; set; }
        public int StudentId { get; set; }
        public DateTime DateCreate { get; set; }
        public StudyingForm StudyingForm { get; set; }
        public StudyingBase StudyingBase { get; set; }
        public int Course { get; set; }
        public int ProviderId { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
