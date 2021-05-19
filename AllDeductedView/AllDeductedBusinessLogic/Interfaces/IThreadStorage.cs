using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.Interfaces
{
    public interface IThreadStorage
    {
        List<ThreadViewModel> GetFullList();

        List<ThreadViewModel> GetFilteredList(ThreadBindingModel model);

        ThreadViewModel GetElement(ThreadBindingModel model);
    }
}
