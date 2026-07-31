using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Точки спавна для зоны 1 (Огонь)")]
    [SerializeField] private List<Transform> _firePoints = new List<Transform>();

    [Header("Точки спавна для зоны 2 (Вода)")]
    [SerializeField] private List<Transform> _waterPoints = new List<Transform>();

    private float _speed;

    private GameObject _fireCoin;
    private GameObject _fireBomb;
    private GameObject _waterCoin;
    private GameObject _waterBomb;

    private float _startSpawnBomb;
    private float _timer;
    private bool _isMove = true;

    // Start is called before the first frame update
    void Start()
    {
        SpawnForZone(_firePoints, _fireCoin, _fireBomb);
        SpawnForZone(_waterPoints, _waterCoin, _waterBomb);
    }

    private void SpawnForZone(List<Transform> points, GameObject coin, GameObject bomb)
    {
        if (coin == null || bomb == null || points.Count == 0)
        {
            return;
        }

        int randomPointIndex = Random.Range(0, points.Count);

        if (_timer < _startSpawnBomb)
        {
            CreateObject(points[randomPointIndex], coin);
        }
        else
        {
            float chanceSpawnBomb = 20 + (_timer / 2);
            chanceSpawnBomb = Mathf.Clamp(chanceSpawnBomb, 0, 50);

            if (Random.Range(0, 100) < chanceSpawnBomb)
            {
                CreateObject(points[randomPointIndex], bomb);
            }
            else
            {
                CreateObject(points[randomPointIndex], coin);
            }
        }
    }

    private void CreateObject(Transform point, GameObject createdObject)
    {
        GameObject newObject = Instantiate(createdObject, point.position, Quaternion.identity);
        newObject.transform.SetParent(transform);
    }

    void FixedUpdate()
    {
        if (_isMove == false)
            return;

        transform.Translate(Vector3.back * _speed * Time.fixedDeltaTime);
    }

    public void Initialize(GameObject fireCoin, GameObject fireBomb, GameObject waterCoin, GameObject waterBomb, float startSpawnBomb, float timer)
    {
        _fireCoin = fireCoin;
        _fireBomb = fireBomb;
        _waterCoin = waterCoin;
        _waterBomb = waterBomb;
        _timer = timer;
        _startSpawnBomb = startSpawnBomb;
    }

    public void SetSpeed(float speed)
    {
        if (speed < 0)
        {
            Debug.LogError("Скорость для тайла ниже 0");
            return;
        }

        _speed = speed;
    }

    public void SetMoving(bool state)
    {
        _isMove = state;
    }
}