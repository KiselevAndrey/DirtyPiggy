using UnityEngine;

public class Field : MonoBehaviour
{
    public enum Direction { Up, Right }

    [Header("Parameters")]
    [SerializeField, Min(1)] private int countOfRows;
    [SerializeField, Min(1)] private int countOfColumns;

    [Header("References")]
    [SerializeField] private System.Collections.Generic.List<Cell> wayPoints;

    private Cell[,] _cells;

    private void Start()
    {
        _cells = new Cell[countOfRows, countOfColumns];

        for (int i = 0; i < wayPoints.Count; i++)
        {
            Vector2Int index = Vector2Int.zero;

            index.x = i / countOfColumns;
            index.y = i % countOfColumns;

            wayPoints[i].Index = index;
            _cells[index.x, index.y] = wayPoints[i];
        }

        wayPoints.Clear();
    }

    public Cell GiveCell(Cell startedCell, Direction direction, int distance = 0)
    {
        Vector2Int newCellIndex = CalculateCellIndex(startedCell.Index, direction, distance);

        if (!CellInMatrix(newCellIndex)) return null; 

        return GiveCell(newCellIndex);
    }

    private bool CellInMatrix(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= countOfRows) return false;
        if (cellIndex.y < 0 || cellIndex.y >= countOfColumns) return false;

        return true;
    }

    private Vector2Int CalculateCellIndex(Vector2Int unitCell, Direction direction, int distance)
    {
        switch (direction)
        {
            case Direction.Up:
                unitCell.x -= distance;
                break;
            case Direction.Right:
                unitCell.y += distance;
                break;
        }

        return unitCell;
    }

    private Cell GiveCell(Vector2Int cellIndex) => _cells[cellIndex.x, cellIndex.y];
}
