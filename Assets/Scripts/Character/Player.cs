using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class Player : MonoBehaviour
    {
        protected MoveComponent moveComponent;
        protected WeaponComponent weaponComponent;
        protected HitPointsComponent hitPointsComponent;

        protected void Awake()
        {
            moveComponent = GetComponent<MoveComponent>();
            weaponComponent = GetComponent<WeaponComponent>();
            hitPointsComponent = GetComponent<HitPointsComponent>();
        }
    }
}