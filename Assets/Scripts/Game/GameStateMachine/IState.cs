using Game.Tiles;

namespace Game.GameStateMachine
{
    public interface IState
    {
        void Enter();
      
        void Exit();
    }
}