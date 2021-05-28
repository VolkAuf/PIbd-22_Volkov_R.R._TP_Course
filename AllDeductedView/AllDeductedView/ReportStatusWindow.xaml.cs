using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.BusinessLogics.Report;
using AllDeductedBusinessLogic.HelperModels.Mail;
using Microsoft.Win32;
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
    /// Логика взаимодействия для ReportStatusWindow.xaml
    /// </summary>
    public partial class ReportStatusWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        private readonly Logger logger;

        public ReportStatusWindow(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void ButtonMake_Click(object sender, RoutedEventArgs e)
        {
            if (DatePikerTo.SelectedDate == null || DatePikerFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePikerFrom.SelectedDate >= DatePikerTo.SelectedDate)
            {
                MessageBox.Show("Дата указана неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var dataSource = logic.GetStatus(new ReportBindingModel
                {
                    DateFrom = DatePikerFrom.SelectedDate,
                    DateTo = DatePikerTo.SelectedDate,
                    ProviderId = App.SelectProvider.Id
                });
                dataGridStudyingStatuses.ItemsSource = dataSource;
                textBoxDateFrom.Content = DatePikerFrom.SelectedDate.Value.ToLongDateString();
                textBoxDateTo.Content = DatePikerTo.SelectedDate.Value.ToLongDateString();
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка формирования данных : " + ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonToPdf_Click(object sender, RoutedEventArgs e)
        {
            if (DatePikerTo.SelectedDate == null || DatePikerFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePikerFrom.SelectedDate >= DatePikerTo.SelectedDate)
            {
                MessageBox.Show("Дата указана неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            {
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        logic.SaveToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = DatePikerFrom.SelectedDate,
                            DateTo = DatePikerTo.SelectedDate
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Ошибка создания .pdf : " + ex.Message);
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ButtonMail_Click(object sender, RoutedEventArgs e)
        {
            if (DatePikerTo.SelectedDate == null || DatePikerFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePikerFrom.SelectedDate >= DatePikerTo.SelectedDate)
            {
                MessageBox.Show("Дата указана неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var fileName = "Report.pdf";
                logic.SaveToPdfFile(new ReportBindingModel
                {
                    FileName = fileName,
                    DateFrom = DatePikerFrom.SelectedDate,
                    DateTo = DatePikerTo.SelectedDate
                });
                Console.WriteLine(App.SelectProvider.Mail);
                MailLogic.MailSend(new MailSendInfo
                {
                    MailAddress = App.SelectProvider.Mail,
                    Subject = "Отчет по статусам",
                    Text = "Отчет по статусам от " + DatePikerFrom.SelectedDate.Value.ToShortDateString() + " по " + DatePikerTo.SelectedDate.Value.ToShortDateString(),
                    FileName = fileName
                });
                MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка создания .pdf : " + ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
        }
    }
}
