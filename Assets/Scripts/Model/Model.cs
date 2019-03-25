using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
	public const int Max_ROWS = 23;
	public const int MAX_COLUMNS = 10;
	private Transform[,] map = new Transform[MAX_COLUMNS,Max_ROWS];

	private int score = 0;
	private int highScore = 0;
	private int numbersGame = 0;
	public int Score { get { return score; } }
	public int HighScore { get { return highScore; } }
	public int NumbersGame { get { return numbersGame; } }


	public bool isDataUpdate = false;//是否检查更新
	private void Awake()
	{
		LoadData();
	}

	/// <summary>
	/// 是否是一个可用的位置,看哪些地方有没有方块
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public bool IsValidMapPositon(Transform t)
	{
		foreach (Transform child in t)
		{
			if (child.tag != "Block") continue;
			Vector2 pos = child.position.Round();
			if (IsInsideMap(pos) == false) return false;
			if (map[(int)pos.x, (int)pos.y] != null) return false;
		}
		return true;
	}
	/// <summary>
	/// 判断是否在地图内
	/// </summary>
	/// <param name="pos"></param>
	private bool IsInsideMap(Vector2 pos)
	{
		return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
	}
	/// <summary>
	/// 摆放图形
	/// </summary>
	/// <param name="t"></param>
	public bool PlaceShape(Transform t)
	{
		foreach (Transform child in t)
		{
			if (child.tag != "Block") continue;
			Vector2 pos = child.position.Round();
			map[(int)pos.x, (int)pos.y] = child;
		}
		return CheckMap();
	}
	/// <summary>
	/// 检查地图是否需要消除行
	/// </summary>
	private bool CheckMap()
	{
		int count = 0;
		for (int i = 0; i < Max_ROWS; i++)
		{
			bool isFull = CheckIsRowFull(i);
			if (isFull)
			{
				count++;
				DeleteRow(i);
				MoveDownRowsAbove(i + 1);
				i--;
			}
		}
		if (count > 0)
		{
			score += (count * 100);
			if (score > highScore)
			{
				highScore = score;
			}
			isDataUpdate = true;
			return true;
		}
		else return false;
	}
	/// <summary>
	/// 检查某一行是否满了
	/// </summary>
	/// <param name="row"></param>
	/// <returns></returns>
	private bool CheckIsRowFull(int row)
	{
		for (int i = 0; i < MAX_COLUMNS; i++)
		{
			if (map[i, row] == null) return false;
		}
		return true;
	}
	/// <summary>
	/// 消除的方法
	/// </summary>
	/// <param name="row"></param>
	private void DeleteRow(int row)
	{
		for (int i = 0; i < MAX_COLUMNS; i++)
		{
			Destroy(map[i,row].gameObject);
			map[i, row] = null;
		}
	}
	/// <summary>
	/// 将上一行及以上都移动下来
	/// </summary>
	private void MoveDownRowsAbove(int row)
	{
		for (int i = row; i < Max_ROWS; i++)
		{
			MoveDownRow(i);
		}
	}
	/// <summary>
	/// 将一行往下移动
	/// </summary>
	private void MoveDownRow(int row)
	{
		for (int i = 0; i < MAX_COLUMNS; i++)
		{
			if (map[i, row] != null)
			{
				map[i, row - 1] = map[i, row];
				map[i, row] = null;
				map[i, row - 1].position += new Vector3(0, -1, 0);
			}
			
		}
	}


	private void LoadData()
	{
		highScore =PlayerPrefs.GetInt("HighScore", 0);
		numbersGame =PlayerPrefs.GetInt("NumbersGame", 0);
	}
	private void SaveDate()
	{
		PlayerPrefs.SetInt("HighScore",highScore);
		PlayerPrefs.SetInt("NumbersGame", numbersGame);
	}


	public bool IsGameOver()
	{
		for (int i = Max_ROWS-3; i < Max_ROWS; i++)
		{
			for (int j = 0; j < MAX_COLUMNS; j++)
			{
				if (map[j, i] != null)
				{
					numbersGame++;
					SaveDate();
					return true;
				}
			}
		}
		return false;
	}

	public void Restart()
	{
		for (int i = 0; i < MAX_COLUMNS; i++)
		{
			for (int j = 0; j < Max_ROWS; j++)
			{
				if (map[i, j] != null)
				{
					Destroy(map[i, j].gameObject);
					map[i, j] = null;
				}
			}
		}
		score = 0;
	}
	public void CleraData()
	{
		score = 0;
		highScore = 0;
		numbersGame = 0;
		SaveDate();
	}
}
