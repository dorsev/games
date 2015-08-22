using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WpfApplication19.Data;
using System.Threading.Tasks;
using GameCommon;
using GameCommon.Presenters;

namespace WpfApplication19.Presenters
{
    public class TickTacToePresenter : BasePresenter,INotifyPropertyChanged
    {
        public GameManger TheGameManager { get; set; }

        public ObservableCollection<Row> ArrayOfRows { get; set; }

        public void CreateNewGame()
        {
            ///removes all board items, and remove property chnged registration
            RemoveOldHandlers();

            ///creates new board
            InitAndRegisterBoard();

            //create new GameManager,and put the last game height if there was one, otherwise put "3"
            int lastGameManger = 3;

            TheGameManager = new GameManger() { MatrixHeigtWidth = lastGameManger };


        }

        private void InitAndRegisterBoard()
        {
            for (int i = 0; i < TheGameManager.MatrixHeigtWidth; i++)
            {
                ArrayOfRows.Add(new Row(TheGameManager, i));

            }

            foreach (var item in ArrayOfRows)
            {
                foreach (var button in (item as Row).Items)
                {
                    button.PropertyChanged += new PropertyChangedEventHandler(button_PropertyChanged);
                }

            }
        }

        private void RemoveOldHandlers()
        {
            foreach (var item in ArrayOfRows)
            {
                foreach (var secondItem in item.Items)
                {
                    secondItem.PropertyChanged -= new PropertyChangedEventHandler(button_PropertyChanged);
                }

            }
            ArrayOfRows.Clear();
        }

        public TickTacToePresenter():base()
        {
            RestartGame = new DorCommand((o) => CreateNewGame(), new Func<bool>(() => true));
            TheGameManager = new GameManger();
            ArrayOfRows = new ObservableCollection<Row>();
            CreateNewGame();
        }

