using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _maxTiles = 10;
    [SerializeField] private List<Tile> _tiles = new List<Tile>();
    void Start()
    {
        _tiles.First().speed = _speed;
        for (int i = 0; i < _maxTiles; i++)
        {
            GenerateTile();
        }
    }
    void Update()
    {
        if (_tiles.Count < _maxTiles)
        {
            GenerateTile();
        }
    }
    private void GenerateTile()
    {
        GameObject newTileObject = Instantiate(_tilePrefab, _tiles.Last().transform.position + Vector3.forward * _tilePrefab.transform.localScale.z, Quaternion.identity);
        Tile newTile = newTileObject.GetComponent<Tile>();
        newTile.speed = _speed;
        _tiles.Add(newTile);
    }
    void OnTriggerEnter(Collider other)
    {
        _tiles.Remove(other.GetComponent<Tile>());
        Destroy(other.gameObject);
        
    }




}

