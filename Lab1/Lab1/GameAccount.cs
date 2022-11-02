using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    // Used to store game information in game history
    public class GameInfo
    {
        public DateTime Time { get; set; }
        public GameAccount Opponent { get; set; }
        public uint RatingBet { get; set; }
        public bool Win { get; set; }
        public uint MatchId { get; set; }
        public GameType Type { get; set; }
    }


    public class GameAccount : ViewModelBase
    {
        // STATIC

        // PUBLIC

        // CTROR
        public GameAccount(string Name)
        {
            this.Name = Name;
            GameHistory = new ReadOnlyObservableCollection<GameInfo>(_gameHistory);
        }
        // copying object method
        public GameAccount Copy()
        {
            return (GameAccount)MemberwiseClone();
        }
        public virtual int GetRatingBet()
        {
            return 30;
        }
        public virtual void WinGame(GameInfo info)
        {
            Rating += GetRatingBet();
            _gameHistory.Add(info);
        }

        public virtual void LoseGame(GameInfo info)
        {
            Rating -= GetRatingBet();
            _gameHistory.Add(info);
        }

        public string Name 
        { 
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public int Rating 
        {
            get => _rating;
            protected set
            {
                if(value < 1)   // prevent from asigning invalid values
                    _rating = 1;
                else
                    _rating = value;
                OnPropertyChanged(nameof(Rating));  // Update wpf controls to display new rating value
            }
        }

        public ReadOnlyObservableCollection<GameInfo> GameHistory { get; private set; }

        // PRIVATE
        protected ObservableCollection<GameInfo> _gameHistory = new ObservableCollection<GameInfo>();
        // Backing fields
        private string _name = "Player";
        private int _rating = 1;
        
    }

    public class NoobAccount : GameAccount
    {
        // STATIC

        // PUBLIC
        public NoobAccount(string Name) : base(Name)
        {
        }
        public override int GetRatingBet()
        {
            return base.GetRatingBet() / 2;
        }

        // PRIVATE
    }

    public class StreakAccount : GameAccount
    {
        // STATIC

        // PUBLIC
        public StreakAccount(string Name) : base(Name)
        {
        }
        public override int GetRatingBet()
        {
            return (int)(base.GetRatingBet() * Math.Log(_streak));
        }

        // Extend win and lose methods to control streak
        public override void WinGame(GameInfo info)
        {
            base.WinGame(info);
            _streak++;
        }

        public override void LoseGame(GameInfo info)
        {
            Rating -= base.GetRatingBet();  // decrease rating by default value, ignore streak factor
            _gameHistory.Add(info);
            _streak = 2;
        }

        // PRIVATE
        private uint _streak = 2; // because log2(2) == 1       
    }
}
