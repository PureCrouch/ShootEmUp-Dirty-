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

    public interface IStartGameListener: IGameStateListener
    {
        void StartGame();
    }

    public interface IPauseGameListener : IGameStateListener
    {
        void PauseGame();   
    }

    public interface IResumeGameListener : IGameStateListener
    {
        void ResumeGame();
    }

    public interface IFinishGameListener : IGameStateListener
    {
        void FinishGame();
    }

    public interface IUpdatable
    {
        void CustomUpdate();
    }

    public interface IFixedUpdatable
    {
        void CustomFixedUpdate();
    }
}