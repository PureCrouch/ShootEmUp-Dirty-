using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp
{
    public class FixedUpdateController : MonoBehaviour
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

        void FixedUpdate()
        {
            foreach (var fixedUpdatable in _fixedUpdatables)
            {
                fixedUpdatable.CustomFixedUpdate();
            }
        }
    }
}

