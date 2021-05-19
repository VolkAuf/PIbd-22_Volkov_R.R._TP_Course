using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using AllDeductedDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AllDeductedDatabaseImplement.Implements
{
    public class ThreadStorage : IThreadStorage
    {
        private ThreadViewModel CreateViewModel(Thread thread)
        {
            return new ThreadViewModel
            {
                Id = thread.Id,
                Name = thread.Name,
                Faculty = thread.Faculty,
            };
        }

        public List<ThreadViewModel> GetFullList()
        {
            using (Context context = new Context())
            {
                return context.Threads
                    .Include(rec => rec.Students)
                    .ThenInclude(rec => rec.StudyingStatus)
                    .Select(rec => new ThreadViewModel
                    {
                        Faculty = rec.Faculty,
                        Name = rec.Name,
                        Statuses = rec.Students.Select(rec => new StudyingStatusViewModel
                        {
                            Id = rec.StudyingStatus.Id,
                            DateCreate = rec.StudyingStatus.DateCreate,
                            Course = rec.StudyingStatus.Course,
                            StudyingForm= rec.StudyingStatus.StudyingForm,
                            StudyingBase= rec.StudyingStatus.StudyingBase,
                            StudentId = rec.StudyingStatus.StudentId,
                        }).ToList(),
                    })
                    .ToList();
            }
        }

        public List<ThreadViewModel> GetFilteredList(ThreadBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (Context context = new Context())
            {
                return context.Threads
                    .Include(rec => rec.Students)
                    .ThenInclude(rec => rec.StudyingStatus)
                    /*.Where(rec => rec.Students.Select(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue) ||
                    (model.DateFrom.HasValue && model.DateTo.HasValue && rec.StudyingStatus.DateCreate.Date >= model.DateFrom.Value.Date && 
                    rec.StudyingStatus.DateCreate.Date <= model.DateTo.Value.Date)))*/
                    .Select(rec => new ThreadViewModel
                    {
                        Faculty = rec.Faculty,
                        Name = rec.Name,
                        Statuses = rec.Students.Select(rec => new StudyingStatusViewModel
                        {
                            Id = rec.StudyingStatus.Id,
                            DateCreate = rec.StudyingStatus.DateCreate,
                            Course = rec.StudyingStatus.Course,
                            StudyingForm = rec.StudyingStatus.StudyingForm,
                            StudyingBase = rec.StudyingStatus.StudyingBase,
                            StudentId = rec.StudyingStatus.StudentId,
                        }).ToList(),
                    })
                    .ToList();
            }
        }

        public ThreadViewModel GetElement(ThreadBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (Context context = new Context())
            {
                Thread thread = context.Threads
                    .Include(rec => rec.Students)
                    .ThenInclude(rec => rec.StudyingStatus)
                    .FirstOrDefault(rec => rec.Name == model.Name ||
                    rec.Id == model.Id);

                return thread != null ? CreateViewModel(thread) : null;
            }
        }
    }
}
