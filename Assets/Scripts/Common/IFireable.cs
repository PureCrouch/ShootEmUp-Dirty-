using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public interface IFireable
    {
        void Fire(Vector2 position, Vector2 direction, BulletConfig bulletConfig);
    }
}