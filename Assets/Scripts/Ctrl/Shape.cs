using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

	private GameManager gameManger;
	private Ctrl ctrl;
	private bool isPause = false;
	private float timer = 0;//计时器，多少秒下落一个格
	private float stepTime = 0.8f;//多少秒下喽
	private Transform pivot;
	private int multiple = 20;//加速倍数
	private bool isSpeedup = false;//是否加速


	private void Awake()
	{
		pivot = transform.Find("Pivot");
	}
	private void Start()
	{
		ctrl.view.downBtn.onClick.AddListener(Down);
		ctrl.view.leftBtn.onClick.AddListener(left);
		ctrl.view.upBtn.onClick.AddListener(Up);
		ctrl.view.rightBtn.onClick.AddListener(Right);
	}
	private void Update()
	{
		if (isPause) return;
		timer += Time.deltaTime;
		if (timer > stepTime)
		{
			timer = 0;
			Fall();
		}
		InputControl();
	}
	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="color"></param>
	public void Init(Color color,Ctrl ctrl,GameManager gameManager)
	{
		foreach (Transform t in transform)
		{
			if(t.tag == "Block")
			{
				t.GetComponent<SpriteRenderer>().color = color;
			}
		}
		this.ctrl = ctrl;
		this.gameManger = gameManager;
	}
	/// <summary>
	/// 下落方法
	/// </summary>
	private void Fall()
	{
		Vector3 pos = transform.position;
		pos.y -= 1;
		transform.position = pos;
		if (ctrl.model.IsValidMapPositon(this.transform) == false)
		{
			pos.y += 1;
			transform.position = pos;
			isPause = true;
			
			bool isLineClear = ctrl.model.PlaceShape(transform);
			if (isLineClear)
			{
				ctrl.audioManager.PlayLineClear();
			}
			gameManger.FallDown();
			return;
		}
		ctrl.audioManager.PlayDrop();
	}
	/// <summary>
	/// 暂停的方法
	/// </summary>
	public void Pause()
	{
		isPause = true;
	}
	public void Resume()
	{
		isPause = false;
	}
	/// <summary>
	/// 对方块的控制PC
	/// </summary>
	private void InputControl()
	{
		//if (isSpeedup) return;
		#region 左右移动

		float h = 0;
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			h = -1;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			h = 1;
		}
		if (h != 0)
		{
			Vector3 pos = transform.position;
			pos.x += h;
			transform.position = pos;
			if (ctrl.model.IsValidMapPositon(this.transform) == false)
			{
				pos.x -= h;
				transform.position = pos;
			}
			else
			{
				ctrl.audioManager.PlayControl();
			}
			
		}
		#endregion
		#region  旋转
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			transform.RotateAround(pivot.position, Vector3.forward, -90);
			if(ctrl.model.IsValidMapPositon(this.transform) == false)
			{
				transform.RotateAround(pivot.position, Vector3.forward, 90);
			}
			else
			{
				ctrl.audioManager.PlayControl();
			}
		}
		#endregion
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			isSpeedup = true;
			stepTime /= multiple;
		}
	}
	#region 对方块的控制Android
	public void Up()
	{
		if (isPause) return;
		transform.RotateAround(pivot.position, Vector3.forward, -90);
		if (ctrl.model.IsValidMapPositon(this.transform) == false)
		{
			transform.RotateAround(pivot.position, Vector3.forward, 90);
		}
		else
		{
			ctrl.audioManager.PlayControl();
		}
	}
	public void Down()
	{
		if (isPause) return;
		isSpeedup = true;
		stepTime /= multiple;
	}
	public void left()
	{
		if (isPause) return;
		Vector3 pos = transform.position;
		pos.x += -1;
		transform.position = pos;
		if (ctrl.model.IsValidMapPositon(this.transform) == false)
		{
			pos.x -= -1;
			transform.position = pos;
		}
		else
		{
			ctrl.audioManager.PlayControl();
		}
	}
	public void Right()
	{
		if (isPause) return;
		Vector3 pos = transform.position;
		pos.x += 1;
		transform.position = pos;
		if (ctrl.model.IsValidMapPositon(this.transform) == false)
		{
			pos.x -= 1;
			transform.position = pos;
		}
		else
		{
			ctrl.audioManager.PlayControl();
		}
	}

	#endregion

}
