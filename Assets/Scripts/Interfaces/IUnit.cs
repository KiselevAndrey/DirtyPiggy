public interface IUnit
{
    Cell Cell { get; set;  }

    void BecomeDirty();

    void Die();
}

public interface IMovingUnit : IUnit
{
    bool IsMoving { get; }
    public Cell HomeCell { get; set; }

    void MoveTo(Cell cell, float duration);
    void MoveTo(Cell cell);

    void SetStartCellList(System.Collections.Generic.List<Cell> cells);
    void MoveToStartPosition();
}