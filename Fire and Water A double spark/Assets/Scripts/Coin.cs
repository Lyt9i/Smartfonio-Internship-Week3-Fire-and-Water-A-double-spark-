using UnityEngine;


public class Coin : MonoBehaviour
{ 
    [SerializeField] private int _tag;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        GameManager.Instance.UpdateScore(_tag);
    }
    
}
