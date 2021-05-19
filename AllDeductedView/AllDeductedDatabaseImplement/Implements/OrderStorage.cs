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
    public class OrderStorage : IOrderStorage
    {
        public void Delete(OrderBindingModel model)
        {
            using (var context = new Context())
            {
                Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Context())
            {
                var order = context.Orders.Include(rec => rec.OrderStudents)
                    .ThenInclude(rec => rec.Student)
                    .Include(rec => rec.Provider)
                    .Include(rec => rec.OrderGroups)
                    .ThenInclude(rec => rec.Group)
                    .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    DateCreate = order.DateCreate,
                    Students = order.OrderStudents.ToDictionary(recPC => recPC.StudentId, recPC => recPC.Student?.LastName),
                    Groups = order.OrderGroups.ToDictionary(recOG => recOG.GroupId, recOG => recOG.Group?.Name)
                } : null;
            }
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Context())
            {
                return context.Orders
                .Include(rec => rec.OrderStudents)
                .ThenInclude(rec => rec.Student)
                .Include(rec => rec.OrderGroups)
                .ThenInclude(rec => rec.Group)
                .Where(rec => (rec.ProviderId == model.ProviderId))
                .ToList()
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    DateCreate = rec.DateCreate,
                    Students = rec.OrderStudents
                .ToDictionary(recPC => recPC.StudentId, recPC => recPC.Student?.LastName),
                    Groups = rec.OrderGroups
                .ToDictionary(recPC => recPC.GroupId, recPC => recPC.Group?.Name),
                })
                .ToList();
            }
        }

        public List<OrderViewModel> GetFullList()
        {
            using (var context = new Context())
            {
                return context.Orders.Include(rec => rec.OrderStudents)
                    .ThenInclude(rec => rec.Student)
                    .Include(rec => rec.Provider)
                    .Include(rec => rec.OrderGroups)
                    .ThenInclude(rec => rec.Group)
                    .ToList()
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        DateCreate = rec.DateCreate,
                        Students = rec.OrderStudents.ToDictionary(recOS => recOS.StudentId, recPC => (recPC.Student?.LastName)),
                        Groups = rec.OrderGroups.ToDictionary(recOS => recOS.GroupId, recPC => (recPC.Group?.Name))
                    }).ToList();
            }
        }

        public void Insert(OrderBindingModel model)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = new Order
                        {
                            DateCreate = DateTime.Now,
                            ProviderId = model.ProviderId
                        };
                        context.Orders.Add(order);
                        context.SaveChanges();
                        CreateModel(model, order, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(OrderBindingModel model)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("элемент не найдена");
                        }
                        element.ProviderId = model.ProviderId;
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public Order CreateModel(OrderBindingModel model, Order order, Context context)
        {
            order.ProviderId = model.ProviderId;
            if(order.Id == 0)
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
            if (model.Id.HasValue)
            {
                    var orderStudent = context.OrderStudents.Where(rec => rec.OrderId == model.Id.Value).ToList();
                    // удалили те, которых нет в модели
                    context.OrderStudents.RemoveRange(orderStudent.Where(recOS => !model.Students.ContainsKey(recOS.StudentId)).ToList());
                    context.SaveChanges();

                    foreach (var os in orderStudent)
                    {
                        if (model.Students.ContainsKey(os.StudentId))
                        {
                            model.Students.Remove(os.StudentId);
                        }
                    }
                    context.SaveChanges();
                     var orderGroups = context.OrderGroups.Where(rec => rec.OrderId == model.Id.Value).ToList();
                    // удалили те, которых нет в модели
                    context.OrderGroups.RemoveRange(orderGroups.Where(recOS => !model.Groups.ContainsKey(recOS.GroupId)).ToList());
                    context.SaveChanges();

                    foreach (var og in orderGroups)
                    {
                        if (model.Groups.ContainsKey(og.GroupId))
                        {
                            model.Groups.Remove(og.GroupId);
                        }
                    }
                    context.SaveChanges();
                

                
            }
            // добавили новые
            if (model.Students != null) {
                foreach (var student in model.Students)
                {
                    context.OrderStudents.Add(new OrderStudent
                    {
                        OrderId = order.Id,
                        StudentId = student.Key
                    });
                    context.SaveChanges();
                }
            }
            
            if(model.Groups != null)
            {
                foreach (var group in model.Groups)
                {
                    context.OrderGroups.Add(new OrderGroup
                    {
                        OrderId = order.Id,
                        GroupId = group.Key
                    });
                    context.SaveChanges();
                }
            }
           

            return order;
        }
    }
}
