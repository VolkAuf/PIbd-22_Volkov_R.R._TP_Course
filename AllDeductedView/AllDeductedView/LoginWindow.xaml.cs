using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly ProviderLogic logic;
        private readonly Logger logger;
        public LoginWindow(ProviderLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxEmail.Text))
                {
                    MessageBox.Show("Введите почту", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(passwordBox.Password))
                {
                    MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }

                var providers = logic.Read(null);

                if (logic.Login(new ProviderBindingModel
                {
                    Login = textBoxEmail.Text,
                    Password = passwordBox.Password
                }))
                {

                    App.SelectProvider = providers.FirstOrDefault(rec => rec.Login == textBoxEmail.Text &&
                    rec.Password == passwordBox.Password);
                    var MainWindow = Container.Resolve<MainWindow>();
                    MainWindow.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверно введен пароль или логин", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка входа в приложение : " + ex.Message);
                MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButton.OK,
                  MessageBoxImage.Error);
            }
        }
        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<RegistrationWindow>();
            form.ShowDialog();
        }
    }
}
