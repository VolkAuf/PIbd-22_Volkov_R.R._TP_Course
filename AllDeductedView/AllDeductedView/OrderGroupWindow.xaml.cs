using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using Unity;

namespace AllDeductedView
{
    /// <summary>
    /// Логика взаимодействия для OrderGroupWindow.xaml
    /// </summary>
    public partial class OrderGroupWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly GroupLogic logicG;
        private readonly OrderLogic logic;
        private OrderViewModel oView;
        private Dictionary<int, string> linkGroup;
        private Dictionary<int, string> Students;
        
        public OrderGroupWindow(GroupLogic logicG, OrderLogic logicO)
        {
            InitializeComponent();
            this.logic = logicO;
            this.logicG = logicG;
        }
        private void OrderGroupWindow_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = logic.Read(null);
                comboBoxOrderGroup.ItemsSource = list;
                comboBoxOrderGroup.SelectedItem = null;
                var listG = logicG.Read(null);
                listBoxGroup.ItemsSource = listG;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReloadList()
        {
            listBoxLinkGroup.Items.Clear();
            foreach (var lg in linkGroup)
            {
                listBoxLinkGroup.Items.Add(new GroupViewModel { Id = lg.Key, Name = lg.Value });
            }
        }

        private void LoadData()
        {
            try
            {
                OrderViewModel view = logic.Read(new OrderBindingModel
                {
                    Id = (int)comboBoxOrderGroup.SelectedValue
                })?[0];
                if (view != null)
                {
                    oView = view;
                    linkGroup = view.Groups;
                    Students = view.Students;
                }
                else
                {
                    linkGroup = new Dictionary<int, string>();
                    Students = new Dictionary<int, string>();
                }
                ReloadList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxOrderGroup.SelectedValue != null)
            {
                if (!linkGroup.ContainsKey((int)listBoxGroup.SelectedValue))
                {
                    linkGroup.Add((int)listBoxGroup.SelectedValue, ((GroupViewModel)listBoxGroup.SelectedItem).Name);
                    ReloadList();
                }
            }
            else
            {
                MessageBox.Show("Выберите приказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxOrderGroup.SelectedValue != null)
            {
                if (listBoxLinkGroup.SelectedItems.Count == 1)
                {
                    MessageBoxResult result = (MessageBoxResult)MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            linkGroup.Remove((int)listBoxLinkGroup.SelectedValue);
                            ReloadList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите приказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxOrderGroup.SelectedValue == null)
            {
                MessageBox.Show("Выберите приказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdateOrder(new OrderBindingModel
                {
                    Id = oView.Id,
                    DateCreate = oView.DateCreate,
                    Groups = linkGroup,
                    Students = Students,
                    ProviderId = App.SelectProvider.Id
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadData();
        }
    }
}
