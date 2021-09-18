using UnityEngine;
using KAP.Helper;

public class Field : MonoBehaviour
{
    public enum Direction { Up, Right }

    [Header("Parameters")]
    [SerializeField, Min(1)] private int countOfRows;
    [SerializeField, Min(1)] private int countOfColumns;
    [SerializeField, Min(1)] private int cabbageCount;

    [Header("References")]
    [SerializeField] private System.Collections.Generic.List<Cell> wayPoints;
    [SerializeField] private System.Collections.Generic.List<Cell> dontSeed;
    [SerializeField] private GameObject cabbagePrefab;

    private Cell[,] _cells;

    #region Start
    private void Start()
    {
        UpdateDontSeedList();
        Seeding();
        WayPointsToCellMatrix();
    }

    private void UpdateDontSeedList()
    {
        for (int i = 0; i < dontSeed.Count; i++)
            dontSeed[i].CanSeeding = false;
    }

    private void Seeding()
    {
        cabbageCount = Mathf.Min(cabbageCount, wayPoints.Count - dontSeed.Count);

        for (int i = 0; i < cabbageCount; i++)
        {
            Cell temp = wayPoints.Random();
            if (temp.CanSeeding)
            {

            }
            else i--;
        }
    }

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

        wayPoints.Clear();
    }
    #endregion

    #region Cell
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
        Matrix temp = new Matrix(unitCell);
        switch (direction)
        {
            case Direction.Up:
                temp.Row -= distance;
                break;
            case Direction.Right:
                temp.Column += distance;
                break;
        }

        return temp;
    }

    private Cell GiveCell(Matrix cellIndex) => _cells[cellIndex.Row, cellIndex.Column];
    #endregion
}
