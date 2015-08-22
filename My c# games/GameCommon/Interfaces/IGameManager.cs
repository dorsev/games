using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GameCommon;

namespace GameCommon.Interfaces
{
    public interface IGameManager
    {
         void calcWinner();

         void calcError();
      
    }
}
