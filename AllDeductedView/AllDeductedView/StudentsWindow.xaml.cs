using AllDeductedBusinessLogic.BindingModels;
using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.ViewModels;
using NLog;
using System;
using System.Windows;
using Unity;

namespace AllDeductedView
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentsWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly StudentLogic logic;
        private readonly Logger logger;

        public StudentsWindow(StudentLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void StudentsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = logic.Read(new StudentBindingModel
                {
                    ProviderId = App.SelectProvider.Id
                });
                if (list != null)
                {
                    dataGridStudents.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка загрузки данных : " + ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<StudentWindow>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridStudents.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<StudentWindow>();
                int id = ((StudentViewModel)dataGridStudents.SelectedItems[0]).Id;
                form.Id = id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridStudents.SelectedItems.Count == 1)
            {
                MessageBoxResult result = (MessageBoxResult)MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = ((StudentViewModel)dataGridStudents.SelectedItems[0]).Id;
                    try
                    {
                        logic.Delete(new StudentBindingModel { Id = id });
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
        private void ButtonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
