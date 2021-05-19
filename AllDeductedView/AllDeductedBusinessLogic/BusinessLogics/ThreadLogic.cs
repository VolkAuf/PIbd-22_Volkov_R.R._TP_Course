using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.BusinessLogics
{
    public class ThreadLogic
    {
        private readonly IThreadStorage storage;

        public ThreadLogic(IThreadStorage ThreadStorage)
        {
            storage = ThreadStorage;
        }

        public List<ThreadViewModel> Read(ThreadBindingModel model)
        {
            if (model == null)
            {
                return storage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<ThreadViewModel> { storage.GetElement(model) };
            }

            return storage.GetFilteredList(model);
        }
    }
}
