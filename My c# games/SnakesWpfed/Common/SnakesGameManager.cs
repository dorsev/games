//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel;
//using System.Collections.ObjectModel;
//using System.Threading.Tasks;
//using GameCommon;
//using GameCommon.Presenters;
//using GameCommon.Interfaces;
//using SnakesWpfed.Common;
//using SnakesWpfed.Presenters;
//using SnakesWpfed.Model;
//using System.Windows.Controls;
//using System.Threading;
//using System.Diagnostics;

//namespace SnakesWpfed.Common
//{
//    public enum GameMode { Solo, AI };
//    public class SnakesGameManager : IGameManager
//    {
//        public List<GamePiece> snakeItems { get; set; }
//        public AISnakeManager AiManager { get; set; }
//        private SnakesPresenter SnakePresenter { get; set; }

//        private GameMode _currentGameMode;
//        public GameMode CurrentGameMode
//        {
//            get
//            {
//                return _currentGameMode;
//            }
//            set
//            {
//                _currentGameMode = value;
//                if (value == GameMode.AI)
//                {
//                    AiManager = new AISnakeManager(SnakePresenter.ArrayOfItems);
//                }
//            }
//        }
//        public SnakesGameManager(SnakesPresenter s)
//        {
//            ///?
//            SnakePresenter = s;
//            CurrentGameMode = GameMode.Solo;
//            snakeItems = new List<GamePiece>();


//        }
//        public void setNewGameDirection(Direction e, ObservableCollection<GamePiece> items)
//        {
//            var item = from m in items
//                       where m.IsHead == true
//                       select m;
//            if (item != null)
//            {
//                GamePiece s = item.First();
//                if (s != null)
//                {
//                    s.MovingDirection = e;
//                }
//            }

//            moveAllSnakePartsDirection();


//        }
//        public void CreateFoodItemsInSnakeList(ObservableCollection<GamePiece> items)
//        {
//            int count = 0;

//            foreach (var snakeItem in items)
//            {
//                count++;
//                if (count == 6)
//                {
//                    snakeItem.IsFood = true;
//                    count = 0;
//                }

//            }

//        }
//        public void CreateHeadOfSnake(ObservableCollection<GamePiece> items)
//        {
//            int count = 0;

//            foreach (var snakeItem in items)
//            {
//                count++;
//                if (count == 47)
//                {
//                    snakeItem.IsFood = false;
//                    snakeItem.IsHead = true;
//                    snakeItem.IsPartOfSnake = true;
//                    snakeItem.MovingDirection = Direction.Left;

//                    snakeItem.positionInSnake = 1;
//                    snakeItems.Add(snakeItem);
//                    return;
//                }
//            }

//        }
//        public void CreateNewGame(ObservableCollection<GamePiece> Items)
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                for (int j = 0; j < 10; j++)
//                {
//                    Items.Add(new GamePiece(i, j));
//                }
//            }
//        }
//        public void UpdateGameBoard(ObservableCollection<GamePiece> items)
//        {
//            if (CurrentGameMode == GameMode.Solo)
//            {

//                //    MoveSnakeByUser(items, null);
//                MoveSnakeByUserNew(items, null);
//            }
//            else
//            {
//                //AI MODE BABY



//                MoveSnakeByUser(items, AiManager.getNextPointCoridnates());

//            }
//        }

//        private void MoveSnakeByUserNew(ObservableCollection<GamePiece> items, XYPointer nextPointCordinates)
//        {



//            //first item of snake -- only one that has direction
//            var head = items.First((m) => m.positionInSnake == 1);
//            List<GamePiece> newItemsToItrate = new List<GamePiece>(snakeItems);

//            //handle head
//            HandlePartMovment(newItemsToItrate.First(), items, nextPointCordinates);



//            //handle everything else besides the head
//            newItemsToItrate.Remove(head);

//            foreach (var item in newItemsToItrate)
//            {
//                HandlePartMovment(item, items, nextPointCordinates);
//            }

//        }
//        public void moveAllSnakePartsDirection()
//        {

//            for (int i = 1; i < snakeItems.Count; i++)
//            {
//                snakeItems[i].MovingDirection = snakeItems[i - 1].lastKnownDirectionBeforeCurrent;

//            }
//        }
//        private void HandlePartMovment(GamePiece item, ObservableCollection<GamePiece> items, XYPointer nextPointCordinates)
//        {
//            bool hasReachedNextPosition = item.HasReachedNextPosition;
//            XYPointer nextPos = item.getNextPositionCordinates(item.MovingDirection);


//            if (!hasReachedNextPosition)
//            {

//                XYPointer whereToSetLast = AddOrSubtractSnakeItemToXYpoint(item, nextPos);
//                item.XPosition = whereToSetLast.XProperty;
//                item.YPosition = whereToSetLast.YProperty;
//            }
//            else
//            {

//                var nextItemThatNeedsToBeRemoved = GetSnakeItemAtPosition(item.getNextPositionCordinates(item.MovingDirection), items);
//                if (nextItemThatNeedsToBeRemoved.IsPartOfSnake)
//                {
//                    // end this game
//                }
                
              

