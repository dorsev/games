using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using WpfApplication19.Presenters;
using GameCommon.Data;
namespace WpfApplication19.Data
{

    public class TickTacToeBaseItem : UIBaseItem, INotifyPropertyChanged
    {
        public WpfApplication19.Presenters.GameManger theGameManger { get; set; }

        public TickTacToeBaseItem(GameManger m, int XPosition, int YPosition)
            : base(XPosition, YPosition)
        {
            theGameManger = m;
        }
    }
}
