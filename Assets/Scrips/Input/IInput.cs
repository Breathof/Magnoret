using System;
namespace InputManager
{
    public interface IInput
    {
        bool Up();
        bool Down();
        bool Right();
        bool Left();
        bool Idle();
    }
}