        void button_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                Task.Factory.StartNew(()
                    =>
                    {
                        calcWinner();
                    }
                );

            }
        }

        private void calcWinner()
        {
            int areRowWinners = ReturnRowFinished(ArrayOfRows);
            int areColumnsWinners = returnColumnsFinished(ArrayOfRows);
            int areCrossWinners = returnAreCrossFinished(ArrayOfRows);

            if (areRowWinners != -1)
            {
                SetyArrayWinners(ArrayOfRows[areRowWinners].Items);
                disableAllGrid();
            }
            if (areColumnsWinners != -1)
            {
                var allItems = GetAllItemsFromRows();
                var columnIposition = from m in allItems
                                      where m.YPosition == areColumnsWinners
                                      select m;
                SetyArrayWinners(new ObservableCollection<GameItem>(columnIposition.ToList()));
                disableAllGrid();
            }
            if (areCrossWinners != -1)
            {
                SetCrossWinner(areCrossWinners);
                disableAllGrid();
            }


        }

        private List<GameItem> GetAllItemsFromRows()
        {
            List<GameItem> toReturn = new List<GameItem>();
            foreach (var row in ArrayOfRows)
            {
                foreach (var gamepiece in row.Items)
                {
                    toReturn.Add(gamepiece);
                }
            }
            return toReturn;
        }

        private void disableAllGrid()
        {
            foreach (var item in ArrayOfRows)
            {
                foreach (var piece in item.Items)
                {
                    if (!piece.IsPartOfWinner)
                    {
                        piece.IsEnabled = false;
                    }
                }
            }

        }

        private void SetCrossWinner(int areCrossWinners)
        {
            switch (areCrossWinners)
            {
                case 0:
                    {
                        GameItem[,] matrix = ReturnMatrixFromArrayGamePieces();
                        matrix[0, 0].IsPartOfWinner = true;
                        matrix[1, 1].IsPartOfWinner = true;
                        matrix[2, 2].IsPartOfWinner = true;
                    }
                    break;
                case 1:
                    {
                        GameItem[,] matrix = ReturnMatrixFromArrayGamePieces();
                        matrix[2, 0].IsPartOfWinner = true;
                        matrix[1, 1].IsPartOfWinner = true;
                        matrix[0, 2].IsPartOfWinner = true;
                    }
                    break;
                default:
                    break;
            }

        }

        private void SetColumnWinner(int areColumnsWinners)
        {
            var allItems = GetAllItemsFromRows();
            var columnIposition = from gamePiece in allItems
                                  where gamePiece.YPosition == areColumnsWinners
                                  select gamePiece;

            foreach (var item in columnIposition)
            {
                item.IsPartOfWinner = true;
                item.SetWinner();
            }
        }

        /// <summary>
        /// this Function is really ugly.
        /// </summary>
        /// <param name="ArrayOfRows"></param>
        /// <returns></returns>
        private int returnAreCrossFinished(ObservableCollection<Row> ArrayOfRows)
        {
            values[,] matrix = ReturnMatrixFromArray();

            if ((matrix[0, 0] == matrix[1, 1]) &&
                (matrix[1, 1] == matrix[2, 2]) &&
                (matrix[0, 0] != values.Empty))
            {
                return 0;
            }
            if ((matrix[2, 0] == matrix[1, 1]) &&
           (matrix[1, 1] == matrix[0, 2]) &&
           (matrix[1, 1] != values.Empty))
            {
                return 1;
            }

            return -1;
        }

        /// <summary>
        /// should return column number
        /// </summary>
        /// <param name="ArrayOfRows"></param>
        /// <returns></returns>
        private int returnColumnsFinished(ObservableCollection<Row> ArrayOfRows)
        {
            var arrayOfItems = GetAllItemsFromRows();
            for (int i = 0; i < TheGameManager.MatrixHeigtWidth; i++)
            {
                var columnIposition = from gamePiece in arrayOfItems
                                      where gamePiece.Value != values.Empty && gamePiece.YPosition == i
                                      group gamePiece by gamePiece.Value into rowValues
                                      select new { count = rowValues.Count() };
                if (columnIposition.Count() == 1)
                {
                    var items = from grouping in columnIposition
                                where grouping.count == TheGameManager.MatrixHeigtWidth
                                select new { };
                    if (items.Count() > 0)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private ObservableCollection<GameItem> GetListOfGameItems(ObservableCollection<Row> ArrayOfRows)
        {
            ObservableCollection<GameItem> toReturn = new ObservableCollection<GameItem>();
            foreach (Row item in ArrayOfRows)
            {
                foreach (var piece in item.Items)
                {
                    toReturn.Add(piece);
                }
            }
            return toReturn;
        }

        private int ReturnRowFinished(ObservableCollection<Row> ArrayOfRows)
        {
            for (int i = 0; i < ArrayOfRows.Count; i++)
            {
                if (ReturnIsRowFinished(ArrayOfRows[i].Items))
                {
                    return i;
                }
            }
            return -1;
        }

        private bool ReturnIsRowFinished(ObservableCollection<GameItem> row)
        {
            var categoryCounts = from gamePiece in row
                                 where gamePiece.Value != values.Empty
                                 group gamePiece by gamePiece.Value into rowValues
                                 select new { count = rowValues.Count() };

            if (categoryCounts.Count() == 1)
            {
                var items = from grouping in categoryCounts
                            where grouping.count == TheGameManager.MatrixHeigtWidth
                            select new { };
                if (items.Count() > 0)
                {
                    return true;
                }
            }
            return false;

        }

        private void SetyArrayWinners(ObservableCollection<GameItem> array)
        {
            foreach (GameItem item in array)
            {
                item.IsPartOfWinner = true;
                item.SetWinner();
            }
        }

        private values[,] ReturnMatrixFromArray()
        {
            values[,] toReturn = new values[3, 3];
            for (int i = 0; i < 3; i++)
            {
                ObservableCollection<GameItem> listOfitems = ArrayOfRows[i].Items;
                for (int j = 0; j < 3; j++)
                {
                    toReturn[i, j] = listOfitems[j].Value;
                }
            }
            return toReturn;
        }

        private GameItem[,] ReturnMatrixFromArrayGamePieces()
        {
            GameItem[,] toReturn = new GameItem[3, 3];
            for (int i = 0; i < 3; i++)
            {
                ObservableCollection<GameItem> listOfitems = ArrayOfRows[i].Items;
                for (int j = 0; j < 3; j++)
                {
                    toReturn[i, j] = listOfitems[j];
                }
            }
            return toReturn;
        }

        #region INOitfyPropetyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void notifyProperty(string property)
        {
            if (property != null)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(property, new PropertyChangedEventArgs(property));
                }
            }
        }

        #endregion INOitfyPropetyChanged

        public object rownum { get; set; }
    }
}
