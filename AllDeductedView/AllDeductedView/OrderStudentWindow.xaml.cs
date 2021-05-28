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
    /// Логика взаимодействия для OrderStudentWindow.xaml
    /// </summary>
    public partial class OrderStudentWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public int Id { get => Convert.ToInt32(comboBoxStudent.SelectedValue); set => comboBoxStudent.SelectedValue = value; }
        public string StudentLastName => comboBoxStudent.Text;
        public OrderStudentWindow(StudentLogic logic)
        {
            InitializeComponent();
            List<StudentViewModel> list = logic.Read(new StudentBindingModel
            {
                ProviderId = App.SelectProvider.Id
            });
            if (list != null)
            {
                comboBoxStudent.DisplayMemberPath = "LastName";
                comboBoxStudent.SelectedValuePath = "Id";
                comboBoxStudent.ItemsSource = list;
                comboBoxStudent.SelectedItem = null;
            }
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxStudent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
