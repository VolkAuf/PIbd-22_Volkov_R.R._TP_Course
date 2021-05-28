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
    public class StudentStorage : IStudentStorage
    {
        public void Delete(StudentBindingModel model)
        {
            using (var context = new Context())
            {
                Student element = context.Students.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Students.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public StudentViewModel GetElement(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Context())
            {
                Student student = context.Students
                .FirstOrDefault(rec => rec.Id == model.Id);
                return student != null ?
                new StudentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Patronymic = student.Patronymic
                } :
                null;
            }
        }

        public List<StudentViewModel> GetFilteredList(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Context())
            {
                return context.Students
                .Include(rec => rec.Thread)
                .ThenInclude(rec => rec.Disciplines)
                .Include(rec=> rec.Provider)
                .Where(rec => (rec.ProviderId == model.ProviderId))
                .ToList()
                .Select(rec => new StudentViewModel
                {
                    Id = rec.Id,
                    FirstName = rec.FirstName,
                    LastName = rec.LastName,
                    Patronymic = rec.Patronymic,
                    Disciplines = rec.Thread?.Disciplines.Select(rec => new DisciplineViewModel
                    { 
                        Id = rec.Id,
                        Name = rec.Name,
                        HoursCount = rec.HoursCount,
                    }).ToList(),
                })
                .ToList();
            }
        }

        public List<StudentViewModel> GetFullList()
        {
            using (var context = new Context())
            {
                return context.Students
                .Include(rec => rec.Thread)
                .ThenInclude(rec => rec.Disciplines)
                .Select(rec => new StudentViewModel
                {
                    Id = rec.Id,
                    FirstName = rec.FirstName,
                    LastName = rec.LastName,
                    Patronymic = rec.Patronymic,
                    Disciplines = rec.Thread.Disciplines.Select(rec => new DisciplineViewModel
                    {
                        Id = rec.Id,
                        Name = rec.Name,
                        HoursCount = rec.HoursCount,
                    }).ToList(),
                })
                .ToList();
            }
        }

        public void Insert(StudentBindingModel model)
        {
            using (var context = new Context())
            {
                context.Add(CreateModel(model, new Student()));
                context.SaveChanges();
            }
        }

        public void Update(StudentBindingModel model)
        {
            using (var context = new Context())
            {
                Student element = context.Students.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        private Student CreateModel(StudentBindingModel model, Student student)
        {
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Patronymic = model.Patronymic;
            student.ProviderId = model.ProviderId;

            return student;
        }
    }
}
