using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
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
            var game = this.DataContext as Game;
            game.NewGame();
            playfield.IsEnabled = true;
            newGameBtn.IsEnabled = false;
        }

        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var field = sender as Field;
            var game = this.DataContext as Game;
            if (field.Type != FieldType.Empty)
                return;
            if(game.MyTurn)
                field.Type = FieldType.X;
            else
                field.Type = FieldType.O;

            if(game.Move(field.SlotX, field.SlotY, game.MyTurn ? FieldType.X : FieldType.O))
            {
                playfield.IsEnabled = false;
                newGameBtn.IsEnabled = true;
                return;
            }

            game.SwitchSides();
        }
    }
}
