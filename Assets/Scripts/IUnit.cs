using UnityEngine;

public interface IUnit
{
    Cell Cell { get; set; }
    float TimeToRelocate { get; }

    void MoveTo(Cell cell);

}
