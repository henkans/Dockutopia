using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Dockutopia
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //TODO Temp font setting
            this.FontFamily = new FontFamily("Lucida Sans Typewriter");
            //this.FontSize = 8;
           // this.SizeToContent = SizeToContent.Height;

            ((INotifyCollectionChanged)itemsControlOutput.Items).CollectionChanged += new NotifyCollectionChangedEventHandler(ScrollDown);
            

        }

        
        private void textBoxCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                textBoxCommand.CaretIndex = textBoxCommand.Text.Length;
            }
        }

        void ScrollDown(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                scrollViewerOutput.ScrollToEnd();
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeToContent = SizeToContent.Manual;
            textBoxCommand.Focus();
        }

    }
}
