using AllDeductedBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AllDeductedBusinessLogic.ViewModels
{
    public class StudyingStatusViewModel
    {
        public int Id { get; set; }
        [DisplayName("Номер зачётной книги студента")]
        public int StudentId { get; set; }
        [DisplayName("Форма обучения")]
        public StudyingForm StudyingForm { get; set; }
        [DisplayName("Основа обучения")]
        public StudyingBase StudyingBase { get; set; }
        [DisplayName("Курс")]
        public int Course { get; set; }
    }
}
