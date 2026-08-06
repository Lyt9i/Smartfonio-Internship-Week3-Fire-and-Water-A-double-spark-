using UnityEngine;

public class Bomb : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Debug.Log("Вы проиграли");
        GameManager.Instance.Die();
    }
    
}