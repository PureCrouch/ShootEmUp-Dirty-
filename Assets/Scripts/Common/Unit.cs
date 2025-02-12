using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected MoveComponent moveComponent;
    protected WeaponComponent weaponComponent;
    protected HitPointsComponent hitPointsComponent;
}
