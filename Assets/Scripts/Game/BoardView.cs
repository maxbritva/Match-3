using System;
using Game.Tiles;
using UnityEngine;
using Zenject;

namespace Game
{
    public class BoardView : MonoBehaviour
    {
        private Board _board;
        private GridSystem<GridTile<Tile>> _grid;

        private void Start()
        {
            _grid = _board.Grid;
        }

        [Inject] private void Construct(Board board)
        {
            _board = board;
        }
    }
}