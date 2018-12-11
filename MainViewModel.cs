using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _userName;
        private string _password;
        public MainViewModel()
        {
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                Set(() => UserName, ref _userName, value);
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                Set(() => Password, ref _password, value);
            }
        }
    }
}
