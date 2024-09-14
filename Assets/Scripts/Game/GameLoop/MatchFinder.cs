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
        private List<Tile> _potionsToRemove = new List<Tile>();

        public bool CheckBoardForMatches(GridSystem grid)
        {
            var hasMatches = false;

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var tile = grid.GetValue(x, y);
                    if (tile.IsInteractable == false && tile.IsMatched) continue;
                    var matchTiles = IsConnected(tile, grid);
                    if (matchTiles.ConnectedTiles.Count < 3) continue;

                    MatchResult multiMatched = MultiMatch(matchTiles, grid);
                    
                    _potionsToRemove.AddRange(multiMatched.ConnectedTiles);
                    foreach (var connectedTile in multiMatched.ConnectedTiles) 
                        connectedTile.SetMatch(true);
                    hasMatches = true;
                }
            }   
            return hasMatches;
        }

        private MatchResult MultiMatch(MatchResult matchTiles, GridSystem grid)
        {
            switch (matchTiles.MatchDirection)
            {
                case MatchDirection.Horizontal:
                case MatchDirection.LongHorizontal:
                {
                    foreach (var tile in matchTiles.ConnectedTiles)
                    {
                        var position = tile.gameObject.transform.position;
                        List<Tile> multiConnectedTiles = new List<Tile>();
                        CheckDirection(grid.WorldToGrid(position), Vector2Int.up, grid, tile, multiConnectedTiles);
                        CheckDirection(grid.WorldToGrid(position), Vector2Int.down, grid, tile, multiConnectedTiles);

                        if (multiConnectedTiles.Count < 2) continue;
                        Debug.Log("multi horizontal match");
                        multiConnectedTiles.AddRange(matchTiles.ConnectedTiles);
                        return new MatchResult(multiConnectedTiles, MatchDirection.Multiply);
                    }
                    return new MatchResult(matchTiles.ConnectedTiles, matchTiles.MatchDirection);
                }
                case MatchDirection.Vertical:
                case MatchDirection.LongVertical:
                {
                    foreach (var tile in matchTiles.ConnectedTiles)
                    { 
                        List<Tile> multiConnectedTiles = new List<Tile>();
                        var position = tile.gameObject.transform.position;
                    
                        CheckDirection(grid.WorldToGrid(position), Vector2Int.right, grid, tile, multiConnectedTiles);
                        CheckDirection(grid.WorldToGrid(position), Vector2Int.left, grid, tile, multiConnectedTiles);

                        if (multiConnectedTiles.Count < 2) continue;
                        Debug.Log("multi vertical match");
                        multiConnectedTiles.AddRange(matchTiles.ConnectedTiles);
                        return new MatchResult(multiConnectedTiles, MatchDirection.Multiply);
                    }
                    return new MatchResult(matchTiles.ConnectedTiles, matchTiles.MatchDirection);
                }
                default:
                    return null;
            }
        }

        private MatchResult IsConnected(Tile tile, GridSystem grid)
        {
            List<Tile> connectedTiles = new List<Tile>();
            connectedTiles.Add(tile);
            var tileGridPosition = grid.WorldToGrid(tile.gameObject.transform.position);
            
            CheckDirection(tileGridPosition, Vector2Int.right, grid, tile, connectedTiles); 
            CheckDirection(tileGridPosition, Vector2Int.left, grid, tile, connectedTiles);

            if (connectedTiles.Count == 3)
                return new MatchResult(connectedTiles, MatchDirection.Horizontal);
            if (connectedTiles.Count > 3) return new MatchResult(connectedTiles, MatchDirection.LongHorizontal);
            
            connectedTiles.Clear();
            connectedTiles.Add(tile);
            
            CheckDirection(tileGridPosition, Vector2Int.up, grid, tile, connectedTiles);
            CheckDirection(tileGridPosition, Vector2Int.down, grid, tile, connectedTiles); 

            if (connectedTiles.Count == 3)
                return new MatchResult(connectedTiles, MatchDirection.Vertical);
            if (connectedTiles.Count > 3)
                return new MatchResult(connectedTiles, MatchDirection.LongVertical);
            
            connectedTiles.Clear();
            return new MatchResult(connectedTiles, MatchDirection.None);
        }

        private void CheckDirection(Vector2Int position, Vector2Int direction, GridSystem grid, Tile  tile, List<Tile> connectedTiles)
        {
            int x = position.x + direction.x;
            int y = position.y + direction.y;
            while ( grid.IsValid(x,y))
            {
                var neighbourTile = grid.GetValue(x, y);
               
                if (neighbourTile.IsInteractable && neighbourTile.IsMatched == false && tile.tileType == neighbourTile.tileType)
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