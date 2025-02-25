using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp
{
    public class UpdateController : MonoBehaviour, IStartGameListener
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        private void Awake()
        {
            RegisterExistingUpdatables();
        }

        private void RegisterExistingUpdatables()
        {
            var existingUpdatables = FindObjectsOfType<MonoBehaviour>(true).OfType<IUpdatable>().ToArray();
            foreach (var updatable in existingUpdatables)
            {
                RegisterUpdatable(updatable);
            }

        }
        public void RegisterUpdatable(IUpdatable updatable)
        {
            if (!_updatables.Contains(updatable))
            {
                _updatables.Add(updatable);
            }
        }

        public void StartGame()
        {
            RegisterExistingUpdatables();
        }

        void Update()
        {
            if (PauseManager.IsPaused == false)
            {
                foreach (var updatable in _updatables)
                {
                    updatable.CustomUpdate();
                }
            }
        }
    }
}

