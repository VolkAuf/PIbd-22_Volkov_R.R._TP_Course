using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.BindingModels
{
    public class StudentBindingModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int ProviderId { get; set; }
    }
}
