using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PauseManager
    {
        public static bool IsPaused { get; private set; }
        public static void SetPause()
        {
            IsPaused = true;
        }
        public static void SetUnpause()
        {
            IsPaused = false;
        }
    }
}

