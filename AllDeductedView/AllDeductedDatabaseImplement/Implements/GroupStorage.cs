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
    public class GroupStorage : IGroupStorage
    {
        public GroupViewModel GetElement(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Context())
            {
                var group = context.Groups.Include(rec => rec.OrderGroups)
                    .ThenInclude(rec => rec.Order)
                    .Include(rec => rec.Customer)
                    .FirstOrDefault(rec => rec.Id == model.Id);
                return group != null ?
                new GroupViewModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    CuratorName = group.CuratorName
                } : null;
            }
        }

        public List<GroupViewModel> GetFilteredList(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Context())
            {
                return context.Groups
                .Include(rec => rec.OrderGroups)
                .ThenInclude(rec => rec.Order)
                .Where(rec => rec.Id == model.Id)
                .ToList()
                .Select(rec => new GroupViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    CuratorName = rec.CuratorName
                })
                .ToList();
            }
        }
        public List<GroupViewModel> GetFullList()
        {
            using (var context = new Context())
            {
                return context.Groups.Include(rec => rec.OrderGroups)
                    .ThenInclude(rec => rec.Order)
                    .ToList()
                    .Select(rec => new GroupViewModel
                    {
                        Id = rec.Id,
                        Name = rec.Name,
                        CuratorName = rec.CuratorName
                    }).ToList();
            }
        }
    }
}
