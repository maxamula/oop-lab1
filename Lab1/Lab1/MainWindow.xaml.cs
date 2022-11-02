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
            this.DataContext = new GameManager();
        }

        NoobAccount player1 = new NoobAccount("Noob");
        StreakAccount player2 = new StreakAccount("Pro");

        private void OnStartNewGameBtnClick(object sender, EventArgs e)
        {
            var manager = this.DataContext as GameManager;
            manager.NewGame(player1, player2, GameType.RatingGame);
            playfield.IsEnabled = true;
            newGameBtn.IsEnabled = false;
        }

        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var field = sender as Field;
            var manager = this.DataContext as GameManager;
            if (field.Type != FieldType.Empty)
                return;
            if(manager.Player)
                field.Type = FieldType.X;
            else
                field.Type = FieldType.O;

            if(manager.CurrentGame.Move(field.SlotX, field.SlotY, manager.Player ? FieldType.X : FieldType.O, manager.Player))
            {
                playfield.IsEnabled = false;
                newGameBtn.IsEnabled = true;
                return;
            }

            manager.Player = !manager.Player;
        }
    }
}
