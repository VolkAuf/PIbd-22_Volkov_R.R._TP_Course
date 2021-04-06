using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.ViewModels;
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
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly OrderLogic logic;

        public OrdersWindow(OrderLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void OrdersWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridOrders.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<OrderWindow>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOrders.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<OrderWindow>();
                int id = ((OrderViewModel)dataGridOrders.SelectedItems[0]).Id;
                form.Id = id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOrders.SelectedItems.Count == 1)
            {
                MessageBoxResult result = (MessageBoxResult)MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = ((OrderViewModel)dataGridOrders.SelectedItems[0]).Id;
                    try
                    {
                        logic.Delete(new OrderBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }
        private void ButtonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
