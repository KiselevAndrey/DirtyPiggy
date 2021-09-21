public interface IUnit
{
    Cell Cell { get; set;  }

    void Die();

    void BecomeDirty();
}

public interface IMovingUnit : IUnit
{
    float TimeToRelocate { get; }
    bool IsMoving { get; }

    void MoveTo(Cell cell, float duration);
    void MoveTo(Cell cell);

    void MoveToStartPosition();
}