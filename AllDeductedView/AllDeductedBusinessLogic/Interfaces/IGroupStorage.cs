using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.Interfaces
{
    public interface IGroupStorage
    {
        List<GroupViewModel> GetFullList();
        List<GroupViewModel> GetFilteredList(GroupBindingModel model);
        GroupViewModel GetElement(GroupBindingModel model);
    }
}
