using UnityEngine;

public class Field : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }

    [Header("Parameters")]
    [SerializeField, Min(1)] private int countOfRows;
    [SerializeField, Min(1)] private int countOfColumns;

    [Header("References")]
    [SerializeField] private System.Collections.Generic.List<Transform> wayPoints;

}
