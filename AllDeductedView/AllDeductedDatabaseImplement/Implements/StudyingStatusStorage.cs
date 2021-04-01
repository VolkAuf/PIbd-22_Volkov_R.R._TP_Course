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
                StudyingStatus element = context.StudyingStatuses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.StudyingStatuses.Remove(element);
                    context.SaveChanges();
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
                .ToList()
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
                StudyingStatus studyingStatus = new StudyingStatus
                {
                    StudentId = model.StudentId,
                    StudyingForm = model.StudyingForm,
                    StudyingBase = model.StudyingBase,
                    Course = model.Course
                };
                context.StudyingStatuses.Add(studyingStatus);
                context.SaveChanges();
                CreateModel(model, studyingStatus);
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
                element.StudentId = model.StudentId;
                element.StudyingForm = model.StudyingForm;
                element.StudyingBase = model.StudyingBase;
                element.Course = model.Course;
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        private StudyingStatus CreateModel(StudyingStatusBindingModel model, StudyingStatus studyingStatus)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Context())
            {
                Student element = context.Students.FirstOrDefault(rec => rec.Id == model.StudentId);
                if (element != null)
                {
                    if (element.StudyingStatus == null)
                    {
                        element.StudyingStatus = new StudyingStatus();
                    }
                    element.StudyingStatus = studyingStatus;
                    context.Students.Update(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
            return studyingStatus;
        }
    }
}
