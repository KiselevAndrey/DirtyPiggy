using UnityEngine;

public class Field : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }

    [Header("Parameters")]
    [SerializeField, Min(1)] private int countOfRows;
    [SerializeField, Min(1)] private int countOfColumns;

    [Header("References")]
    [SerializeField] private System.Collections.Generic.List<Cell> wayPoints;

    private void Start()
    {
        for (int i = 0; i < wayPoints.Count; i++)
        {
            Vector2Int index = Vector2Int.zero;

            index.x = i / countOfColumns;
            index.y = i % countOfColumns;

            wayPoints[i].Index = index;
        }
    }

    public bool CanMove(ref Vector2Int unitCell, Direction direction)
    {
        Vector2Int newCell = CalculateCell(unitCell, direction);

        if (newCell.x < 0 || newCell.x >= countOfRows) return false;
        if (newCell.y < 0 || newCell.y >= countOfColumns) return false;

        unitCell = newCell;
        return true;
    }

    private bool CellInMatrix()
    {
        return false;
    }

    private Vector2Int CalculateCell(Vector2Int unitCell, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                unitCell.x--;
                break;
            case Direction.Down:
                unitCell.x++;
                break;
            case Direction.Left:
                unitCell.y--;
                break;
            case Direction.Right:
                unitCell.y++;
                break;
        }

        return unitCell;
    }
}
