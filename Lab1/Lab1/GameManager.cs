using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab1
{
    public enum GameType
    {
        RatingGame,
        TrainingGame,
        OneWayGame
    }

    // This is the singleton, will be created only once and set up as main window data context
    public class GameManager : ViewModelBase
    {
        // STATIC

        // PUBLIC
        public void NewGame(GameAccount p1, GameAccount p2, GameType type)
        {
            Field.ClearFields();    // clear playgrid
            Player = true;         // make x move first
            // Create new game instance based of the type
            switch(type)
            {
                case GameType.RatingGame:
                    CurrentGame = new RatingGame(p1, p2);
                    break;
                case GameType.TrainingGame:
                    CurrentGame = new TrainingGame(p1, p2);
                    break;
                case GameType.OneWayGame:
                    CurrentGame = new OneWayGame(p1, p2, true); // one way game only available for X player
                    break;
            }
        }

        public bool Player { get; set; } // indicates who will make next move: true if x, false if o

        public Game CurrentGame // current game pointer
        { 
            get => _currentGame;
            private set
            {
                if(_currentGame != value)
                {
                    _currentGame = value;
                    OnPropertyChanged(nameof(CurrentGame));
                }
            }
        }
        
        // PRIVATE
        private Game _currentGame = null;   // backing field
    }
}
