using System;
using UnityEngine;

namespace Game.Tiles
{
   [Serializable]
    public class BlankTile
    {
        [SerializeField] private int xPosition;
        [SerializeField] private int yPosition;

        public int XPos => xPosition;
        public int YPos => yPosition;
    }
}