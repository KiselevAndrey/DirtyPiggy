using UnityEngine;
using KAP.Helper;

public class Field : MonoBehaviour
{
    public static Field singleton;

    [Header("Parameters")]
    [SerializeField, Min(1)] private int countOfRows;
    [SerializeField, Min(1)] private int countOfColumns;
    [SerializeField, Min(1)] public int cabbageCount;

    [Header("References")]
    [SerializeField] private System.Collections.Generic.List<Cell> wayPoints;
    [SerializeField] private System.Collections.Generic.List<Cell> dontSeed;
    [SerializeField] private GameObject cabbagePrefab;

    private Cell[,] _cells;

    #region Awake Start
    private void Awake()
    {
        if (!singleton) singleton = this;
        
        UpdateDontSeedList();
        WayPointsToCellMatrix();
    }

    /// <summary> Prohibits sowing in cells </summary>
    private void UpdateDontSeedList()
    {
        for (int i = 0; i < dontSeed.Count; i++)
            dontSeed[i].CanSeeding = false;
    }

    /// <summary> Plants cabbage in a cell </summary>
    public void Seeding()
    {
        cabbageCount = Mathf.Min(cabbageCount, wayPoints.Count - dontSeed.Count);

        for (int i = 0; i < cabbageCount; i++)
        {
            Cell temp = wayPoints.Random();
            if (temp.CanSeeding && temp.UnitsIsEmpty())
            {
                temp.SpawningUnit(cabbagePrefab);
            }
            else i--;
        }
    }

    /// <summary> Converts a list of cells into a matrix. Gives cells an Index </summary>
    private void WayPointsToCellMatrix()
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
    }
    #endregion

    #region Cell
    public Cell GiveCell(Cell startedCell, Direction.Directions direction, int distance)
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

    private Matrix CalculateCellIndex(Matrix unitCell, Direction.Directions direction, int distance)
    {
        Matrix temp = new Matrix(unitCell);

        switch (direction)
        {
            case Direction.Directions.Up:
                temp.Row -= distance;
                break;
            case Direction.Directions.Down:
                temp.Row += distance;
                break;
            case Direction.Directions.Left:
                temp.Column -= distance;
                break;
            case Direction.Directions.Right:
                temp.Column += distance;
                break;
            case Direction.Directions.Error:
                Debug.LogWarning("Error direction");
                break;
        }

        return temp;
    }

    private Cell GiveCell(Matrix cellIndex) => _cells[cellIndex.Row, cellIndex.Column];
    #endregion

    #region Static
    public static int Distance(Cell firstCell, Cell secondCell)
    {
        return firstCell == null || secondCell == null
            ? default
            : Mathf.Abs(firstCell.Index.Row - secondCell.Index.Row) + Mathf.Abs(firstCell.Index.Column - secondCell.Index.Column);
    }
    #endregion

    public void CheckDirections()
    {
        for (int i = 0; i < _cells.GetLength(0); i++)
            for (int j = 0; j < _cells.GetLength(1); j++)
                CheckDirections(_cells[i, j]);
    }

    public void CheckDirections(Cell cell)
    {
        print(cell);
        print("Up " + GiveCell(cell, Direction.Up, 1));
        print("Down " + GiveCell(cell, Direction.Down, 1));
        print("Left " + GiveCell(cell, Direction.Left, 1));
        print("Rigth " + GiveCell(cell, Direction.Right, 1));
        print("__________________");
    }
}
