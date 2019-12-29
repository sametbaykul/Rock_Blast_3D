using UnityEngine;
using System.Collections.Generic;

namespace BallBlast
{

    // --------------------------------------------------
    // LevelData.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Level Data", menuName = "Data/Level Data", order = 1)]

    public class LevelData : ScriptableObject
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        [Header("General Config")]
        public int level = 1;
    }
}
