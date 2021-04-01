using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.Interfaces
{
    public interface IStudyingStatusStorage
    {
        List<StudyingStatusViewModel> GetFullList();
        List<StudyingStatusViewModel> GetFilteredList(StudyingStatusBindingModel model);
        StudyingStatusViewModel GetElement(StudyingStatusBindingModel model);
        void Insert(StudyingStatusBindingModel model);
        void Update(StudyingStatusBindingModel model);
        void Delete(StudyingStatusBindingModel model);
    }
}