//                if (nextItemThatNeedsToBeRemoved.IsFood)
//                {
//                    //remove the apple from the game
//                    items.Remove(nextItemThatNeedsToBeRemoved);
//                    // make apple sound now

//                    AddSnakeLastItemAnotherChild();

//                }
                
//            }
//        }

//        private void AddSnakeLastItemAnotherChild()
//        {
//            Direction d = snakeItems.Last().MovingDirection;
//            XYPointer positionOfLatestSnakeItem = snakeItems.Last().CurrentPointOfItem;
//            XYPointer newSnakeBodyPosition = getPositionPreviousToCurrentSnakeItem(positionOfLatestSnakeItem, snakeItems.Last().MovingDirection);
//            GamePiece s = new GamePiece(newSnakeBodyPosition.XProperty, newSnakeBodyPosition.YProperty);
//            s.MovingDirection = d;
//            s.IsPartOfSnake = true;
//            snakeItems.Add(s);
//            SnakePresenter.ArrayOfItems.Add(s);
//        }

//        private XYPointer getPositionPreviousToCurrentSnakeItem(XYPointer positionOfLatestSnakeItem, Direction direction)
//        {
//            //
//            decimal xToReturn = positionOfLatestSnakeItem.XProperty;
//            decimal yToReturn = positionOfLatestSnakeItem.YProperty;
//            switch (direction)
//            {
//                case Direction.Up:
//                    {
//                        yToReturn += 1;
//                        // if im in 2,2 and moving up, the previos is 2,3
//                    }
//                    break;
//                case Direction.Down:
//                    {
//                        yToReturn -= 1;
//                        // if im in 2,2 and moving down. the previos is 2,1
//                    }
//                    break;
//                case Direction.Left:
//                    {
//                        xToReturn += 1;
//                        //return x+1
//                        // if im in 2,2 and moving left,the previos is 3,2
//                    }
//                    break;
//                case Direction.Right:
//                    {
//                        xToReturn -= 1;
//                        // if im in 2,2 and moving right. the previos is 1,2

//                    }
//                    break;
//                case Direction.None:
//                    break;
//                default:
//                    break;
//            }
//            return new XYPointer() { XProperty = xToReturn, YProperty = yToReturn };
//        }

//        private XYPointer AddOrSubtractSnakeItemToXYpoint(GamePiece last, XYPointer nextPos)
//        {
//            return new XYPointer() { XProperty = HandleXProperty(last, nextPos.XProperty), YProperty = HandleYProperty(last, nextPos.YProperty) };
//        }

//        private decimal HandleXProperty(GamePiece last, decimal p)
//        {
//            decimal xToReturn = last.XPosition;
//            if (last.XPosition == p)
//            {
//                return xToReturn;
//            }
//            if (xToReturn > p)
//            {
//                decimal diff = xToReturn - p;
//                if (diff > 1)
//                {
//                    xToReturn = p;
//                }
//                else
//                {
//                    xToReturn -= 0.01m;
//                }
//            }
//            else
//            {
//                decimal diff = p - xToReturn;
//                if (diff > 1)
//                {
//                    xToReturn = p;
//                }
//                else
//                {
//                    xToReturn += 0.01m;
//                }
//                //last.Xposition<p
//            }
//            return xToReturn;
//        }

//        private decimal HandleYProperty(GamePiece last, decimal p)
//        {
//            decimal yToReturn = last.YPosition;
//            if (yToReturn == p)
//            {
//                return yToReturn;
//            }
//            if (yToReturn > p)
//            {
//                decimal diff = yToReturn - p;
//                if (diff > 1)
//                {
//                    yToReturn = p;
//                }
//                else
//                {
//                    yToReturn -= 0.01m;
//                }
//            }
//            else
//            {
//                decimal diff = p - yToReturn;
//                if (diff > 1)
//                {
//                    yToReturn = p;
//                }
//                else
//                {
//                    yToReturn += 0.01m;
//                }
//                //last.Xposition<p
//            }
//            return yToReturn;
//        }

//        /// <summary>
//        /// old method, need to change it
//        /// </summary>
//        /// <param name="items"></param>
//        /// <param name="nextPointCordinates"></param>
//        private void MoveSnakeByUser(ObservableCollection<GamePiece> items, XYPointer nextPointCordinates)
//        {
//            bool shuoldKeepLastItem = false;

//            //first item of snake -- only one that has direction
//            var head = items.First((m) => m.positionInSnake == 1);

//            //last item of snake
//            var last = GetSnakeItemWithLargestPositionInSnakeScore(items);
//            var nextItemThatNeedsToBeRemoved = GetSnakeItemAtPosition(head.getNextPositionCordinates(head.MovingDirection), items);
//            if (nextItemThatNeedsToBeRemoved.IsPartOfSnake)
//            {
//                KillGame();
//            }

