using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.ViewModels;
using Microsoft.Win32;
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
    /// Логика взаимодействия для ListDisciplineWindow.xaml
    /// </summary>
    public partial class ListDisciplineWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ReportLogic reportLogic;
        private readonly StudentLogic studentLogic;
        public ListDisciplineWindow(StudentLogic studentLogic, ReportLogic reportLogic)
        {
            this.studentLogic = studentLogic;
            this.reportLogic = reportLogic;
            InitializeComponent();
        }

        public void ListDisciplineWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = studentLogic.Read(null);
                if (list != null)
                {
                    dataGridStudents.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonWord_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridStudents.SelectedItem == null || dataGridStudents.SelectedItems.Count == 0)
            {
                MessageBox.Show("Студент не выбран", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            try
            {
                if (dialog.ShowDialog() == true)
                {
                    var list = new List<StudentViewModel>();
                    foreach (var student in dataGridStudents.SelectedItems)
                    {
                        list.Add((StudentViewModel)student);
                    }
                    reportLogic.SaveToWordFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName,
                        Students = list,
                    });
                    MessageBox.Show("Формирование завершено", "ОК", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonExcel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridStudents.SelectedItem == null || dataGridStudents.SelectedItems.Count == 0)
            {
                MessageBox.Show("Студент не выбран", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "xls|*.xlsx" };
            try
            {
                if (dialog.ShowDialog() == true)
                {
                    var list = new List<StudentViewModel>();
                    foreach (var student in dataGridStudents.SelectedItems)
                    {
                        list.Add((StudentViewModel)student);
                    }
                    reportLogic.SaveToExcelFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName,
                        Students = list,
                    });
                    MessageBox.Show("Формирование завершено", "ОК", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
