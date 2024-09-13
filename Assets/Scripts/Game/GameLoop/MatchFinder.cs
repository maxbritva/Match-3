﻿using System.Collections.Generic;
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
                    _potionsToRemove.AddRange(matchTiles.ConnectedTiles);
                    foreach (var connectedTile in matchTiles.ConnectedTiles) 
                        connectedTile.SetMatch(true);
                    hasMatches = true;
                }
            }   
            return hasMatches;
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