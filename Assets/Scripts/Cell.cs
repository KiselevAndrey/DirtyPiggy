using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int Index { get; set; }

    public IUnit Unit { get; set; }
}
