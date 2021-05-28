using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.ViewModels;
using AllDeductedDatabaseImplement.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace AllDeductedView
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    
    public class DataGridDate
    {
        public int Id { get; set; }

        public string LastName { get; set; }
    }
    public partial class OrderWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly OrderLogic logic;
        private readonly StudentLogic logicS;
        private Dictionary<int,string>  orderStudents;
        private readonly Logger logger;

        public int Id
        {
            set { id = value; }
        }

        private int? id;

        public OrderWindow(OrderLogic logic,StudentLogic logicS)
        {
            InitializeComponent();
            this.logic = logic;
            this.logicS = logicS;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void OrdersWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    OrderViewModel view = logic.Read(new OrderBindingModel { 
                        Id = id.Value,
                        ProviderId = App.SelectProvider.Id
                    })?[0];
                    if (view != null)
                    {
                        orderStudents = view.Students;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Ошибка загрузки данных : " + ex.Message);
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                orderStudents = new Dictionary<int,string>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (orderStudents != null)
                {
                    dataGridStudents.Items.Clear();
                    foreach (var orsru in orderStudents)
                    {
                        dataGridStudents.Items.Add(new DataGridDate
                        {
                            Id = orsru.Key,
                            LastName = orderStudents[orsru.Key]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка заполнения данными : " + ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<OrderStudentWindow>();
            if (form.ShowDialog().Value == true)
            {
                if (orderStudents.ContainsKey(form.Id))
                {
                    orderStudents[form.Id] = (form.StudentLastName);
                }
                else
                {
                    orderStudents.Add(form.Id, form.StudentLastName);
                }
                LoadData();
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (orderStudents == null)
            {
                MessageBox.Show("Заполните приказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdateOrder(new OrderBindingModel
                {
                    Id = id,
                    ProviderId = App.SelectProvider.Id,
                    Students = orderStudents
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка сохранения данных : " + ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void ButtonDell_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridStudents.SelectedItems.Count == 1)
            {
                MessageBoxResult result = (MessageBoxResult)MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = ((DataGridDate)dataGridStudents.SelectedItems[0]).Id;
                    try
                    {
                        orderStudents.Remove(id);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Ошибка удаления данных : " + ex.Message);
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }
    }
}
