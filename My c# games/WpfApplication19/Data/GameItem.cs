using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using GameCommon;

namespace WpfApplication19.Data
{
    public enum values
    {
        X = 0,
        O = 1,
        Empty = 2
    }
    public enum GameMode
    {
        AgainstComputer=0,
        AgainstOtherPlayer=1,
        IDontCare=2
    }
    public class GameItem : TickTacToeBaseItem, INotifyPropertyChanged
    {
        private bool isEnabled;
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }

            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        private bool _isPartOfWinner;
        public bool IsPartOfWinner
        {
            get
            {
                return _isPartOfWinner;
            }

            set
            {
                _isPartOfWinner = value;
                OnPropertyChanged("IsPartOfWinner");
            }
        }
        public int count { get; set; }
        public DorCommand setValueCommand { get; set; }

        private values _value;
        public values Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public GameItem(WpfApplication19.Presenters.GameManger gamingManager, int XPosition, int YPosition)
            : base(gamingManager, XPosition, YPosition)
        {
            Value = values.Empty;
            setValueCommand = new DorCommand((param) => DoSomething(), (() => true));
            _isPartOfWinner = false;
            IsEnabled = true;
        }

        private void DoSomething()
        {
            Value = theGameManger.getNextValue();
            theGameManger.lastValueUsed = Value;
        }
        public void SetWinner()
        {
            IsPartOfWinner = true;
            OnPropertyChanged("IsPartOfWinner");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
