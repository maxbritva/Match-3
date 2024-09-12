using Game.Grid;
using Game.Tiles;
using Game.Utils;
using Level;
using UnityEngine;
using VContainer;
using GridSystem = Game.Grid.GridSystem;

namespace Game.Board
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private LevelConfiguration _levelConfiguration;
        public GridSystem Grid => _grid;
        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        public float CellSize { get; private set; }
        public Vector3 OriginPosition { get; private set; }

        private GridSystem _grid;
        private GridCoordinator _gridCoordinator;
        private TileCreator _tileCreator;
        private GenerateBlankTiles _generateBlankTiles;
        private bool _isDebugging;
        private GameDebug _gameDebug;
        private SetupCamera _setupCamera;

        private void Start()
        {
            GridWidth = _levelConfiguration.LevelGridWidth;
            GridHeight = _levelConfiguration.LevelGridHeight;;
            CellSize = 1f;
            OriginPosition = Vector3.zero;
            if(_isDebugging)
                _gameDebug.ShowDebugGrid(GridWidth, GridHeight, CellSize, OriginPosition, transform);
            _grid = new GridSystem(GridWidth, GridHeight, CellSize, OriginPosition,_gridCoordinator);
            _generateBlankTiles.Generate(_levelConfiguration);
           InitializeBoard();
           _setupCamera = new SetupCamera(this, false);
        }
        public void Destroy(GameObject target) => Destroy(target, 0.1f);
        private void InitializeBoard()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    if (_generateBlankTiles.Blanks[x, y])
                    {
                        var tile = _tileCreator.CreateBlankTile(_gridCoordinator.GridToWorldCenter(x, y, CellSize, OriginPosition), transform);
                        Grid.SetValue(x,y, tile);
                    }
                    else
                    {
                        var tile = _tileCreator.CreateTile(_gridCoordinator.GridToWorldCenter(x, y, CellSize, OriginPosition), transform);
                        Grid.SetValue(x,y, tile);
                    }
                }
            }
        }

       [Inject] private void Construct(GridCoordinator gridCoordinator, GameDebug gameDebug, TileCreator tileCreator, GenerateBlankTiles blankTiles)
        {
            _gridCoordinator = gridCoordinator;
            _gameDebug = gameDebug;
            _generateBlankTiles = blankTiles;
            _tileCreator = tileCreator;
        }
    }
}