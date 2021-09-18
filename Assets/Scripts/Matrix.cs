using UnityEngine;

public class Matrix : MonoBehaviour
{
    public int Row { get; set; }
    public int Column { get; set; }

    #region Construct
    public Matrix(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public Matrix() => Row = Column = 0;

    public Matrix(Matrix matrix)
    {
        Row = matrix.Row;
        Column = matrix.Column;
    }
    #endregion

    public static Matrix zero => new Matrix(0, 0);

    #region Index In Matrix
    public bool RowIndexInMatrix(int maxRow) => IndexInMatrix(Row, maxRow);
    
    public bool ColumnIndexInMatrix(int maxColumn) => IndexInMatrix(Column, maxColumn);
    
    private bool IndexInMatrix(int num, int maxIndex) => num >= 0 && num < maxIndex;
    #endregion
}
