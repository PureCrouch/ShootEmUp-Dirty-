using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class UpdateController : MonoBehaviour
    {
        private IUpdatable[] _updatables;

        private void Awake()
        {
            _updatables = transform.parent.GetComponentsInChildren<IUpdatable>();
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

