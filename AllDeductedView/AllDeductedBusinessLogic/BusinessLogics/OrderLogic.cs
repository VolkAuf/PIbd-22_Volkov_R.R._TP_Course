using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.BusinessLogics
{
    public class OrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        public OrderLogic(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }
        public void CreateOrUpdateOrder(OrderBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _orderStorage.Update(model);
            }
            else
            {

                _orderStorage.Insert(new OrderBindingModel
                {
                    Students = model.Students,
                    ProviderId = model.ProviderId,
                    DateCreate = DateTime.Now
                });
            }
        }
        
        public void Delete(OrderBindingModel model)
        {
            OrderViewModel element = _orderStorage.GetElement(new OrderBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _orderStorage.Delete(model);
        }
    }
}
