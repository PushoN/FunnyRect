using System;
using UnityEngine;

namespace BringMvc.RectGame.Views
{
    public interface IFunnyRectView
    {
        #region Events

        event EventHandler RotationEnd;

        event EventHandler JumpEnd;

        event EventHandler Clicked;

        #endregion

        #region Methods

        void EnableInput();

        void DisableInput();

        void Rotate();

        void Jump();

        void ChangeColor(Color color);

        #endregion
    }
}