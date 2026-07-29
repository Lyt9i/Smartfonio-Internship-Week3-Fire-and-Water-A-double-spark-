using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private float _speed;
    [SerializeField] private int _maxCount;
    [SerializeField] private List<Tile> _tiles = new List<Tile>();
    [SerializeField] private Transform _tileHolder;

    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private float _startSpawnBomb = 3;
    [SerializeField] private float _tileLength = 0;

    private float _timer;
    private bool _isEnabling = true;

    void Start()
    {
        if (_tileLength <= 0f)
        {
            Renderer rend = _tilePrefab.GetComponentInChildren<Renderer>();
            if (rend != null)
            {
                _tileLength = rend.bounds.size.z;
            }
            else
            {
                Debug.LogWarning("[TileGenerator] Не удалось определить длину тайла автоматически.", this);
            }
        }
        _tiles.First().SetSpeed(_speed);
        for (int i = 0; i < _maxCount; i++)
        {
            GenerateTile();
        }
  
    }

    void Update()
    {
        if (_isEnabling == false)
            return;

        _timer += Time.deltaTime;

        if (_tiles.Count < _maxCount)
        {
            GenerateTile();
        }
    }

    public void SetEnabling(bool state)
    {
        _isEnabling = state;
        
        foreach(Tile tile in _tiles)
        {
            tile.SetMoving(state);
        }
    }

    private void GenerateTile()
    {
        Vector3 spawnPosition = _tiles.Last().transform.position + Vector3.forward * _tileLength;
        GameObject newTileObject = Instantiate(_tilePrefab, spawnPosition, Quaternion.identity);
        Tile newTile = newTileObject.GetComponent<Tile>();
        newTile.Initialize(_coin, _bomb, _startSpawnBomb, _timer);
        newTile.SetSpeed(_speed);
        _tiles.Add(newTile);
        newTileObject.transform.SetParent(_tileHolder);
    }

    private void OnTriggerEnter(Collider other)
    {
        _tiles.Remove(other.GetComponent<Tile>());
        Destroy(other.gameObject);
    }
}