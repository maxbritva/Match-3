using Level;

namespace Game.Tiles
{
    public class GenerateBlankTiles
    {
        public bool[,] Blanks { get; private set; }
        
        public void Generate(LevelConfiguration levelConfiguration)
        {
            Blanks = new bool[levelConfiguration.LevelGridWidth, levelConfiguration.LevelGridWidth];
            for (int i = 0; i < levelConfiguration.BlockTilesLayout.Count; i++) 
                Blanks[levelConfiguration.BlockTilesLayout[i].XPos, levelConfiguration.BlockTilesLayout[i].YPos] = true;
        }
    }
}