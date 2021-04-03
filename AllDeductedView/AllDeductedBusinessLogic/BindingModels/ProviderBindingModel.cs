using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AllDeductedBusinessLogic.BindingModels
{
    public class ProviderBindingModel
    {
        public int? Id { get; set; }

        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Логин")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Почта")]
        public string Mail { get; set; }
    }
}
