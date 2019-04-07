using System;
using UnityEngine;

namespace InputManager
{
    public class TouchScreen: IInput
    {
        public TouchScreen()
        {
        }
        public bool Right()
        {
            return Input.GetTouch(0).position.x > Screen.width/2;
        }

        public bool Left()
        {
            return Input.GetTouch(0).position.x < Screen.width / 2;
        }

        public bool Up()
        {
            return Input.GetTouch(0).position.y < Screen.height / 2;
        }

        public bool Down()
        {
            return Input.GetTouch(0).position.y < Screen.height / 2;
        }

        public bool Idle()
        {
            return Input.touches.Length == 0;
        }
    }
}
