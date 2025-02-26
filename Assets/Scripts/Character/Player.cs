using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ShootEmUp
{
    
    public class Player : Unit
    {
        private void Start()
        {
            moveComponent = gameObject.GetComponent<MoveComponent>();
            weaponComponent = gameObject.GetComponent<WeaponComponent>();
            hitPointsComponent = gameObject.GetComponent<HitPointsComponent>();
        }
    }
}