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
    /// Логика взаимодействия для StudyingStatusesWindow.xaml
    /// </summary>
    public partial class StudyingStatusesWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly StudyingStatusLogic logic;
        private readonly Logger logger;

        public StudyingStatusesWindow(StudyingStatusLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void StudyingStatusWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = logic.Read( new StudyingStatusBindingModel
                {
                    ProviderId = App.SelectProvider.Id
                });
                if (list != null)
                {
                    dataGridStudyingStatuses.ItemsSource = list;
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
            var form = Container.Resolve<StudyingStatusWindow>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridStudyingStatuses.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<StudyingStatusWindow>();
                int id = ((StudyingStatusViewModel)dataGridStudyingStatuses.SelectedItems[0]).Id;
                form.Id = id;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridStudyingStatuses.SelectedItems.Count == 1)
            {
                MessageBoxResult result = (MessageBoxResult)MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = ((StudyingStatusViewModel)dataGridStudyingStatuses.SelectedItems[0]).Id;
                    try
                    {
                        logic.Delete(new StudyingStatusBindingModel { Id = id });
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
