using System.Collections.Generic;
using Game.Tiles;

namespace Game.GameLoop
{
    public class MatchResult
    {
        private List<Tile> _connectedTiles;
        private MatchDirection _matchDirection;

        public List<Tile> ConnectedTiles => _connectedTiles;
        public MatchDirection MatchDirection => _matchDirection;

        public MatchResult(List<Tile> connectedTiles, MatchDirection matchDirection)
        {
            _connectedTiles = connectedTiles;
            _matchDirection = matchDirection;
        }
    }
}