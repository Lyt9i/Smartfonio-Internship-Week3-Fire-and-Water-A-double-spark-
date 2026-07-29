
using UnityEditor;
using UnityEngine;

/// <summary>
/// Автоматически "прилепляет" объекты с компонентом SnapToGrid к сетке
/// прямо во время перетаскивания в Scene View — без зажатия Ctrl.
/// При отпускании мыши позиция дополнительно подтягивается к ближайшему
/// соседнему объекту с любой стороны: слева, справа, спереди, сзади,
/// сверху или снизу — так можно строить как ряды на полу, так и стопки.
/// </summary>
[InitializeOnLoad]
public static class SnapToGridSystem
{
    // Максимальное расстояние (в юнитах) для "магнитного" прилипания к соседу
    private const float NeighborSnapDistance = 0.6f;

    static SnapToGridSystem()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        if (e.type != EventType.MouseDrag && e.type != EventType.MouseUp)
            return;

        foreach (Transform t in Selection.transforms)
        {
            SnapToGrid snap = t.GetComponent<SnapToGrid>();
            if (snap == null) continue;

            Vector3 pos = t.position - snap.gridOffset;
            float size = Mathf.Max(0.0001f, snap.cellSize);

            pos.x = Mathf.Round(pos.x / size) * size;
            pos.y = Mathf.Round(pos.y / size) * size;
            pos.z = Mathf.Round(pos.z / size) * size;

            pos += snap.gridOffset;

            if (t.position != pos)
            {
                Undo.RecordObject(t, "Snap To Grid");
                t.position = pos;
            }

            // Магнит к ближайшему соседу срабатывает при отпускании мыши
            if (e.type == EventType.MouseUp)
            {
                TrySnapToNeighbor(t, snap);
            }
        }
    }

    private static void TrySnapToNeighbor(Transform t, SnapToGrid snap)
    {
        SnapToGrid[] all = Object.FindObjectsByType<SnapToGrid>(FindObjectsSortMode.None);
        float bestDist = NeighborSnapDistance;
        Transform bestNeighbor = null;

        // Ищем ближайшего соседа в 3D (по всем осям, а не только по полу)
        foreach (SnapToGrid other in all)
        {
            if (other.transform == t) continue;

            float dist = Vector3.Distance(t.position, other.transform.position);
            if (dist < bestDist)
            {
                bestDist = dist;
                bestNeighbor = other.transform;
            }
        }

        if (bestNeighbor == null) return;

        float size = Mathf.Max(0.0001f, snap.cellSize);
        Vector3 delta = t.position - bestNeighbor.position;
        Vector3 snapped = bestNeighbor.position;

        // Определяем, с какой стороны находится объект — по оси с наибольшим
        // расстоянием (это и есть сторона, к которой он прилипает: X — бок,
        // Y — верх/низ, Z — перед/зад). По остальным двум осям выравниваем
        // объект строго вплотную к соседу, чтобы не было перекоса.
        float ax = Mathf.Abs(delta.x);
        float ay = Mathf.Abs(delta.y);
        float az = Mathf.Abs(delta.z);

        if (ax >= ay && ax >= az)
        {
            snapped.x = bestNeighbor.position.x + Mathf.Sign(delta.x == 0 ? 1 : delta.x) * size;
        }
        else if (ay >= ax && ay >= az)
        {
            snapped.y = bestNeighbor.position.y + Mathf.Sign(delta.y == 0 ? 1 : delta.y) * size;
        }
        else
        {
            snapped.z = bestNeighbor.position.z + Mathf.Sign(delta.z == 0 ? 1 : delta.z) * size;
        }

        if (t.position != snapped)
        {
            Undo.RecordObject(t, "Snap To Neighbor");
            t.position = snapped;
        }
    }
}
