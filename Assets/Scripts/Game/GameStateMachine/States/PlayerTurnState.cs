using Game.Board;

namespace Game.GameStateMachine.States
{
    public class PlayerTurnState: IState
    {
        private BoardInteractions _boardInteractions;
        public void Enter()
        {
            _boardInteractions.EnablePlayerInputs(true);
            
        }

        public void Exit()
        {
            _boardInteractions.EnablePlayerInputs(false);
        }
    }
}