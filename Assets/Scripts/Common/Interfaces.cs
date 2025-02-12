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

    public interface IGameStateListener
    {

    }

    public interface IStartGameListener
    {
        void StartGame();   
    }

    public interface IPauseGameListener
    {
        void PauseGame();   
    }

    public interface IResumeGameListener
    {
        void ResumeGame();
    }

    public interface IFinishGameListner
    {
        void FinishGame();
    }
}