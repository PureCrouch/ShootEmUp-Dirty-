using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
        public struct BulletArgs
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
}