using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using AllDeductedDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllDeductedDatabaseImplement.Implements
{
    public class ProviderStorage : IProviderStorage
    {
        private Provider CreateModel(ProviderBindingModel model, Provider provider, Context context)
        {
            User user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Login = model.Login,
                Password = model.Password,
                Mail = model.Mail,
            };

            context.Users.Add(user);
            context.SaveChanges();

            provider.UserId = user.Id;
            return provider;
        }

        public ProviderViewModel CreateViewModel(Provider provider)
        {
            return new ProviderViewModel
            {
                Id = provider.UserId,
                FirstName = provider.User.FirstName,
                LastName = provider.User.LastName,
                Login = provider.User.Login,
                Password = provider.User.Password,
                Mail = provider.User.Mail
            };
        }

        public void Delete(ProviderBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ProviderViewModel GetElement(ProviderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Context())
            {
                var provider = context.Providers
                    .Include(rec => rec.User)
                    .FirstOrDefault(rec => rec.User.Login == model.Login ||
                    rec.UserId == model.Id);

                return provider != null ? CreateViewModel(provider) : null;
            }
        }

        public List<ProviderViewModel> GetFilteredList(ProviderBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<ProviderViewModel> GetFullList()
        {
            using (var context = new Context())
            {
                return context.Providers.Include(rec => rec.User).Select(CreateViewModel).ToList();
            }
        }

        public void Insert(ProviderBindingModel model)
        {
            using (var context = new Context())
            {
                context.Providers.Add(CreateModel(model, new Provider(), context));
                context.SaveChanges();
            }
        }

        public void Update(ProviderBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
