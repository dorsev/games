using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnakesWpfed.Model.SnakePieces;
using System.Diagnostics;
using SnakesWpfed.Model;

namespace SnakesWpfed.Presenters.SnakesPresenters
{
    public class SnakePiecePresenter : BasePresenterItem
    {
        public SnakePiecePresenter WhoToFollowSnakePresenter { get; set; }

        [DebuggerDisplay("IsHead = {IsHead}")]
        public bool IsHead
        {
            get
            {

                if (WhoToFollowSnakePresenter == null) return true;
                return false;
            }
        }

        public Model.XYPointer getNextPointCordinates()
        {
            if (WhoToFollowSnakePresenter != null)
            {
                WhoToFollowSnakePresenter.CurrentGamePiece.getNextPointCordinates();
                return new Model.XYPointer() { XProperty = WhoToFollowSnakePresenter.CurrentGamePiece.nextCertifaedPoint.XProperty, YProperty = WhoToFollowSnakePresenter.CurrentGamePiece.nextCertifaedPoint.YProperty };
            }

            return base.CurrentGamePiece.getNextPointCordinates();
        }

        public SnakePiecePresenter(GamePiece g, SnakePiecePresenter WhoToFollow)
            : base(g)
        {
            WhoToFollowSnakePresenter = WhoToFollow;
        }
    }
}
