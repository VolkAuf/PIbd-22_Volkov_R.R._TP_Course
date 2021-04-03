using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.Interfaces
{
    public interface IProviderStorage
    {
        List<ProviderViewModel> GetFullList();

        List<ProviderViewModel> GetFilteredList(ProviderBindingModel model);

        ProviderViewModel GetElement(ProviderBindingModel model);

        void Insert(ProviderBindingModel model);

        void Update(ProviderBindingModel model);

        void Delete(ProviderBindingModel model);
    }
}
