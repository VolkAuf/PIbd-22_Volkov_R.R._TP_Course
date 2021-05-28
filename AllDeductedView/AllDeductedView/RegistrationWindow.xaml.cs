using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly ProviderLogic logic;
        private readonly Logger logger;
        private readonly int logPassLength = 50;
        public RegistrationWindow(ProviderLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFirstName.Text))
            {
                MessageBox.Show("Заполните \"Имя\"", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxLastName.Text))
            {
                MessageBox.Show("Заполните \"Фамилию\"", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxLogin.Text))
            {
                MessageBox.Show("Заполните поле \"Логин\"", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Заполните поле \"пароль\"", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (textBoxPassword.Text.Length > logPassLength)
            {
                MessageBox.Show("Ограничение на пароль в 50 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(textBoxMail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                MessageBox.Show("Почта введена некорректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (textBoxMail.Text.Length > logPassLength)
            {
                MessageBox.Show("Ограничение на почту в 50 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new ProviderBindingModel
                {
                    FirstName = textBoxFirstName.Text,
                    LastName = textBoxLastName.Text,
                    Login = textBoxLogin.Text,
                    Password = textBoxPassword.Text,
                    Mail = textBoxMail.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
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
    }
}