//            if (nextItemThatNeedsToBeRemoved.IsFood)
//            {
//                shuoldKeepLastItem = true;
//            }

//            //add new item infront of the first
//            if (nextPointCordinates != null)
//            {
//                //AI MODE
//                decimal FinalNextXposition = nextPointCordinates.XProperty;
//                decimal FinalNextYposition = nextPointCordinates.YProperty;

//            }
//            else
//            {
//                decimal FinalNextXposition = head.getNextPositionCordinates(head.MovingDirection).XProperty;
//                decimal FinalNextYposition = head.getNextPositionCordinates(head.MovingDirection).YProperty;
//            }

//            //make next snake item, we will change his xPosition and Yposition next
//            GamePiece snakeItemToBeInserted = new GamePiece(head.XPosition, head.YPosition);
//            snakeItemToBeInserted = new GamePiece(head.getNextPositionCordinates(head.MovingDirection).XProperty, head.getNextPositionCordinates(head.MovingDirection).YProperty);
//            snakeItemToBeInserted.IsPartOfSnake = true;
//            snakeItemToBeInserted.IsHead = true;
//            snakeItemToBeInserted.positionInSnake = 1;
//            snakeItemToBeInserted.MovingDirection = head.MovingDirection;



//            // in case we are eating an apple, no need to remove the last item, since we just need to grow.
//            if (!shuoldKeepLastItem)
//            {

//                //remove last
//                XYPointer lastCordinates = new XYPointer() { XProperty = last.XPosition, YProperty = last.YPosition };
//                items.Remove(last);
//                GamePiece s = new GamePiece(lastCordinates.XProperty, lastCordinates.YProperty);
//                items.Add(s);

//            }
//            else
//            {
//                UpdateAppleSound();

//                last.IsPartOfSnake = true;
//                last.IsHead = false;
//                last.positionInSnake++;
//                last.MovingDirection = Direction.None;
//                last.changeAllProperties();

//            }

//            //make the head be part of body
//            head.IsHead = false;
//            head.IsPartOfSnake = true;
//            head.positionInSnake++;
//            head.MovingDirection = Direction.None;
//            head.changeAllProperties();

//            //remove the item thta is currently infron of the head of the snake

//            items.Remove(nextItemThatNeedsToBeRemoved);

//            //insert the item i want to be infront of the head of the snake
//            items.Add(snakeItemToBeInserted);
//            // updateSnakeItemToFinalCordinates(FinalNextXposition, FinalNextYposition, snakeItemToBeInserted);  
//        }

//        private void updateSnakeItemToFinalCordinates(decimal FinalNextXposition, decimal FinalNextYposition, GamePiece snakeItemToBeInserted)
//        {
//            while ((FinalNextXposition != snakeItemToBeInserted.XPosition) || (FinalNextYposition != snakeItemToBeInserted.YPosition))
//            {
//                if (FinalNextYposition == snakeItemToBeInserted.YPosition)
//                {
//                }
//                else if (FinalNextYposition > snakeItemToBeInserted.YPosition)
//                {
//                    snakeItemToBeInserted.YPosition += 0.001m;
//                }
//                else
//                {
//                    snakeItemToBeInserted.YPosition -= 0.001m;
//                }

//                if (FinalNextXposition == snakeItemToBeInserted.XPosition)
//                {
//                }
//                else if (FinalNextXposition > snakeItemToBeInserted.XPosition)
//                {
//                    snakeItemToBeInserted.XPosition += 0.001m;
//                }
//                else
//                {
//                    snakeItemToBeInserted.XPosition -= 0.001m;
//                }
//            }
//        }

//        private void UpdateAppleSound()
//        {
//            SnakePresenter.DidNotEatAppleLastMove = false;

//            ThreadPool.QueueUserWorkItem((o) =>
//            {
//                Thread.Sleep(1000);
//                SnakePresenter.DidNotEatAppleLastMove = true;
//            });
//        }

//        private void KillGame()
//        {
//            SnakePresenter.IsGameRunning = false;
//        }

//        private GamePiece GetSnakeItemWithLargestPositionInSnakeScore(ObservableCollection<GamePiece> items)
//        {
//            GamePiece largestSnakeItem = new GamePiece(0, 0);


//            foreach (GamePiece item in items)
//            {
//                if (item.positionInSnake > largestSnakeItem.positionInSnake)
//                {
//                    largestSnakeItem = item;
//                }
//            }
//            return largestSnakeItem;
//        }
//        private GamePiece GetSnakeItemAtPosition(XYPointer nextPoint, ObservableCollection<GamePiece> items)
//        {
//            foreach (var item in items)
//            {
//                if (item.YPosition == nextPoint.YProperty)
//                {
//                    if (item.XPosition == nextPoint.XProperty)
//                    {
//                        return item;
//                    }
//                }
//            }

//            //error,this should not occur
//            return new GamePiece(-1, -1);

//        }
//        public void calcWinner()
//        {
//            //
//            return;
//        }
//        public void calcError()
//        {
//            //
//            return;
//        }

//    }
//}
