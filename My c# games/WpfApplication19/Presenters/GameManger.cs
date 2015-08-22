using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApplication19.Data;

namespace WpfApplication19.Presenters
{
    public class GameManger
    {
        public int MatrixHeigtWidth { get; set; }
        public GameManger()
        {
            lastValueUsed = values.Empty;
            gameMode = GameMode.AgainstOtherPlayer;
            MatrixHeigtWidth = 3;
        }

        public GameMode gameMode { get; set; }

        public values lastValueUsed { get; set; }

        public values getNextValue()
        {
            switch (lastValueUsed)
            {

                case values.X:
                    {
                        return values.O;
                    }

                case values.O:
                    {
                        return values.X;
                    }

                case  values.Empty:
                        {
                            return values.X;
                        }

                default:
                        return values.Empty;

            }
        }


        
    }
}
