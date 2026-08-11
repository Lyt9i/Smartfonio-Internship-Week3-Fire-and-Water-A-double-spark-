using UnityEngine;
public class SnapToGrid : MonoBehaviour
{
    [Tooltip("Размер стороны кубического префаба в юнитах. Должен совпадать у всех префабов, которые должны стыковаться.")]
    public float cellSize = 1f;

    [Tooltip("Смещение сетки, если начало координат сцены не совпадает с углом карты.")]
    public Vector3 gridOffset = Vector3.zero;
}