using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class GameAccount : ViewModelBase
    {

        private string _userName = "dsdsdsds";
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
    }
}
