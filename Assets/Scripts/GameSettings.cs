using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "GameSettings")]
public class GameSettings : ScriptableObject 
{
	public List<LevelSettings> Levels
	{
		get
		{
			return _levels;
		}
	}

	[System.Serializable]
	public class LevelSettings
	{
		public int StartRow;
		public int StartCol;
		public int GoalRow;
		public int GoalCol;
	}
	
	[SerializeField]
	private List<LevelSettings> _levels;
}
