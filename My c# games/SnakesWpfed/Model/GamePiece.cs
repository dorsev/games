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

namespace SnakesWpfed.Model
{
    public enum Direction { Up, Down, Left, Right, None }
    public class GamePiece : INotifyPropertyChanged
    {
        #region ctor

        public GamePiece(decimal x, decimal y)
        {
            XPosition = x;
            YPosition = y;
            MovingDirection = Direction.None;
            _nextCertifafedPoint = new XYPointer() { XProperty = XPosition, YProperty = YPosition };
        }

        public GamePiece(decimal x, decimal y, Direction d)
        {
            XPosition = x;
            YPosition = y;
            MovingDirection = d;
            _nextCertifafedPoint = new XYPointer() { XProperty = XPosition, YProperty = YPosition };
        }

        private XYPointer _nextCertifafedPoint;

        public XYPointer nextCertifaedPoint
        {
            get
            {
                return _nextCertifafedPoint;
            }


            set
            {
                _nextCertifafedPoint = value;
            }

        }

        private bool ShouldUpdateLocationOfNextPoint(decimal xpositionToReturn, decimal ypositionToReturn)
        {
            if (xpositionToReturn - Math.Floor(xpositionToReturn) > 0)
            {
                return false;
            }
            if (ypositionToReturn - Math.Floor(ypositionToReturn) > 0)
            {
                return false;

            }

            return true;
        }

        public virtual XYPointer getNextPointCordinates()
        {
            XYPointer nextPoint = new XYPointer();
            decimal xpositionToReturn = XPosition;
            decimal ypositionToReturn = YPosition;

            if (ShouldUpdateLocationOfNextPoint(xpositionToReturn, ypositionToReturn))
            {

                #region case directions...

                switch (MovingDirection)
                {
                    // when i move in need to change the cell i just left. 
                    case Direction.Up:
                        {
                            ypositionToReturn--;
                            if (ypositionToReturn < 0)
                            {
                                ypositionToReturn = 9;
                            }
                        }
                        break;
                    case Direction.Down:
                        {
                            ypositionToReturn++;
                            if (ypositionToReturn > 9)
                            {
                                ypositionToReturn = 0;
                            }
                        }
                        break;
                    case Direction.Left:
                        {
                            xpositionToReturn--;
                            if (xpositionToReturn < 0)
                            {
                                xpositionToReturn = 9;
                            }
                        }
                        break;
                    case Direction.Right:
                        {
                            xpositionToReturn++;
                            if (xpositionToReturn > 9)
                            {
                                xpositionToReturn = 0;
                            }
                        }
                        break;
                    case Direction.None:
                        {

                        }
                        break;
                    default:
                        break;
                }
                #endregion

                nextCertifaedPoint = new XYPointer()
                {
                    XProperty = xpositionToReturn,
                    YProperty = ypositionToReturn
                };
            }




            nextPoint.XProperty = nextCertifaedPoint.XProperty;
            nextPoint.YProperty = nextCertifaedPoint.YProperty;

            return nextPoint;
        }

        #endregion


        #region methods

        public void UpdateAllUIProperties()
        {
            OnPropertyChanged("XPosition");
            OnPropertyChanged("YPosition");
            OnPropertyChanged("MovingDirection");
        }

        public bool HasReachedNextCordinates
        {
            get
            {
                if ((Math.Floor(XPosition) - XPosition == 0) && (Math.Floor(YPosition) - YPosition == 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region members

        private decimal _xPosition;
        private decimal _yPosition;
        private Direction _movingDirection;

        #endregion

        #region properties

        public decimal XPosition
        {
            get
            {
                return _xPosition;
            }
            set
            {
                _xPosition = value;
                OnPropertyChanged("XPosition");
                OnPropertyChanged("YPosition");
            }
        }
        public decimal YPosition
        {
            get
            {
                return _yPosition;
            }
            set
            {
                _yPosition = value;
                OnPropertyChanged("YPosition");
                OnPropertyChanged("XPosition");
            }
        }
        public Direction MovingDirection
        {
            get
            {
                return _movingDirection;
            }
            set
            {
                _movingDirection = value;
                OnPropertyChanged("MovingDirection");
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}
