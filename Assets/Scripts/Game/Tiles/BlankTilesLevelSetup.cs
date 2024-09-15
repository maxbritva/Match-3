using Level;

namespace Game.Tiles
{
    public class BlankTilesLevelSetup
    {
        public bool[,] Blanks { get; private set; }
        
        public void Generate(LevelConfiguration levelConfiguration)
        {
            Blanks = new bool[levelConfiguration.GridWidth, levelConfiguration.GridWidth];
            for (int i = 0; i < levelConfiguration.BlankTilesLayout.Count; i++) 
                Blanks[levelConfiguration.BlankTilesLayout[i].XPos, levelConfiguration.BlankTilesLayout[i].YPos] = true;
        }
    }
}