using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using WpfApplication19.Data;
using System.ComponentModel;

namespace WpfApplication19.Presenters
{
    public class Row : TickTacToeBaseItem, INotifyPropertyChanged
    {
        public int RowNum { get; set; }
        public ObservableCollection<GameItem> Items { get; set; }

        public Row(GameManger m, int rowNum)
            : base(m, 0, rowNum)
        {
            Items = new ObservableCollection<GameItem>();
            for (int i = 0; i < theGameManger.MatrixHeigtWidth; i++)
            {
                Items.Add(new GameItem(m, rowNum, i));
            }


        }
    }
}

