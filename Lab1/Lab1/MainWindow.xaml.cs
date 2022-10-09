using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnWinowLoaded;
        }

        private void OnWinowLoaded(object sender, EventArgs e)
        {
            Loaded -= OnWinowLoaded;
            this.DataContext = new Game();
        }

        private void OnStartNewGameBtnClick(object sender, EventArgs e)
        {
            int I = 0;
        }

        private void OnFieldMouseDown(object sender, EventArgs e)
        {
            var field = sender as Image;
        }
    }
}
