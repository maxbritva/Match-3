namespace Game.Tiles
{
    public class GridTile<T>
    {
        private GridSystem<GridTile<T>> _grid;
        private int _x;
        private int _y;
        private T _tile;

        public GridTile(int x, int y, GridSystem<GridTile<T>> grid)
        {
            _x = x;
            _y = y;
            _grid = grid;
        }
        public void SetValue(T tile) => _tile = tile;
    }
}