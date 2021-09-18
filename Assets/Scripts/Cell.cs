using UnityEngine;

public class Cell : MonoBehaviour
{
    public Matrix Index { get; set; }

    public IUnit Unit { get; set; }

    public void Print() => print(Index.Row + " " + Index.Column);
}
