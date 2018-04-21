using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour 
{
	void Awake () 
	{
		_mapTiles = new List<MapTile>();
	}

	void Start ()
	{
		GenerateMap();
	}

	private void GenerateMap ()
	{
		Vector3 _tilePos = Vector3.zero;

		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				InstantiateTile(Random.Range(0, _tilePrefabs.Count), _tilePos);
				_tilePos.z += _tileSeparation;
			}
			_tilePos.x += _tileSeparation;
			_tilePos.z = 0;
		}
	}

	private MapTile InstantiateTile (int prefabIndex, Vector3 position)
	{
		return Instantiate(_tilePrefabs[prefabIndex], position, Quaternion.identity);
	}


	[SerializeField]
	private float _tileSeparation = 1f;
	[SerializeField]
	private List<MapTile> _tilePrefabs;

	private List<MapTile> _mapTiles;
}
