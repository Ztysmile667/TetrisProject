using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverState : FSMState {

	private void Awake()
	{
		stateID = StateID.GameOver;
	}

}
