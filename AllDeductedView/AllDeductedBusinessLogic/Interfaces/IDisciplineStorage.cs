using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.Interfaces
{
    public interface IDisciplineStorage
    {
        List<DisciplineViewModel> GetFullList();
        List<DisciplineViewModel> GetFilteredList(DisciplineBindingModel model);
        DisciplineViewModel GetElement(DisciplineBindingModel model);
    }
}
