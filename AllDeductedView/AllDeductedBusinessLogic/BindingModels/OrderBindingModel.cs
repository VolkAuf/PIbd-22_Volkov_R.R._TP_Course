using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }

        public DateTime DateCreate { get; set; }

        public List<int> Students { get; set; }

        public int ProviderId { get; set; }
    }
}
