using System;
namespace InputManager
{
    public class Factory
    {
        private Keyboard Keyboard;

        private TouchScreen Screen;

        public Factory()
        {
        }

        public Keyboard GetKeyboard()
        {
            if (Keyboard != null)
            {
                return Keyboard;
            }
            return new Keyboard();
        }

        public TouchScreen GetTouchScreen()
        {
            if (Screen != null)
            {
                return Screen;
            }
            return new TouchScreen();
        }
    }
}
