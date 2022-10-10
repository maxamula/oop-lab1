using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public GameAccount EnemyAccount
        {
            get => _enemyAccount;
            private set
            {
                if (_enemyAccount != value)
                {
                    _enemyAccount = value;
                    OnPropertyChanged(nameof(EnemyAccount));
                }
            }
        }

        private long matchId = 0;
        public uint Rating { get; private set; }

        private uint moves  = 0;
        public bool MyTurn { get; private set; } = true;
        private FieldType[,] map = new FieldType[3, 3];
        
        private Random r = new Random(1337);
        public void NewGame()
        {
            matchId++;
            Rating = (uint)r.Next(0, 150);
            _currentAccount = _userAccount;
            moves = 0;
            MyTurn = true;
            Field.ClearFields();
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    map[i, j] = FieldType.Empty;
                }
            }
        }

        public bool Move(int x, int y, FieldType s)
        {
            if (map[x, y] == FieldType.Empty)
            {
                map[x, y] = s;
            }
            moves++;

            var winner = _currentAccount == _userAccount ? _userAccount : _enemyAccount;
            var loser = _currentAccount == _userAccount ? _enemyAccount : _userAccount;

            for (int i = 0; i < 3; i++)
            {
                if (map[x, i] != s)
                    break;
                if (i == 3 - 1)
                {
                    winner.WinGame(new GameInfo() { enemy = loser, Info = $"{winner.UserName} vs {loser.UserName}", MatchID = $"MatchId: {matchId}", Rating = $"Rating: {Rating}", Result="Win", Time = DateTime.Now}, Rating);
                    loser.LoseGame(new GameInfo() { enemy = winner, Info = $"{winner.UserName} vs {loser.UserName}", MatchID = $"MatchId: {matchId}", Rating = $"Rating: -{Rating}", Result = "Loose", Time = DateTime.Now }, Rating);
                    return true;
                }
            }


            for (int i = 0; i < 3; i++)
            {
                if (map[i, y] != s)
                    break;
                if (i == 3 - 1)
                {
                    winner.WinGame(new GameInfo() { enemy = loser, Info = $"{winner.UserName} vs {loser.UserName}", MatchID = $"MatchId: {matchId}", Rating = $"Rating: {Rating}", Result = "Win", Time = DateTime.Now }, Rating);
                    loser.LoseGame(new GameInfo() { enemy = winner, Info = $"{winner.UserName} vs {loser.UserName}", MatchID = $"MatchId: {matchId}", Rating = $"Rating: -{Rating}", Result = "Loose", Time = DateTime.Now }, Rating);
                    return true;
                }
            }

            if (x == y)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (map[i, i] != s)
                        break;
                    if (i == 3 - 1)
                    {
                        winner.WinGame(new GameInfo() { enemy = loser, Info = $"{winner.UserName} vs {loser.UserName}", MatchID = $"MatchId: {matchId}", Rating = $"Rating: {Rating}", Result = "Win", Time = DateTime.Now }, Rating);
                        loser.LoseGame(new GameInfo() { enemy = winner, Info = $"{winner.UserName} vs {loser.UserName}", MatchID = $"MatchId: {matchId}", Rating = $"Rating: -{Rating}", Result = "Loose", Time = DateTime.Now }, Rating);
                        return true;
                    }
                }
            }

            if (x + y == 3 - 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (map[i, (3 - 1) - i] != s)
                        break;
                    if (i == 3 - 1)
                    {
                        winner.WinGame(new GameInfo() { enemy = loser, Info = $"{winner.UserName} vs {loser.UserName}", MatchID = $"MatchId: {matchId}", Rating = $"Rating: {Rating}", Result = "Win", Time = DateTime.Now }, Rating);
                        loser.LoseGame(new GameInfo() { enemy = winner, Info = $"{winner.UserName} vs {loser.UserName}", MatchID = $"MatchId: {matchId}", Rating = $"Rating: -{Rating}", Result = "Loose", Time = DateTime.Now }, Rating);
                        return true;
                    }
                }
            }

            if (moves == 9)
            {
                return true;
            }
            return false;
        }

        private GameAccount _currentAccount;
        public void SwitchSides()
        {
            if (_currentAccount == _userAccount)
                _currentAccount = _enemyAccount;
            else
                _currentAccount = _userAccount;
            MyTurn = !MyTurn;
        }
    }
}
