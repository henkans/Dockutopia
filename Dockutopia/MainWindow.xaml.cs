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
            if (e.Key == Key.Enter)
            {
                textBoxCommand.SelectAll();

            }
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                textBoxCommand.CaretIndex = textBoxCommand.Text.Length;
            }
        }

        private void textBoxOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBoxOutput.ScrollToEnd();
        }
    }
}
