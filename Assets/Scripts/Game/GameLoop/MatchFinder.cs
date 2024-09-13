using System.Collections.Generic;
using Game.Grid;
using Game.Tiles;
using UnityEngine;

namespace Game.GameLoop
{
    public enum MatchDirection
    {
        Horizontal,
        Vertical,
        LongHorizontal,
        LongVertical,
        Multiply,
        None
    }
    public class MatchFinder
    {
        private readonly Vector2Int _rightDirection = new Vector2Int(1,0);
        private readonly Vector2Int _leftDirection = new Vector2Int(-1,0);
        private readonly Vector2Int _upDirection = new Vector2Int(0,1);
        private readonly Vector2Int _downDirection = new Vector2Int(0,-1);
        
        private List<Tile> _potionsToRemove = new List<Tile>();

        public bool CheckBoard(Board.GameBoard gameBoard, GridCoordinator coordinator)
        {
            var hasMatches = false;

            for (int x = 0; x < gameBoard.GridWidth; x++)
            {
                for (int y = 0; y < gameBoard.GridHeight; y++)
                {
                    var tile = gameBoard.Grid.GetValue(x, y);
                    if (tile.IsInteractable == false && tile.IsMatched) continue;
                    var matchTiles = IsConnected(tile, coordinator, gameBoard.Grid);
                    if (matchTiles.ConnectedTiles.Count < 3) continue;
                    _potionsToRemove.AddRange(matchTiles.ConnectedTiles);
                    foreach (var connectedTile in matchTiles.ConnectedTiles) 
                        connectedTile.SetMatch(true);
                    hasMatches = true;
                }
            }   
            return hasMatches;
        }

        private MatchResult IsConnected(Tile tile, GridCoordinator coordinator, GridSystem gridSystem)
        {
            List<Tile> connectedTiles = new List<Tile>();
            connectedTiles.Add(tile);
            var tileGridPosition = coordinator.WorldToGrid(tile.gameObject.transform.position);
            
            CheckDirection(tileGridPosition, _rightDirection, gridSystem, tile, connectedTiles); //right
            CheckDirection(tileGridPosition, _leftDirection, gridSystem, tile, connectedTiles); //left

            if (connectedTiles.Count == 3)
                return new MatchResult(connectedTiles, MatchDirection.Horizontal);



            if (connectedTiles.Count > 3)
                return new MatchResult(connectedTiles, MatchDirection.LongHorizontal);

            connectedTiles.Clear();
            connectedTiles.Add(tile);
            CheckDirection(tileGridPosition, _upDirection, gridSystem, tile, connectedTiles); //up
            CheckDirection(tileGridPosition, _downDirection, gridSystem, tile, connectedTiles); //down

            if (connectedTiles.Count == 3)
                return new MatchResult(connectedTiles, MatchDirection.Vertical);


            if (connectedTiles.Count > 3)
                return new MatchResult(connectedTiles, MatchDirection.LongVertical);

            return new MatchResult(connectedTiles, MatchDirection.None);
        }

        private void CheckDirection(Vector2Int position, Vector2Int direction, GridSystem gridSystem, Tile  tile, List<Tile> connectedTiles)
        {
            int x = position.x + direction.x;
            int y = position.y + direction.y;
            while ( gridSystem.IsValid(x,y))
            {
                var neighbourTile = gridSystem.GetValue(x, y);
               
                if (neighbourTile.IsInteractable && neighbourTile.IsMatched == false && tile.GetType() == neighbourTile.GetType())
                {
                    connectedTiles.Add(neighbourTile);

                    x += direction.x;
                    y += direction.y;
                }
                else
                    break;
            }
        }
    }
}