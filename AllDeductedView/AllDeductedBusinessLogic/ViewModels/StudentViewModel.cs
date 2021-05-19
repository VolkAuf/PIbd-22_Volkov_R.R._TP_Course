using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AllDeductedBusinessLogic.ViewModels
{
    public class StudentViewModel
    {
        [DisplayName("Зачётная книга")]
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        public List<DisciplineViewModel> Disciplines { get; set; }

        //public List<StudyingStatusViewModel> Statuses { get; set; }
    }
}
