using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour 
{
	public static event Action OnPlayerSpawned = delegate {};
	public static event Action OnBallSpawned = delegate {};

	void Awake () 
	{
		_mapTiles = new List<MapTile>();
	}

	void Start ()
	{

	}

	public void Init (GameSettings.LevelSettings level, float destructionTime)
	{
		string mapText = LoadMap();
		_levelSettings = level;
		_destructionTime = destructionTime;
		GenerateMap(mapText);
		InstantiatePlayerAndBall(_levelSettings.StartRow, _levelSettings.StartCol);
    _currentDestructionRadius = CalculateMaxDestructionRadius();
		StartCoroutine(DestroyWorldCorroutine(_destructionTime));	
	}

	public void InstantiateBall (Vector3 ballPosition)
	{
		Instantiate(_ballPrefab, ballPosition, Quaternion.identity);
		OnBallSpawned();
	}

	private void InstantiatePlayerAndBall (int startRow, int startCol)
	{
		MapTile mt = GetMapTile(startRow, startCol);
		Instantiate(_playerPrefab, mt.transform.position, Quaternion.identity);
		OnPlayerSpawned();
	}

	[SerializeField]
	private GameObject _playerPrefab;
	[SerializeField]
	private GameObject _ballPrefab;

	private IEnumerator DestroyWorldCorroutine (float destructionTime)
	{

		while (true)
		{
			yield return new WaitForSeconds(destructionTime);
      ChangeSkybox();
			DestroyBorderTiles(_levelSettings.GoalRow, _levelSettings.GoalCol, false);
		}
	}
	
	private int CalculateMaxDestructionRadius()
	{
		int a = System.Math.Max(_levelSettings.GoalRow, _levelSettings.GoalCol);
		int b = System.Math.Max(_mapRows - 1 - _levelSettings.GoalRow, _mapCols - 1 - _levelSettings.GoalCol);
		return System.Math.Max(a,b);
	}

	private void DestroyBorderTiles (int row, int col, bool random)
	{
		List<MapTile> tiles = GetTilesInRadius(row, col,_currentDestructionRadius);
		StartCoroutine(DeactivateTilesCorroutine(tiles, random));
		_currentDestructionRadius--;
	}

	private List<MapTile> GetTilesInRadius (int row, int col, int radius)
	{
		List<MapTile> tiles = new List<MapTile>();
		MapTile mt;
		for(int i = -radius; i < radius; i++)
		{
			mt = GetMapTile(row - radius, col + i);
			if (mt != null)
			{
				tiles.Add(mt);
			}
		}
		for(int i = -radius; i < radius; i++)
		{
			mt = GetMapTile(row + i, col + radius);
			if (mt != null)
			{
				tiles.Add(mt);
			}
		}
		for(int i = radius; i > -radius; i--)
		{
			mt = GetMapTile(row + radius, col + i);
			if (mt != null)
			{
				tiles.Add(mt);
			}
		}
		for(int i = radius; i > -radius; i--)
		{
			mt = GetMapTile(row + i, col - radius);
			if (mt != null)
			{
				tiles.Add(mt);
			}
		}
		return tiles;
	}

	private IEnumerator DeactivateTilesCorroutine (List<MapTile> tiles, bool random)
	{
		while (tiles.Count > 0)
		{
			int tileToDeactivate = random ? UnityEngine.Random.Range(0, tiles.Count) : 0;
			tiles[tileToDeactivate].Deactivate();
			tiles.RemoveAt(tileToDeactivate);
			yield return new WaitForSeconds (0.03f);
		}
	}

	private IEnumerator DestroyMapCorroutine ()
	{
    while (true)
		{
      MapTile mt = GetMapTile(UnityEngine.Random.Range(0, _mapRows), UnityEngine.Random.Range(0, _mapCols));
			if(mt != null)
			{
				mt.Deactivate();
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	private void DestroyMap ()
	{
		StopAllCoroutines();
		for (int i = 0; i < _mapTiles.Count; i++)
		{
			Destroy(_mapTiles[i].gameObject);
			_mapTiles[i] = null;
		}
		_mapTiles.Clear();
	}

	private void GenerateMap (string mapText)
	{
		int row = 0;
		int col = 0;
		string[] rows = mapText.Split('\n');
		_mapRows = rows.Length;

		for (int i = 0; i < _mapRows; i++)
		{
			string[] tiles = rows[i].Split('-');
			_mapCols = tiles.Length;
			for (int j = 0; j < _mapCols; j++)
			{
				string[] tileData = tiles[j].Split(',');
				int prefabIndex = int.Parse(tileData[0]);
				int yIndex = int.Parse(tileData[1]);
				int rotIndex = int.Parse(tileData[2]);
				InstantiateTile(prefabIndex, row, col, yIndex, rotIndex);
				col += _tileSeparation;
			}
			row += _tileSeparation;
			col = 0;
		}

		MapTile goalTile = GetMapTile(_levelSettings.GoalRow, _levelSettings.GoalCol);
		GameObject go = Instantiate(_goalPrefab, goalTile.transform.position, Quaternion.identity);
		go.transform.SetParent(goalTile.transform);
	}

	private MapTile GetMapTile (int row, int col)
	{
		if (row < 0 || row >= _mapRows || col < 0 || col >= _mapCols)
		{
			return null;
		}
		return _mapTiles[_mapCols * row + col];
	}

	private float[] _tileHeights = new float[] {0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f};
	private float[] _tileRotations = new float[] {0f, 90f, 180f, 270f};

	private void InstantiateTile (int prefabIndex, int x, int z, int yIndex, int rotIndex)
	{
		if(_tilePrefabs[prefabIndex].randomRotation)
		{
			rotIndex = UnityEngine.Random.Range(0, _tileRotations.Length);
		}
		Vector3 position = new Vector3(x, _tileHeights[yIndex], z);
		Vector3 rotation = new Vector3(0f, _tileRotations[rotIndex], 0f);
		MapTile mt;
		if (x == _levelSettings.GoalRow && z == _levelSettings.GoalCol ||
			  x == _levelSettings.StartRow && z == _levelSettings.StartCol)
		{
			mt = Instantiate(_tilePrefabs[prefabIndex].GetVariant(0), position, Quaternion.Euler(rotation));
		} 
		else
		{
			mt = Instantiate(_tilePrefabs[prefabIndex].GetRandomVariant(), position, Quaternion.Euler(rotation));
		}
		Renderer r = mt.GetComponentInChildren<Renderer>();
		switch(prefabIndex)
		{
			case 0:	// Empty
				r.material.color = _tileColors[0];
				break;
			case 1:	// Floor
				r.material.color = _tileColors[yIndex];
				break;
			case 2:	// Wall
				r.material.color = _tileColors[UnityEngine.Random.Range(0, 4)];
				break;
			case 3:	// Slope
			case 4:	// Slope plus
				r.material.color = _tileColors[1];
				break;
			default:
				break;
		}
		_mapTiles.Add(mt);
		mt.transform.SetParent(transform);
	}

  private void ChangeSkybox()
  {
    if (RenderSettings.skybox.HasProperty("Color 2"))
    {
      Debug.Log("Color 2");
    }
    RenderSettings.skybox.SetColor("Color 2", Color.black);
    Debug.LogError(RenderSettings.skybox.GetColor("Color 2"));
  }

  private void Update()
  {
    DynamicGI.UpdateEnvironment();
  }

  void OnGUI ()
	{
		if (GUI.Button(new Rect(10, 10, 80, 30), "Regenerate!"))
		{
			DestroyMap();
			Init(_levelSettings, _destructionTime);
    }

		if (GUI.Button(new Rect(100, 10, 80, 30), "Destroy 1"))
		{
			DestroyBorderTiles(_levelSettings.GoalRow, _levelSettings.GoalCol, false);
    }

		if (GUI.Button(new Rect(100, 50, 80, 30), "Destroy 2"))
		{
			DestroyBorderTiles(_levelSettings.GoalRow, _levelSettings.GoalCol, true);
    }
	}

	private string LoadMap()
  {	
		StreamReader reader = new StreamReader(_mapPath); 
		string mapText = reader.ReadToEnd();
		reader.Close();
		return mapText;
  }

	[SerializeField]
	private int _tileSeparation = 1;


	[Serializable]
	private class MapTileVariants
	{
		[SerializeField]
		private List<MapTile> variants;
		[SerializeField]
		public bool randomRotation;
		public MapTile GetVariant(int i)
		{
			return variants[i];
		}
		public MapTile GetRandomVariant()
		{
			return variants[UnityEngine.Random.Range(0,variants.Count)];
		}
	}

	[SerializeField]
	private List<MapTileVariants> _tilePrefabs;

	[SerializeField]
	private GameObject _goalPrefab;

	[SerializeField]
	private List<Color> _tileColors;

  [SerializeField]
  private Material _skyboxMaterial;

  [SerializeField]
  private Color _skyboxDoomColor;

	private List<MapTile> _mapTiles;
	private int _mapCols;
	private int _mapRows;

	private GameSettings.LevelSettings _levelSettings;
	private float _destructionTime;

	private int _currentDestructionRadius;

	private string _mapPath = "Assets/Resources/Map.txt";
}
