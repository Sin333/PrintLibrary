using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PrintLibrary
{
    /// <summary>
    /// If you need Print UIElement:
    /// -In Center You need UIElemnt in a Height and Width = PrintDocument(Height,Width)
    /// </summary>
    public class PrintDocument
    {
        FixedDocument document = new FixedDocument();
        List<string> printerList;
        PrintQueue printQueue;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="height">Height document</param>
        /// <param name="width">Width document</param>
        /// <param name="updateon">Param for run UpdateListofPrinters</param>
        public PrintDocument(double height = 800, double width = 800, bool updateon = true)
        {
            if (updateon)
                UpdateListofPrinters();
            document.DocumentPaginator.PageSize = new Size { Height = height, Width = width };
        }

        /// <summary>
        /// Get all PrinterNames from the system
        /// </summary>
        public void UpdateListofPrinters()
        {
            printerList = new LocalPrintServer().GetPrintQueues().Where(x => x.CurrentJobSettings.CurrentPrintTicket.OutputColor != null).Select(x => x.FullName).ToList();
        }

        /// <summary>
        /// Return a List PrintNames of type string
        /// </summary>
        /// <returns></returns>
        public List<string> GetListofPrinters()
        {
            if (printerList == null)
                UpdateListofPrinters();
            return printerList;
        }

        /// <summary>
        /// Clearning Document for printing
        /// </summary>
        public void CleaningDocument()
        {
            document = new FixedDocument();
        }

        /// <summary>
        /// Selection printer in index of list PrintName
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public bool SelectionPrinter(int Index)
        {
            if (printerList.Count > Index)
            {
                printQueue = new PrintQueue(new PrintServer(), printerList.ElementAt(Index));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Selection printer in name of list PrintName
        /// </summary>
        /// <param name="PrinterName"></param>
        /// <returns></returns>
        public bool SelectionPrinter(string PrinterName)
        {
            if (printerList.Contains(PrinterName))
            {
                printQueue = new PrintQueue(new PrintServer(), PrinterName);
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Test Printing
        /// </summary>
        /// <param name="description"></param>
        public void PrintingDocumentTest(string description = "Printing document")
        {
            PrintDialog pd = new PrintDialog
            {
                PrintQueue = printQueue
            };

            ExampleForm exampleForm = new ExampleForm(); 
            AddedTicketforDocument(exampleForm, exampleForm.UIGrid);

            pd.PrintDocument(document.DocumentPaginator, description);
            CleaningDocument();
        }

        public void PrintingDocumentTest(int countpages, string description = "Printing document")
        {
            PrintDialog pd = new PrintDialog
            {
                PrintQueue = printQueue
            };

            ExampleForm exampleForm;
            for (int i = 0; i < countpages; i++)
            {
                exampleForm = new ExampleForm();
                AddedTicketforDocument(exampleForm, exampleForm.UIGrid);
            }

            pd.PrintDocument(document.DocumentPaginator, description);
            CleaningDocument();
        }

        public void PrintingDocument(string description = "Printing document")
        {
            PrintDialog pd = new PrintDialog
            {
                PrintQueue = printQueue
            };

            pd.PrintDocument(document.DocumentPaginator, description);
            CleaningDocument();
        }

        /// <summary>
        /// After this method you Page.Content=null;
        /// Page must Contains UIVisual
        /// </summary>
        /// <param name="Page">You page there you Contains you are UIElement</param>
        /// <param name="UIVisual">UIElement for added in document</param>
        public void AddedTicketforDocument(Page Page, UIElement UIVisual)
        {
            UIElement visual = UIVisual;
            Page.Content = null;

            FixedPage fixedPage = new FixedPage();
            fixedPage.Children.Add(visual);

            fixedPage.Width = document.DocumentPaginator.PageSize.Width;
            fixedPage.Height = document.DocumentPaginator.PageSize.Height;
            PageContent contentpage = new PageContent
            {
                Child = fixedPage
            };

            document.Pages.Add(contentpage);
        }
    }
}