using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp
{
    public class UpdateController : MonoBehaviour
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
                RegisterFixedUpdatable(updatable);
            }

        }
        public void RegisterFixedUpdatable(IUpdatable fixedUpdatable)
        {
            if (!_updatables.Contains(fixedUpdatable))
            {
                _updatables.Add(fixedUpdatable);
            }
        }

        void Update()
        {
            foreach (var updatable in _updatables)
            {
                updatable.CustomUpdate();
            }
        }
    }
}

