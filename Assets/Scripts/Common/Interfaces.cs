using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public interface IUserInputListener
    {
        void UserInputReceived(int input);
        void FireInputReceived();
    }
    public interface IFireable
    {
        void Fire(Vector2 position, Vector2 direction, BulletConfig bulletConfig);
    }
}