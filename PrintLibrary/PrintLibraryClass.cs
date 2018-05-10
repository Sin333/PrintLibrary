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
    public class PrintLibraryClass
    {
        private FixedDocument Document = new FixedDocument();
        private List<string> PrinterList;
        private PrintQueue PrintQueue;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="height">Height document</param>
        /// <param name="width">Width document</param>
        /// <param name="updateon">Param for run UpdateListofPrinters</param>
        public PrintLibraryClass(double height = 800, double width = 800, bool updateon = true)
        {
            if (updateon)
                UpdateListofPrinters();
            Document.DocumentPaginator.PageSize = new Size { Height = height, Width = width };
        }

        /// <summary>
        /// Check Selected printer
        /// </summary>
        /// <returns></returns>
        public bool IsSelectedPrinter()
        {
            if (PrintQueue == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Get all PrinterNames from the system
        /// </summary>
        public void UpdateListofPrinters()
        {
            PrinterList = new LocalPrintServer().GetPrintQueues().Where(x => x.CurrentJobSettings.CurrentPrintTicket.OutputColor != null).Select(x => x.FullName).ToList();
        }

        /// <summary>
        /// Return a List PrintNames of type string
        /// </summary>
        /// <returns></returns>
        public List<string> GetListofPrinters()
        {
            if (PrinterList == null)
                UpdateListofPrinters();
            return PrinterList;
        }

        /// <summary>
        /// Return Document for printing
        /// </summary>
        public FixedDocument GetDocumentPrinting()
        {
            return Document;
        }

        /// <summary>
        /// Clearning Document for printing
        /// </summary>
        public void CleaningDocument()
        {
            Document = new FixedDocument();
        }

        /// <summary>
        /// Selection printer in index of list PrintName
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public bool SelectionPrinter(int Index)
        {
            if (PrinterList.Count > Index)
            {
                PrintQueue = new PrintQueue(new PrintServer(), PrinterList.ElementAt(Index));
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
            if (PrinterList.Contains(PrinterName))
            {
                PrintQueue = new PrintQueue(new PrintServer(), PrinterName);
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
                PrintQueue = PrintQueue
            };

            ExampleForm exampleForm = new ExampleForm();
            AddedTicketforDocument(exampleForm, exampleForm.UIGrid);

            pd.PrintDocument(Document.DocumentPaginator, description);
            CleaningDocument();
        }

        public void PrintingDocumentTest(int countpages, string description = "Printing document")
        {
            PrintDialog pd = new PrintDialog
            {
                PrintQueue = PrintQueue
            };

            ExampleForm exampleForm;
            for (int i = 0; i < countpages; i++)
            {
                exampleForm = new ExampleForm();
                AddedTicketforDocument(exampleForm, exampleForm.UIGrid);
            }

            pd.PrintDocument(Document.DocumentPaginator, description);
            CleaningDocument();
        }

        /// <summary>
        /// Print FixedDocument in this Library, after ClearingDocument()
        /// </summary>
        /// <param name="document"></param>
        /// <param name="description"></param>
        public void PrintingDocument(string description = "Printing document")
        {
            PrintDialog pd = new PrintDialog { PrintQueue = PrintQueue };

            pd.PrintDocument(Document.DocumentPaginator, description);
            CleaningDocument();
        }

        /// <summary>
        /// Print you FixedDocument
        /// </summary>
        /// <param name="document"></param>
        /// <param name="description"></param>
        public void PrintingDocument(FixedDocument document, string description = "Printing document")
        {
            PrintDialog pd = new PrintDialog { PrintQueue = PrintQueue };

            pd.PrintDocument(document.DocumentPaginator, description);
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

            fixedPage.Width = Document.DocumentPaginator.PageSize.Width;
            fixedPage.Height = Document.DocumentPaginator.PageSize.Height;
            PageContent contentpage = new PageContent
            {
                Child = fixedPage
            };

            Document.Pages.Add(contentpage);
        }
    }
}