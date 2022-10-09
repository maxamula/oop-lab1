using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class GameInfo
    {
        public DateTime Time { get; set; }
        public string Result { get; set; }
        public string Info { get; set; }
        public string Rating { get; set; }
    }

    public class GameAccount : ViewModelBase
    {
        public GameAccount()
        {
            GameHistory = new ReadOnlyObservableCollection<GameInfo>(_gameHistory);
        }

        private string _userName = "Player";
        public string UserName
        {
            get => _userName;
            set
            {
                if (ValidateUserName(value) && _userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }


        private bool ValidateUserName(string username)
        {
            username = username.Trim();
            if (username == string.Empty)
                return false;
            if (username.Length > 50)
                return false;
            return true;
        }

        private uint _score = 0;
        public uint Score 
        {
            get => _score;
            private set
            {
                if(_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }

        public void WinGame(uint rating, GameAccount enemy)
        {
            Score += rating;
            enemy.LoseGame(rating, this);
            _gameHistory.Add(new GameInfo() { Info = $"{UserName} vs {enemy.UserName}", Result = "Win", Time = DateTime.Now, Rating = $"Rating: {rating}"});
        }

        public void LoseGame(uint rating, GameAccount enemy)
        {
            _gameHistory.Add(new GameInfo() { Info = $"{UserName} vs {enemy.UserName}", Result = "Lose", Time = DateTime.Now, Rating = $"Rating: -{rating}" });
            if ((int)Score - rating < 0)
            {
                Score = 0;
                return;
            }
            Score -= rating;    
        }

        public ReadOnlyObservableCollection<GameInfo> GameHistory { get; private set; } 
        private ObservableCollection<GameInfo> _gameHistory = new ObservableCollection<GameInfo>();
    }
}
