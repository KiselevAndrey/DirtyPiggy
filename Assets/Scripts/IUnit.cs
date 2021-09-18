using UnityEngine;

public interface IUnit
{
    Cell Cell { get; }
    float TimeToRelocate { get; }
    bool IsMoving { get; }

    void MoveTo(Cell cell, float duration);
    void MoveTo(Cell cell);

    void MoveToStartPosition();

}
