using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Game : ViewModelBase
    {
        private GameAccount _userAccount = new GameAccount();
        public GameAccount UserAccout
        {
            get => _userAccount;
            private set
            {
                if (_userAccount != value)
                {
                    _userAccount = value;
                    OnPropertyChanged(nameof(UserAccout));
                }
            }
        }
        private GameAccount _enemyAccount = new GameAccount();
        public GameAccount EnemyAccount { get; private set; }

        private GameAccount _currentPlayer;

        public void NextPlayer()
        {
            if (_currentPlayer == _userAccount)
                _currentPlayer = _enemyAccount;
            else
                _currentPlayer = _userAccount;
        }
    }
}
