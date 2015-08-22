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
using SnakesWpfed.Presenters;
using SnakesWpfed.Model;
using System.Windows.Controls;
using System.Threading;
using System.Diagnostics;
using SnakesWpfed.Presenters.SnakesPresenters;
using SnakesWpfed.Model.SnakePieces;


namespace SnakesWpfed.Common
{
    public enum GameMode { Solo, AI };
    public class NewSnakesGameManager : IGameManager
    {
        public List<SnakePiecePresenter> snakeParts { get; set; }

        private SnakesPresenter SnakePresenter { get; set; }


        public NewSnakesGameManager(SnakesPresenter s)
        {
            ///?
            SnakePresenter = s;

            snakeParts = new List<SnakePiecePresenter>();


        }
        public void setNewGameDirection(Direction e, ObservableCollection<BasePresenterItem> items)
        {
            var head = GetHeadItems(items);
            if (head == null)
            {
                return;
            }
            head.CurrentGamePiece.MovingDirection = e;
        }

        private SnakePiecePresenter GetHeadItems(ObservableCollection<BasePresenterItem> itemsTo)
        {
            var snakePiecePresenterItems = itemsTo.OfType<SnakePiecePresenter>();
            if (!snakePiecePresenterItems.Any())
            {
                DebugHelper.WriteLog("error occured, not head items were found, so directino wasn't changed", "NewSnakesGameManager.cs");
                return null;
            }
            var head = snakePiecePresenterItems.FirstOrDefault((m) =>
            {
                return m.IsHead;
            });

            if (head == null)
            {
                return null;
            }
            return head;

        }
        public void CreateFoodItemsInSnakeList(ObservableCollection<BasePresenterItem> items)
        {


        }
        public void CreateHeadOfSnake(ObservableCollection<BasePresenterItem> items)
        {


        }
        public void CreateNewGame(ObservableCollection<BasePresenterItem> Items)
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 6 && j == 6)
                    {
                        Items.Add(new SnakePiecePresenter(new SnakePiece(i, j, Direction.Left),null));
                    }
                    else if (i == j)
                    {
                        Items.Add(new ApplePresenter(new FoodPiece(i, j)));
                    }
                    else
                    {
                        Items.Add(new GrassPresenter(new GrassPiece(i, j)));
                    }

                }
            }
        }
        public void UpdateGameBoard(ObservableCollection<BasePresenterItem> items)
        {

            var snakeItems = SnakePresenter.ArrayOfItems.OfType<SnakePiecePresenter>().ToList();

            for (int i = 0; i < snakeItems.Count(); i++)
            {
                var nextCorindate = snakeItems[i].getNextPointCordinates();
                var gamePiece = snakeItems[i].CurrentGamePiece;


                if (gamePiece.HasReachedNextCordinates == false)
                {
                    MoveSnakeItemToNextXLocation(gamePiece, nextCorindate);
                    MoveSnakeItemToNextYLocation(gamePiece, nextCorindate);
                }
                else
                {
                    tryEat(gamePiece, nextCorindate, SnakePresenter.ArrayOfItems);
                    MoveSnakeItemToNextXLocation(gamePiece, nextCorindate);
                    MoveSnakeItemToNextYLocation(gamePiece, nextCorindate);
                }
            }

        }

        private void tryEat(GamePiece gamePiece, XYPointer nextCorindate, ObservableCollection<BasePresenterItem> items)
        {
            var goingToBeEatten = items.Where((m) =>
              {
                  //get the item that answers "next cordinate".
                  if (m.CurrentGamePiece.XPosition == nextCorindate.XProperty &&
                      m.CurrentGamePiece.YPosition == nextCorindate.YProperty)
                  {
                      return true;
                  }
                  return false;
              });

            var firstOrDeafult = goingToBeEatten.FirstOrDefault();
            if (firstOrDeafult != null)
            {
                if (firstOrDeafult is GrassPresenter)
                {
                    return;
                }
                if (firstOrDeafult is SnakePiecePresenter)
                {
                    //end game
                    DebugHelper.WriteLog("game eneded... bummer", "new snakes game manager");
                    return;
                }


                int index = items.IndexOf(firstOrDeafult);

                SnakePiecePresenter whoToFollow;
                if (snakeParts.Count > 0)
                {
                    whoToFollow = snakeParts.Last();
                }
                else
                {
                    whoToFollow = GetHeadItems(SnakePresenter.ArrayOfItems);
                }


                var snakePiece = new SnakePiece(nextCorindate.XProperty, nextCorindate.YProperty);
                SnakePiecePresenter newSnakepresetner = new SnakePiecePresenter(snakePiece,whoToFollow);
                snakeParts.Add(newSnakepresetner);
                items[index] = newSnakepresetner;

            }
            DebugHelper.WriteLog(string.Format("eatted {0},{1}", nextCorindate.XProperty, nextCorindate.YProperty), "new snakes game manager");
        }




        private bool HasAnyDiff(GamePiece gamePiece, XYPointer nextCorindate)
        {
            return gamePiece.XPosition == nextCorindate.XProperty
                && gamePiece.YPosition == nextCorindate.YProperty;
        }


        public void MoveSnakeItemToNextYLocation(GamePiece p, XYPointer whereToMove)
        {
            decimal snakeYPosition = p.YPosition;
            decimal wantedSnakeYPosition = whereToMove.YProperty;

            if (snakeYPosition != wantedSnakeYPosition)
            {

                if (snakeYPosition > wantedSnakeYPosition)
                {
                    if (snakeYPosition - wantedSnakeYPosition > 1)
                    {
                        p.YPosition = whereToMove.YProperty;
                        return;
                    }
                    p.YPosition -= 0.1m;
                }
                else if (snakeYPosition < wantedSnakeYPosition)
                {
                    if (wantedSnakeYPosition - snakeYPosition > 1)
                    {
                        p.YPosition = whereToMove.YProperty;
                        return;
                    }
                    p.YPosition += 0.1m;
                }
            }
        }

        public void MoveSnakeItemToNextXLocation(GamePiece p, XYPointer whereToMove)
        {
            decimal snakeXPosition = p.XPosition;
            decimal wantedSnakeXPosition = whereToMove.XProperty;

            if (snakeXPosition != wantedSnakeXPosition)
            {

                if (snakeXPosition > wantedSnakeXPosition)
                {
                    if (snakeXPosition - wantedSnakeXPosition > 1)
                    {
                        p.XPosition = whereToMove.XProperty;
                        return;
                    }
                    p.XPosition -= 0.1m;
                }
                else if (snakeXPosition < wantedSnakeXPosition)
                {
                    if (wantedSnakeXPosition - snakeXPosition > 1)
                    {
                        p.XPosition = whereToMove.XProperty;
                        return;
                    }
                    p.XPosition += 0.1m;
                }
            }
        }
        private void MoveSnakeByUserNew(ObservableCollection<GamePiece> items, XYPointer nextPointCordinates)
        {




        }
        public void moveAllSnakePartsDirection()
        {


        }
        private void HandlePartMovment(GamePiece item, ObservableCollection<GamePiece> items, XYPointer nextPointCordinates)
        {

        }

        private void AddSnakeLastItemAnotherChild()
        {

        }

        private void getPositionPreviousToCurrentSnakeItem(XYPointer positionOfLatestSnakeItem, Direction direction)
        {
            //

        }

        private void AddOrSubtractSnakeItemToXYpoint(GamePiece last, XYPointer nextPos)
        {

        }

        private void HandleXProperty(GamePiece last, decimal p)
        {

        }

        private void HandleYProperty(GamePiece last, decimal p)
        {

        }



        private void updateSnakeItemToFinalCordinates(decimal FinalNextXposition, decimal FinalNextYposition, GamePiece snakeItemToBeInserted)
        {

        }

        private void UpdateAppleSound()
        {

        }

        private void KillGame()
        {

        }

        private void GetSnakeItemWithLargestPositionInSnakeScore(ObservableCollection<GamePiece> items)
        {

        }
        private void GetSnakeItemAtPosition(XYPointer nextPoint, ObservableCollection<GamePiece> items)
        {


        }
        public void calcWinner()
        {
            //
            return;
        }
        public void calcError()
        {
            //
            return;
        }



        public Direction GetCurrentHeadDirection()
        {
            var head = GetHeadItems(SnakePresenter.ArrayOfItems);
            return head.CurrentGamePiece.MovingDirection;
        }
    }
}
