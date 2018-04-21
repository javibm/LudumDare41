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
		string mapText = LoadMap();
		GenerateMap(mapText);
	}

	private void DestroyMap ()
	{
		for (int i = 0; i < _mapTiles.Count; i++)
		{
			Destroy(_mapTiles[i].gameObject);
			_mapTiles[i] = null;
		}
		_mapTiles.Clear();
	}

	private void GenerateMap (string mapText)
	{
		int x = 0;
		int z = 0;
		string[] rows = mapText.Split('\n');

		for (int i = 0; i < rows.Length; i++)
		{
			string[] tiles = rows[i].Split('-');
			for (int j = 0; j < tiles.Length; j++)
			{
				string[] tileData = tiles[j].Split(',');
				int prefabIndex = int.Parse(tileData[0]);
				int yIndex = int.Parse(tileData[1]);
				int rotIndex = int.Parse(tileData[2]);
				InstantiateTile(prefabIndex, x, z, yIndex, rotIndex);
				z += _tileSeparation;
			}
			x += _tileSeparation;
			z = 0;
		}
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

	void OnGUI ()
	{
		if (GUI.Button(new Rect(10, 10, 100, 70), "Regenerate!"))
		{
			DestroyMap();
			string mapText = LoadMap();
			GenerateMap(mapText);
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

	private List<MapTile> _mapTiles;

	private string _mapPath = "Assets/Resources/Map.txt";
}
