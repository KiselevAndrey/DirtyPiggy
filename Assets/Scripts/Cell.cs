using UnityEngine;

public class Cell : MonoBehaviour
{
    #region Properties
    public Matrix Index { get; set; }

    public IUnit Unit { get; set; }

    public Vector3 Position => transform.position;
    #endregion

    public void Print() => print(Index.Row + " " + Index.Column);
}
