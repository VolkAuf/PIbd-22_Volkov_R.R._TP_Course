using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.BusinessLogics
{
    public class ProviderLogic
    {
        private readonly IProviderStorage storage;

        public ProviderLogic(IProviderStorage storage)
        {
            this.storage = storage;
        }
        public List<ProviderViewModel> Read(ProviderBindingModel model)
        {
            if (model == null)
            {
                return storage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<ProviderViewModel> { storage.GetElement(model) };
            }
            return null;
        }

        public void CreateOrUpdate(ProviderBindingModel model)
        {
            var provider = storage.GetElement(
                new ProviderBindingModel
                {
                    Login = model.Login
                });

            if (provider != null && provider.Id != model.Id)
            {
                throw new Exception("Уже есть поставщик с таким логином");
            }

            if (model.Id.HasValue)
            {
                storage.Update(model);
            }
            else
            {
                storage.Insert(model);
            }
        }

        public void Delete(ProviderBindingModel model)
        {
            var customer = storage.GetElement(
                new ProviderBindingModel
                {
                    Id = model.Id
                });

            if (customer == null)
            {
                throw new Exception("поставщик не найден");
            }

            storage.Delete(model);
        }

        public bool Login(ProviderBindingModel model)
        {
            var provider = storage.GetElement(
                new ProviderBindingModel
                {
                    Login = model.Login,
                    Mail = model.Mail
                });

            if (provider == null || !provider.Password.Equals(model.Password))
            {
                throw new Exception("поставщик c такими данными не найден");
            }

            return true;
        }
    }
}
