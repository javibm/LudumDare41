using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour 
{
	void Awake () 
	{
		_mapTiles = new List<MapTile>();
	}

	void Start ()
	{
		Init();
	}

	public void Init()
	{
		string mapText = LoadMap();
		_goalRow = 14;
		_goalCol = 14;
		
		GenerateMap(mapText);
		_currentDestructionRadius = CalculateMaxDestructionRadius();		
	}
	private int _currentDestructionRadius;

	private int CalculateMaxDestructionRadius()
	{
		int a = System.Math.Max(_goalRow, _goalCol);
		int b = System.Math.Max(_mapRows - 1 - _goalRow, _mapCols - 1 - _goalCol);
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

		MapTile goalTile = GetMapTile(_goalRow, _goalCol);
		Instantiate(_goalPrefab, goalTile.transform.position, Quaternion.identity);
		goalTile.transform.SetParent(goalTile.transform);
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
		Vector3 position = new Vector3(x, _tileHeights[yIndex], z);
		Vector3 rotation = new Vector3(0f, _tileRotations[rotIndex], 0f);
		MapTile mt = Instantiate(_tilePrefabs[prefabIndex].GetRandomVariant(), position, Quaternion.Euler(rotation));
		_mapTiles.Add(mt);
		mt.transform.SetParent(transform);
	}

  private void GenerateVoid()
  {
    GameObject endVoid = new GameObject();
    endVoid.transform.parent = transform.parent;
    BoxCollider endVoidCollider = endVoid.AddComponent<BoxCollider>();
    endVoidCollider.isTrigger = true;

    endVoidCollider.size = new Vector3(_mapRows, 1, 1);
  }

	void OnGUI ()
	{
		if (GUI.Button(new Rect(10, 10, 80, 30), "Regenerate!"))
		{
			DestroyMap();
			Init();
    }

		if (GUI.Button(new Rect(100, 10, 80, 30), "Destroy 1"))
		{
			DestroyBorderTiles(_goalRow, _goalCol, false);
    }

		if (GUI.Button(new Rect(100, 50, 80, 30), "Destroy 2"))
		{
			DestroyBorderTiles(_goalRow, _goalCol, true);
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
		public MapTile GetRandomVariant()
		{
			return variants[UnityEngine.Random.Range(0,variants.Count)];
		}
	}

	[SerializeField]
	private List<MapTileVariants> _tilePrefabs;

	[SerializeField]
	private GameObject _goalPrefab;

	private List<MapTile> _mapTiles;
	private int _mapCols;
	private int _mapRows;

	private int _goalRow;
	private int _goalCol;

	private string _mapPath = "Assets/Resources/Map.txt";
}
