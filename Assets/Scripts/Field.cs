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
            Matrix index = Matrix.zero;

            index.Row = i / countOfColumns;
            index.Column = i % countOfColumns;

            wayPoints[i].Index = index;
            _cells[index.Row, index.Column] = wayPoints[i];
        }

        wayPoints.Clear();
    }

    public Cell GiveCell(Cell startedCell, Direction direction, int distance = 1)
    {
        Matrix newCellIndex = CalculateCellIndex(startedCell.Index, direction, distance);

        if (!CellInMatrix(newCellIndex)) return null; 

        return GiveCell(newCellIndex);
    }

    private bool CellInMatrix(Matrix cellIndex)
    {
        if (!cellIndex.RowIndexInMatrix(countOfRows)) return false;
        if (!cellIndex.ColumnIndexInMatrix(countOfColumns)) return false;

        return true;
    }

    private Matrix CalculateCellIndex(Matrix unitCell, Direction direction, int distance)
    {
        switch (direction)
        {
            case Direction.Up:
                unitCell.Row -= distance;
                break;
            case Direction.Right:
                unitCell.Column += distance;
                break;
        }

        return unitCell;
    }

    private Cell GiveCell(Matrix cellIndex) => _cells[cellIndex.Row, cellIndex.Column];
}
