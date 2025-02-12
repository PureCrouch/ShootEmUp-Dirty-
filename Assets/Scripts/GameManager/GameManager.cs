using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        private void Start()
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            InputManager inputManager = FindObjectOfType<InputManager>();

            if (playerController != null && inputManager != null)
            {
                playerController.SetInputHandler(inputManager);
            }
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}