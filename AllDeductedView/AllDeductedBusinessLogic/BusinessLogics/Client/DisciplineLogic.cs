using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.BusinessLogics
{
    public class DisciplineLogic
    {
        private readonly IDisciplineStorage _disciplineStorage;
        public DisciplineLogic(IDisciplineStorage disciplineStorage)
        {
            _disciplineStorage = disciplineStorage;
        }
        public List<DisciplineViewModel> Read(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return _disciplineStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DisciplineViewModel> { _disciplineStorage.GetElement(model) };
            }
            return _disciplineStorage.GetFilteredList(model);
        }
    }
}
