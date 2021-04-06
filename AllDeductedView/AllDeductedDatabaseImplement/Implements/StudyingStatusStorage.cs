using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using AllDeductedDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllDeductedDatabaseImplement.Implements
{
    public class StudyingStatusStorage : IStudyingStatusStorage
    {
        public void Delete(StudyingStatusBindingModel model)
        {
            using (var context = new Context())
            {
                StudyingStatus status = context.StudyingStatuses.FirstOrDefault(rec => rec.Id == model.Id);
                if (status != null)
                {
                    context.StudyingStatuses.Remove(status);
                    context.SaveChanges();
                }
                Student student = context.Students.FirstOrDefault(rec => rec.Id == model.StudentId);
                if (student != null)
                {
                    student.StudyingStatus = null;
                    context.Students.Update(student);
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public StudyingStatusViewModel GetElement(StudyingStatusBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Context())
            {
                StudyingStatus studyingStatus = context.StudyingStatuses
                .FirstOrDefault(rec => rec.Id == model.Id);
                return studyingStatus != null ?
                new StudyingStatusViewModel
                {
                    Id = studyingStatus.Id,
                    StudentId = studyingStatus.StudentId,
                    StudyingForm = studyingStatus.StudyingForm,
                    StudyingBase = studyingStatus.StudyingBase,
                    Course = studyingStatus.Course
                } :
                null;
            }
        }

        public List<StudyingStatusViewModel> GetFilteredList(StudyingStatusBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Context())
            {
                return context.StudyingStatuses
                .Select(rec => new StudyingStatusViewModel
                {
                    Id = rec.Id,
                    StudentId = rec.StudentId,
                    StudyingForm = rec.StudyingForm,
                    StudyingBase = rec.StudyingBase,
                    Course = rec.Course
                })
                .ToList();
            }
        }

        public List<StudyingStatusViewModel> GetFullList()
        {
            using (var context = new Context())
            {
                return context.StudyingStatuses
                .Select(rec => new StudyingStatusViewModel
                {
                    Id = rec.Id,
                    StudentId = rec.StudentId,
                    StudyingForm = rec.StudyingForm,
                    StudyingBase = rec.StudyingBase,
                    Course = rec.Course
                })
                .ToList();
            }
        }

        public void Insert(StudyingStatusBindingModel model)
        {
            using (var context = new Context())
            {
                context.Add(CreateModel(model, new StudyingStatus(), context));
                context.SaveChanges();
            }
        }

        public void Update(StudyingStatusBindingModel model)
        {
            using (var context = new Context())
            {
                StudyingStatus element = context.StudyingStatuses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                
                CreateModel(model, element, context);
                context.SaveChanges();
            }
        }

        private StudyingStatus CreateModel(StudyingStatusBindingModel model, StudyingStatus studyingStatus, Context context)
        {
            if (model == null)
            {
                return null;
            }

                Student element = context.Students.FirstOrDefault(rec => rec.Id == model.StudentId);
                if (element != null)
                {
                    if (element.StudyingStatus == null)
                    {
                        element.StudyingStatus = new StudyingStatus();
                    }
                    studyingStatus.StudyingBase = model.StudyingBase;
                    studyingStatus.StudyingForm = model.StudyingForm;
                    studyingStatus.ProviderId = model.ProviderId;
                    studyingStatus.Course = model.Course;
                    studyingStatus.StudentId = model.StudentId;
                    element.StudyingStatus = studyingStatus;
                    context.Students.Update(element);
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            return studyingStatus;
        }
    }
}
