using PrintLibrary;
using System.Windows;
using System.Windows.Controls;

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PrintDocument testPrinting = new PrintDocument();
        public MainWindow()
        {
            InitializeComponent();
            //testPrinting = new PrintDocument(UIBRun.Height, UIBRun.Width);
            UICBListPrinters.ItemsSource = testPrinting.GetListofPrinters();
        }

        private void UIBRun_Click(object sender, RoutedEventArgs e)
        {
            //testPrinting.SelectionPrinter(UICBListPrinters.SelectedItem.ToString());
            testPrinting.PrintingDocumentTest(1);
        }

        private void UICBListPrinters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            testPrinting.SelectionPrinter(UICBListPrinters.SelectedItem.ToString());
        }
    }
}
