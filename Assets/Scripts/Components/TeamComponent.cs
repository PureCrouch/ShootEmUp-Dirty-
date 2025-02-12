using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour
    {
        public bool IsPlayer
        {
            get { return isPlayer; }
        }
        
        [SerializeField] private bool isPlayer;
    }
}