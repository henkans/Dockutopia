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


        }



        private void textBoxCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                textBoxCommand.CaretIndex = textBoxCommand.Text.Length;
            }
        }

        private void textBoxOutput_TextChanged(object sender, TextChangedEventArgs e)
        {



            textBoxOutput.ScrollToEnd();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxCommand.Focus();
        }
    }
}
