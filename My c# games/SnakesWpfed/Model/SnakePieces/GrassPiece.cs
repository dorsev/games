using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameCommon.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using SnakesWpfed.Common;

namespace SnakesWpfed.Model.SnakePieces
{
    public class GrassPiece :GamePiece, INotifyPropertyChanged
    {
        public GrassPiece(decimal x, decimal y):base(x,y)
        {

        }
        public void changeAllProperties()
        {

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
