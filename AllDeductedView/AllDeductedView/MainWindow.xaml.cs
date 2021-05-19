using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace AllDeductedView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<StudentsWindow>();
            window.ShowDialog();
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<OrdersWindow>();
            window.ShowDialog();
        }

        private void ButtonStatus_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<StudyingStatusesWindow>();
            window.ShowDialog();
        }

        private void ButtonLink_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<OrderGroupWindow>();
            window.ShowDialog();
        }
        private void ButtonReportDiscipline_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<ListDisciplineWindow>();
            window.ShowDialog();
        }

        private void ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<ReportStatusWindow>();
            window.ShowDialog();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
