using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private List<Transform> _points = new List<Transform>();
    
    private float _speed;
    private GameObject _coin;
    private GameObject _bomb;
    private float _startSpawnBomb;
    private float _timer;
    private bool _isMove = true;

    // Start is called before the first frame update
    void Start()
    {
        if (_coin == null || _bomb == null)
        {
            return;
        }

        int randomPoinIndex = Random.Range(0, _points.Count);

        if (_timer < _startSpawnBomb)
        {
            CreateObject(randomPoinIndex, _coin);
        }
        else
        {
            float chanceSpawnBomb = 20 + (_timer / 2);
            chanceSpawnBomb = Mathf.Clamp(chanceSpawnBomb, 0, 50);

            if (Random.Range(0, 100) < chanceSpawnBomb)
            {
                CreateObject(randomPoinIndex, _bomb);
            }
            else
            {
                CreateObject(randomPoinIndex, _coin);
            }
        }
    }
    private void CreateObject(int randomPoinIndex, GameObject createdObject)
    {
        GameObject newCoin = Instantiate(createdObject, _points[randomPoinIndex].position, Quaternion.identity);
        newCoin.transform.SetParent(transform);
    }

    void FixedUpdate()
    {
        if (_isMove == false)
            return;

        transform.Translate(Vector3.back *  _speed * Time.fixedDeltaTime);
    }

    public void Initialize(GameObject coin, GameObject bomb, float startSpawnBomb, float timer)
    {
        _coin = coin;
        _bomb = bomb;
        _timer = timer;
        _startSpawnBomb = startSpawnBomb;
    }

    public void SetSpeed(float speed)
    {
        if(speed < 0)
        {
            Debug.LogError("Ñêîðîñòü äëÿ òàéëà íèæå 0");
            return;
        }

        _speed = speed;
    }

    public void SetMoving(bool state)
    {
        _isMove = state;
    }
}