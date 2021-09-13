using UnityEngine;

public interface IUnit
{
    Vector2 Position { get; }
    float TimeToRelocate { get; }

    void Move(Vector2 newPosition);


}
