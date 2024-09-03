namespace Game.Tiles
{
    public class GridTile<T>
    {
        private GridSystem<GridTile<T>> _grid;
        private int _x;
        private int _y;

        public GridTile(int x, int y, GridSystem<GridTile<T>> grid)
        {
            _x = x;
            _y = y;
            _grid = grid;
        }
    }
}