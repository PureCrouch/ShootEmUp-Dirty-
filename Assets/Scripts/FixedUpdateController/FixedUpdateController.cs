using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    public class FixedUpdateController : MonoBehaviour, IStartGameListener
    {
        private List<IFixedUpdatable> _fixedUpdatables = new List<IFixedUpdatable>();

        private void Awake()
        {
            RegisterExistingFixedUpdatables();
        }

        private void RegisterExistingFixedUpdatables()
        {
            var existingUpdatables = FindObjectsOfType<MonoBehaviour>(true).OfType<IFixedUpdatable>().ToArray();
            foreach (var updatable in existingUpdatables)
            {
                RegisterFixedUpdatable(updatable);
            }

        }
        public void RegisterFixedUpdatable(IFixedUpdatable fixedUpdatable)
        {
            if (!_fixedUpdatables.Contains(fixedUpdatable))
            {
                _fixedUpdatables.Add(fixedUpdatable);
            }
        }

        public void StartGame()
        {
            RegisterExistingFixedUpdatables();
        }
        void FixedUpdate()
        {
            if (PauseManager.IsPaused == false)
            {
                foreach (var fixedUpdatable in _fixedUpdatables)
                {
                    fixedUpdatable.CustomFixedUpdate();
                }
            }
        }
    }
}

