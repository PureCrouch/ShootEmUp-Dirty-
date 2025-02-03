using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public abstract class WeaponSettings : MonoBehaviour
    {
        [SerializeField] private BulletConfig bulletConfig;
        public BulletConfig GetBulletConfig()
        {
            return bulletConfig;
        }
    }
}