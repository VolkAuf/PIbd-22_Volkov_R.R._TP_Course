using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.BusinessLogics
{
    public class StudentLogic
    {
        private readonly IStudentStorage _studentStorage;
        public StudentLogic(IStudentStorage studentStorage)
        {
            _studentStorage = studentStorage;
        }
        public List<StudentViewModel> Read(StudentBindingModel model)
        {
            if (model == null)
            {
                return _studentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<StudentViewModel> { _studentStorage.GetElement(model) };
            }
            return _studentStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(StudentBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _studentStorage.Update(model);
            }
            else
            {
                _studentStorage.Insert(model);
            }
        }
        public void Delete(StudentBindingModel model)
        {
            StudentViewModel element = _studentStorage.GetElement(new StudentBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _studentStorage.Delete(model);
        }
    }
}
