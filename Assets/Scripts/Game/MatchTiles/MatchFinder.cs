﻿using System.Collections.Generic;
using Game.Grid;
using Game.Tiles;
using UnityEngine;

namespace Game.MatchTiles
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
        public List<Tile> TilesToRemove { get;}
        public MatchResult CurrentMatchResult { get; private set; }
        public MatchFinder() => TilesToRemove = new List<Tile>();
        
        public bool CheckBoardForMatches(GridSystem grid)
        {
            var hasMatches = false;

            ClearTilesToRemoveList();
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var tile = grid.GetValue(x, y);
                    if(tile == null) continue;
                    if (tile.IsInteractable == false && tile.IsMatched) continue;
                    var matchTiles = FindConnectedTiles(tile, grid);
                    if (matchTiles.ConnectedTiles.Count < 3) continue;
                    CurrentMatchResult = matchTiles;
                    TilesToRemove.AddRange(matchTiles.ConnectedTiles);
                    foreach (var connectedTile in matchTiles.ConnectedTiles) 
                        connectedTile.SetMatch(true);
                    hasMatches = true;
                }
            }   
            return hasMatches;
        }
        private MatchResult FindConnectedTiles(Tile tile, GridSystem grid)
        {
            List<Tile> connectedTiles = new List<Tile>();
            connectedTiles.Add(tile);
            var tileGridPosition = grid.WorldToGrid(tile.gameObject.transform.position);
            
            CheckDirection(tileGridPosition, Vector2Int.right, grid, tile, connectedTiles); 
            CheckDirection(tileGridPosition, Vector2Int.left, grid, tile, connectedTiles);

            if (connectedTiles.Count == 3)
                return CheckForMultiMatch(connectedTiles, grid, Vector2Int.right, MatchDirection.Horizontal);
            
            if (connectedTiles.Count > 3)
                return CheckForMultiMatch(connectedTiles, grid, Vector2Int.right, MatchDirection.LongHorizontal);
            
            connectedTiles.Clear();
            connectedTiles.Add(tile);
            
            CheckDirection(tileGridPosition, Vector2Int.up, grid, tile, connectedTiles);
            CheckDirection(tileGridPosition, Vector2Int.down, grid, tile, connectedTiles); 

            if (connectedTiles.Count == 3)
                return CheckForMultiMatch(connectedTiles, grid, Vector2Int.up, MatchDirection.Vertical);
            if (connectedTiles.Count > 3)
                return CheckForMultiMatch(connectedTiles, grid, Vector2Int.up, MatchDirection.LongVertical);
            
            connectedTiles.Clear();
            return new MatchResult(connectedTiles, MatchDirection.None);
        }
        private MatchResult CheckForMultiMatch(List<Tile> connectedTiles, GridSystem grid, Vector2Int direction, MatchDirection matchDirection)
        {
            foreach (var tile in connectedTiles)
            {
                var position = tile.gameObject.transform.position;
                List<Tile> multiConnectedTiles = new List<Tile>();
                CheckDirection(grid.WorldToGrid(position), direction, grid, tile, multiConnectedTiles);
                CheckDirection(grid.WorldToGrid(position), direction * -1, grid, tile, multiConnectedTiles);

                if (multiConnectedTiles.Count <= 2) continue;
                multiConnectedTiles.AddRange(connectedTiles);
                return new MatchResult(multiConnectedTiles, MatchDirection.Multiply);
            }
            return new MatchResult(connectedTiles, matchDirection);
        }

        private void CheckDirection(Vector2Int position, Vector2Int direction, GridSystem grid, Tile  tile, List<Tile> connectedTiles)
        {
            int x = position.x + direction.x;
            int y = position.y + direction.y;
            while ( grid.IsValid(x,y))
            {
                var neighbourTile = grid.GetValue(x, y);
                if(neighbourTile == null)
                    break;
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

        public void ClearTilesToRemoveList()
        {
            for (int i = 0; i < TilesToRemove.Count; i++) TilesToRemove[i].SetMatch(false);
            TilesToRemove.Clear();
        }

        public void ClearCurrentMatchResult() => CurrentMatchResult.ConnectedTiles.Clear();
    }
}