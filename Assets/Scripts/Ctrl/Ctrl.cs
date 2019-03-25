using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ctrl属于一个桥梁，连接Model和View层，取数据的时候从Model层取，需要显示东西的时候用View中显示的方法显示（Model和View层不交互）
/// </summary>
public class Ctrl : MonoBehaviour {
	[HideInInspector]
	public Model model;
	[HideInInspector]
	public View view;
	private FSMSystem fsm;
	[HideInInspector]
	public CameraManager cameraManager;
	[HideInInspector]
	public GameManager gameManager;
	public AudioManager audioManager;

	private void Awake()
	{
		model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
		view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
		cameraManager = GetComponent<CameraManager>();
		gameManager = GetComponent<GameManager>();
		audioManager = GetComponent<AudioManager>();
	}
	private void Start()
	{
		MakeFSM();
	}
	void MakeFSM()
	{
		fsm = new FSMSystem();
		FSMState[] states = GetComponentsInChildren<FSMState>();
		//将所有状态添加到状态机中管理
		foreach (FSMState state in states)
		{
			fsm.AddState(state,this);
		}
		//设置默认状态
		MenuState s = GetComponentInChildren<MenuState>();
		fsm.SetCurrentState(s);

	}
}
