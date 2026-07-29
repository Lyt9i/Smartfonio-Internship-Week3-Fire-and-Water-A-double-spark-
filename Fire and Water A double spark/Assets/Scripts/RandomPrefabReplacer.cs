
using UnityEngine;
using System.Collections.Generic;

public class RandomPrefabReplacer : MonoBehaviour
{
    [Tooltip("Варианты префабов, один из которых будет случайно выбран для каждой точки.")]
    public GameObject[] prefabVariants;

    private void Awake()
    {
        if (prefabVariants == null || prefabVariants.Length == 0)
        {
            Debug.LogWarning($"[RandomPrefabReplacer] На объекте '{name}' пустой список вариантов.", this);
            return;
        }

        
        List<Transform> existingChildren = new List<Transform>();
        foreach (Transform child in transform)
            existingChildren.Add(child);

        foreach (Transform child in existingChildren)
        {
            GameObject chosen = prefabVariants[Random.Range(0, prefabVariants.Length)];

            GameObject spawned = Instantiate(chosen, child.position, child.rotation, transform);
            spawned.transform.localScale = child.localScale;

            Destroy(child.gameObject);
        }
    }
}