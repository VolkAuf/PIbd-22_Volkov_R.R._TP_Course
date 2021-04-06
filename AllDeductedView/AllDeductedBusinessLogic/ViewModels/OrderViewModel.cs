using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AllDeductedBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int,string> Students { get; set; }

        public Dictionary<int, string> Groups { get; set; }
    }
}
