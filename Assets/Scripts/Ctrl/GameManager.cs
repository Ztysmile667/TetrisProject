using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private bool isInPause = true;//游戏是否暂停
	public Shape[] shapes;//方块预制件
	public Color[] colors;
	private Shape currentShape = null;
	private Ctrl ctrl;
	private Transform blockHolder;


	private void Awake()
	{
		ctrl = GetComponent<Ctrl>();
		blockHolder = transform.Find("BlockHolder");
	}
	private void Update()
	{
		if (isInPause) return;
		if(currentShape == null)
		{
			SpawnShape();
		}
	}

	public void StartGame()
	{
		isInPause = false;
		if (currentShape != null)
		{
			currentShape.Resume();
		}
	}
	public void ClearShape()
	{
		if (currentShape != null)
		{
			Destroy(currentShape.gameObject);
			currentShape = null;
		}
	}
	public void PauseGame()
	{
		isInPause = true;
		if (currentShape != null)
		{
			currentShape.Pause();
		}
	}
	/// <summary>
	/// 生成方块
	/// </summary>
	void SpawnShape()
	{
		int index = Random.Range(0, shapes.Length);
		int indexColor = Random.Range(0, colors.Length);
		currentShape = GameObject.Instantiate(shapes[index]);
		currentShape.transform.parent = blockHolder;
		currentShape.Init(colors[indexColor],ctrl,this);
	}
	/// <summary>
	/// 方块落下来了(完成时)
	/// </summary>
	public void FallDown()
	{
		currentShape = null;
		if (ctrl.model.isDataUpdate)
		{
			ctrl.view.UpdateGameUI(ctrl.model.Score,ctrl.model.HighScore);
		}
		foreach (Transform t in blockHolder)
		{
			if (t.childCount <= 1)
			{
				Destroy(t.gameObject);
			}
		}
		if (ctrl.model.IsGameOver())
		{
			PauseGame();
			ctrl.view.ShowGameOverUI(ctrl.model.Score);
		}
	}
}
