using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GameCommon;
using GameCommon.Presenters;
using GameCommon.Interfaces;
using SnakesWpfed.Common;
using System.Windows.Threading;
using SnakesWpfed.Model;
using System.Threading;
using System.IO;
using SnakesWpfed.Presenters.SnakesPresenters;
using System.Diagnostics;

namespace SnakesWpfed.Presenters
{
    public enum RunningSpeedState { Faster, Slower };
    public class SnakesPresenter : BasePresenter, INotifyPropertyChanged
    {
        #region private

        private bool _isGamePaused;
        private ObservableCollection<BasePresenterItem> _arrayOfItems;

        #endregion

        #region properties

        public bool IsGamePaused
        {
            get
            {
                return _isGamePaused;
            }
            set
            {
                _isGamePaused = value;
                OnPropertyChanged("IsGamePaused");
            }
        }
        public Random myRand { get; set; }
        public DorCommand MoveSnakeUp { get; set; }
        public DorCommand MoveSnakeRight { get; set; }
        public DorCommand MoveSnakeLeft { get; set; }
        public DorCommand PauseResumeGame { get; set; }
        public DorCommand SlowDownStuff { get; set; }
        public DorCommand MoveSnakeDown { get; set; }
        public System.Diagnostics.Stopwatch StopWatch { get; set; }
        public int currentIntervalCounter { get; set; }
        public int Counter { get; set; }
        public DispatcherTimer timer { get; set; }
        public static NewSnakesGameManager TheGameManager { get; set; }

        public ObservableCollection<BasePresenterItem> ArrayOfItems
        {
            get
            {
                return _arrayOfItems;
            }
            set
            {
                _arrayOfItems = value;
                OnPropertyChanged("ArrayOfItems");
            }
        }

        #endregion

        #region ctor

        public SnakesPresenter()
            : base()
        {
            startNewGame();

            RestartGame = new DorCommand((m) => restartGame(), (() => true));
            IsGamePaused = false;

        }

        #endregion

        #region functions

        public void startNewGame()
        {
            currentIntervalCounter = 100;
            ImageHelper.changeToDefaults();
            TheGameManager = new NewSnakesGameManager(this);
            ArrayOfItems = new ObservableCollection<BasePresenterItem>();
            TheGameManager.CreateNewGame(ArrayOfItems);
            TheGameManager.CreateFoodItemsInSnakeList(ArrayOfItems);
            TheGameManager.CreateHeadOfSnake(ArrayOfItems);


            StopWatch = new System.Diagnostics.Stopwatch();
            StopWatch.Start();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, currentIntervalCounter);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            MoveSnakeDown = new DorCommand((param) =>
                {
                    var direction = TheGameManager.GetCurrentHeadDirection();
                    if (direction == Direction.Down)
                    {
                        //need to spees things up.
                        SpeedOrSlowUpCurrentIntervalRate(RunningSpeedState.Faster);

                    }
                    else
                    {
                        TheGameManager.setNewGameDirection(Direction.Down, ArrayOfItems);
                    }
                },
                (() => true));
            MoveSnakeLeft = new DorCommand((param) =>
            {
                var direction = TheGameManager.GetCurrentHeadDirection();
                if (direction == Direction.Left)
                {
                    //need to spees things up.
                    SpeedOrSlowUpCurrentIntervalRate(RunningSpeedState.Faster);

                }
                else
                {
                    TheGameManager.setNewGameDirection(Direction.Left, ArrayOfItems);
                }
            },
                (() => true));
            MoveSnakeRight = new DorCommand((param) =>
            {
                var direction = TheGameManager.GetCurrentHeadDirection();
                if (direction == Direction.Right)
                {
                    //need to spees things up.
                    SpeedOrSlowUpCurrentIntervalRate(RunningSpeedState.Faster);

                }
                else
                {
                    TheGameManager.setNewGameDirection(Direction.Right, ArrayOfItems);
                }
            },
                (() => true));
            MoveSnakeUp = new DorCommand((param) =>
            {
                var direction = TheGameManager.GetCurrentHeadDirection();
                if (direction == Direction.Up)
                {
                    //need to spees things up.
                    SpeedOrSlowUpCurrentIntervalRate(RunningSpeedState.Faster);

                }
                else
                {
                    TheGameManager.setNewGameDirection(Direction.Up, ArrayOfItems);
                }
            },
                (() => true));
            SlowDownStuff = new DorCommand((o) => SpeedOrSlowUpCurrentIntervalRate(RunningSpeedState.Slower), (() => true));
            PauseResumeGame = new DorCommand((o) => PauseRestartGame(), (() => true));
        }

        private void SpeedOrSlowUpCurrentIntervalRate(RunningSpeedState s)
        {
            if (currentIntervalCounter / 2 == 0)
            {
                return;
            }
            switch (s)
            {
                case RunningSpeedState.Faster:
                    {
                        currentIntervalCounter = currentIntervalCounter / 2;
                        timer.Interval = new TimeSpan(0, 0, 0, 0, currentIntervalCounter);
                    }
                    break;
                case RunningSpeedState.Slower:
                    {
                        currentIntervalCounter = currentIntervalCounter * 2;
                        timer.Interval = new TimeSpan(0, 0, 0, 0, currentIntervalCounter);
                    }
                    break;
                default:
                    {
                        throw new InvalidOperationException("how did we get here ?");
                    }

            }

        }

        private void PauseRestartGame()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                DebugHelper.WriteLog("Game was paused", "SnakesPresenter.cs");
                IsGamePaused = true;
            }
            else
            {
                IsGamePaused = false;
                DebugHelper.WriteLog("Game was renewed", "SnakesPresenter.cs");
                timer.Start();
            }
        }

        private void InitVideoRandomPath()
        {
            //

        }
        public void restartGame()
        {
            //
        }


        public void changeDirection(Direction e)
        {
            // TheGameManager.setNewGameDirection(e, ArrayOfItems);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Counter++;
            if (StopWatch.ElapsedMilliseconds > 1000)
            {

                DebugHelper.WriteLog(String.Format("Snakes Wpfed : number of snake parts is {0}", ArrayOfItems.OfType<SnakePiecePresenter>().Count()), "SnakesPresenter");
                DebugHelper.WriteLog(String.Format("Snakes Wpfed : fps is {0}", Counter), "SnakesPresenter");

                Counter = 0;
                StopWatch.Restart();
            }
            TheGameManager.UpdateGameBoard(ArrayOfItems);
            OnPropertyChanged("ArrayOfItems");

        }

        #endregion

        #region InoitfyPropetyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion INOitfyPropetyChanged
    }
}
