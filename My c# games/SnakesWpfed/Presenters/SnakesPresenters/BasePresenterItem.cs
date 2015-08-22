using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnakesWpfed.Model;
using System.ComponentModel;

namespace SnakesWpfed.Presenters.SnakesPresenters
{
    public class BasePresenterItem
        : INotifyPropertyChanged
    {
        public GamePiece CurrentGamePiece { get; set; }
        public BasePresenterItem(GamePiece g)
        {
            CurrentGamePiece = g;
            CurrentGamePiece.PropertyChanged += new PropertyChangedEventHandler(CurrentGamePiece_PropertyChanged);
            OnPropertyChanged("CurrentGamePiece");
        }

        void CurrentGamePiece_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("CurrentGamePiece");
        }


        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
