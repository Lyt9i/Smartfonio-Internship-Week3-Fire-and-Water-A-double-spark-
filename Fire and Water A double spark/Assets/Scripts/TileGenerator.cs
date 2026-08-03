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

    [Header("Зона 1 — Огонь")]
    [SerializeField] private GameObject _fireCoin;
    [SerializeField] private GameObject _fireBomb;

    [Header("Зона 2 — Вода")]
    [SerializeField] private GameObject _waterCoin;
    [SerializeField] private GameObject _waterBomb;

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
        _speed += Time.deltaTime * 0.01f;

        if (_tiles.Count < _maxCount)
        {
            GenerateTile();
        }
    }

    public void SetEnabling(bool state)
    {
        _isEnabling = state;

        foreach (Tile tile in _tiles)
        {
            tile.SetMoving(state);
        }
    }

    private void GenerateTile()
    {
        Vector3 spawnPosition = _tiles.Last().transform.position + Vector3.forward * _tileLength;
        GameObject newTileObject = Instantiate(_tilePrefab, spawnPosition, Quaternion.identity);
        Tile newTile = newTileObject.GetComponent<Tile>();
        newTile.Initialize(_fireCoin, _fireBomb, _waterCoin, _waterBomb, _startSpawnBomb, _timer);
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