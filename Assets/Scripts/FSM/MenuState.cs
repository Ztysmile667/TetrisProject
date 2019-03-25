using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState {

	private void Awake()
	{
		stateID = StateID.Menu;
		//转换状态
		AddTransition(Transition.StartButtonClick, StateID.Play);
	}
	/// <summary>
	/// 当我们进入状态的时候会调用的方法
	/// </summary>
	public override void DoBeforeEntering()
	{
		ctrl.view.ShowMenu();
		ctrl.cameraManager.ZoomOut();
	}
	/// <summary>
	/// 离开状态的时候的方法
	/// </summary>
	public override void DoBeforeLeaving()
	{
		ctrl.view.HideMenu();
	}

	public void OnStartButtonClick()
	{
		ctrl.audioManager.PlayCursor();
		fsm.PerformTransition(Transition.StartButtonClick);
	}

	public void OnRankButtonClick()
	{
		ctrl.view.ShowRankUI(ctrl.model.HighScore,ctrl.model.Score,ctrl.model.NumbersGame);
	}

	public void OnDestoryButtonClick()
	{
		ctrl.model.CleraData();
		OnRankButtonClick();
	}

	public void OnRestartButtonClick()
	{
		ctrl.model.Restart();
		ctrl.gameManager.ClearShape();
		fsm.PerformTransition(Transition.StartButtonClick);
	}
}
