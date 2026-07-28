using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] public float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed * Time.fixedDeltaTime);
    }
}
