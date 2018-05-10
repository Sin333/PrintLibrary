using PrintLibrary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PrintLibraryClass PrintLibrary = new PrintLibraryClass();
        public MainWindow()
        {
            InitializeComponent();
            UICBListPrinters.ItemsSource = PrintLibrary.GetListofPrinters();
        }

        private void UIBRun_Click(object sender, RoutedEventArgs e)
        {
            PrintLibrary.PrintingDocumentTest(1);
        }

        private void UICBListPrinters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PrintLibrary.SelectionPrinter(UICBListPrinters.SelectedItem.ToString());
        }
    }
}
