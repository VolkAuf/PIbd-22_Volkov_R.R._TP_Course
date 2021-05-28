using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.ViewModels;
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
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id
        {
            set { id = value; }
        }

        private int? id;

        private readonly StudentLogic logicS;
        private readonly Logger logger;
        public StudentWindow(StudentLogic logicS)
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            this.logicS = logicS;
        }
        private void StudentWindow_Load(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                StudentViewModel student = logicS.Read(new StudentBindingModel
                {
                    Id = id,
                    ProviderId = App.SelectProvider.Id
                })?[0];

                textBoxFirstName.Text = student.FirstName;
                textBoxLastName.Text = student.LastName;
                textBoxPatronymic.Text = student.Patronymic;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFirstName.Text))
            {
                MessageBox.Show("Заполните поле FirstName", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxLastName.Text))
            {
                MessageBox.Show("Заполните поле LastName", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPatronymic.Text))
            {
                MessageBox.Show("Заполните поле Patronymic", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logicS.CreateOrUpdate(new StudentBindingModel
                {
                    Id = id,
                    FirstName = textBoxFirstName.Text.ToString(),
                    LastName = textBoxLastName.Text.ToString(),
                    Patronymic = textBoxPatronymic.Text.ToString(),
                    ProviderId = App.SelectProvider.Id
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка созранения данных : " + ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
