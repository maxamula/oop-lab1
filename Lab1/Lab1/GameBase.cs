using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab1
{
    public abstract class Game : ViewModelBase
    {
        // STATIC 
        private static uint s_lastId = 0;

        // PUBLIC
        public Game(GameAccount p1, GameAccount p2)
        {
            // Increment last used id and assign it to current instance of game
            _gameId = ++s_lastId;
            // Set up players
            Player1 = p1;
            Player2 = p2;
            // Clear map
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    map[i, j] = FieldType.Empty;
                }
            }
        }

        // Will return true if one of the players won
        // Automatically will call Wingame or Losegame for players  
        // x, y: coordinate of the cell
        // s: x or o type of th cell
        // player: true if 1st player if else: false
        public virtual bool Move(int x, int y, FieldType s, bool player)
        {
            // Set field
            if (map[x, y] == FieldType.Empty)
            {
                map[x, y] = s;
            }

            _moves++;

            // rows
            for (int i = 0; i < 3; i++)
            {
                if (map[x, i] != s)
                    break;
                if (i == 3 - 1)
                {
                    PlayerWin(player);
                    return true;
                }
            }

            // cols
            for (int i = 0; i < 3; i++)
            {
                if (map[i, y] != s)
                    break;
                if (i == 3 - 1)
                {
                    PlayerWin(player);
                    return true;
                }
            }
            // diagonal
            if (x == y)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (map[i, i] != s)
                        break;
                    if (i == 3 - 1)
                    {
                        PlayerWin(player);
                        return true;
                    }
                }
            }
            // reverse diagonal
            if (x + y == 3 - 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (map[i, (3 - 1) - i] != s)
                        break;
                    if (i == 3 - 1)
                    {
                        PlayerWin(player);
                        return true;
                    }
                }
            }
            // draw
            if (_moves == 9)
            {
                return true;
            }
            return false;
        }

        // Calls when player wins
        // player: true if 1st player
        public abstract void PlayerWin(bool player);

        public GameAccount Player1
        { 
            get => _player1;
            private set
            {
                if(_player1 != value)
                {
                    _player1 = value;
                    OnPropertyChanged(nameof(Player1));
                }
            }
        }

        public GameAccount Player2
        {
            get => _player2;
            private set
            {
                if (_player2 != value)
                {
                    _player2 = value;
                    OnPropertyChanged(nameof(Player2));
                }
            }
        }

        // PRIVATE
        protected uint _gameId = 0;

        private GameAccount _player1;
        private GameAccount _player2;
        private FieldType[,] map = new FieldType[3, 3]; // represents each cell of playgrid
        private uint _moves = 0;    // number of moves made during the game
    }

    public class RatingGame : Game
    {
        public RatingGame(GameAccount p1, GameAccount p2) : base(p1, p2)
        {
        }

        public override void PlayerWin(bool player)
        {
            var winner = Player1;
            var loser = Player2;
            // If 2nd player won, swap winner and loser
            if (!player)
            {
                GameAccount temp = winner;
                winner = loser;
                loser = temp;
            }
            // Rating game makes both players gain/lose rating
            GameInfo loseInfo = new GameInfo() { MatchId = this._gameId, Opponent = winner, RatingBet = (uint)loser.GetRatingBet(), Time = DateTime.Now, Win = false, Type = GameType.RatingGame };
            GameInfo winInfo = new GameInfo() { MatchId = this._gameId, Opponent = loser, RatingBet = (uint)winner.GetRatingBet(), Time = DateTime.Now, Win = true, Type = GameType.RatingGame };

            winner.WinGame(winInfo);
            loser.LoseGame(loseInfo);
        }
    }

    public class TrainingGame : Game
    {
        public TrainingGame(GameAccount p1, GameAccount p2) : base(p1, p2)
        {
        }

        public override void PlayerWin(bool player)
        {
            var winner = Player1;
            var loser = Player2;
            // If 2nd player won, swap winner and loser
            if (!player)
            {
                GameAccount temp = winner;
                winner = loser;
                loser = temp;
            }
            // Training game, no rating
        }
    }

    public class OneWayGame : Game
    {
        public OneWayGame(GameAccount p1, GameAccount p2, bool player) : base(p1, p2)
        {
        }

        public override void PlayerWin(bool player)
        {
            var winner = Player1;
            var loser = Player2;
            // If 2nd player won, swap winner and loser
            if (!player)
            {
                GameAccount temp = winner;
                winner = loser;
                loser = temp;
            }
            // OneWay game
            if(player == _ratingPlayer)
            {
                winner.WinGame(null);
            }
        }

        // true if 1st player will lose/gain rating false if second
        private bool _ratingPlayer = true;
    }
}
