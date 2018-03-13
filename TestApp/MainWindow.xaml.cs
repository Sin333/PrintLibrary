using PrintLibrary;
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

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PrintDocument testPrinting = new PrintDocument(MainWindow.UI);
        public MainWindow()
        {
            InitializeComponent();
            //testPrinting.UpdateListofPrinters();
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
