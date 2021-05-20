using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.BusinessLogics
{
    public class StudyingStatusLogic
    {
        private readonly IStudyingStatusStorage _studyingStatusStorage;
        public StudyingStatusLogic(IStudyingStatusStorage studyingStatusStorage)
        {
            _studyingStatusStorage = studyingStatusStorage;
        }
        public List<StudyingStatusViewModel> Read(StudyingStatusBindingModel model)
        {
            if (model == null)
            {
                return _studyingStatusStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<StudyingStatusViewModel> { _studyingStatusStorage.GetElement(model) };
            }
            return _studyingStatusStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(StudyingStatusBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _studyingStatusStorage.Update(model);
            }
            else
            {
                _studyingStatusStorage.Insert(new StudyingStatusBindingModel
                { 
                    StudyingBase = model.StudyingBase,
                    StudyingForm = model.StudyingForm,
                    Course = model.Course,
                    DateCreate = DateTime.Now,
                    StudentId = model.StudentId,
                    ProviderId = model.ProviderId
                });
            }
        }
        public void Delete(StudyingStatusBindingModel model)
        {
            StudyingStatusViewModel element = _studyingStatusStorage.GetElement(new StudyingStatusBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _studyingStatusStorage.Delete(model);
        }
    }
}
