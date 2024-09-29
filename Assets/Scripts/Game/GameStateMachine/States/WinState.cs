using Game.FX;
using UnityEngine;

namespace Game.GameStateMachine.States
{
    public class WinState: IState
    {
        private EndGamePanelView _endGamePanelView;
        public WinState(EndGamePanelView endGamePanelView) => _endGamePanelView = endGamePanelView;
        public void Enter() => _endGamePanelView.ShowEndGamePanel(true);
        public void Exit() { }
    }
}