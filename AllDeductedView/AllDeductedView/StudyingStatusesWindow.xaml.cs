using AllDeductedBusinessLogic.BusinessLogics;
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
    /// Логика взаимодействия для StudyingStatusesWindow.xaml
    /// </summary>
    public partial class StudyingStatusesWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly StudyingStatusLogic logic;

        public StudyingStatusesWindow(StudyingStatusLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void StudyingStatusWindow_Loaded(object sender, RoutedEventArgs e)
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
                    dataGridStudyingStatuses.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
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
            /*if (dataGridStudyingStatuses.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<StudyingStatusWindow>();
                int id = ((StudyingStatusViewModel)dataGridStudyingStatuses.SelectedItems[0]).WorkId;
                form.Id = id;
                form.Duration = ((StudyingStatusViewModel)dataGridStudyingStatuses.SelectedItems[0]).Duration;
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }*/
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            /*if (dataGridStudyingStatuses.SelectedItems.Count == 1)
            {
                MessageBoxResult result = (MessageBoxResult)MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int id = ((StudyingStatusViewModel)dataGridStudyingStatuses.SelectedItems[0]).WorkId;
                    try
                    {
                        logic.Delete(new StudyingStatusBindingModel { WorkId = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }*/
        }
        private void ButtonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
