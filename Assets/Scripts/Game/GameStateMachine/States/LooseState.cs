using Game.FX;

namespace Game.GameStateMachine.States
{
    public class LooseState : IState
    {
        private readonly EndGamePanelView _endGamePanelView;

        public LooseState(EndGamePanelView endGamePanelView) => _endGamePanelView = endGamePanelView;

        public void Enter() => _endGamePanelView.ShowEndGamePanel(false);

        public void Exit() { }
    }
}