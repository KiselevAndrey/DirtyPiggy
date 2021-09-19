using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    #region Properties
    public Matrix Index { get; set; }

    public List<IUnit> Unit { get; private set; }

    public bool CanSeeding { get; set; }

    public Vector3 Position => transform.position;
    #endregion

    public void Print() => print(Index.Row + " " + Index.Column);

    private void Awake()
    {
        CanSeeding = true;
        Unit = new List<IUnit>();
        Index = new Matrix();
    }

    #region Seeding
    public void SpawningUnit(GameObject unitPrefab)
    {
        Instantiate(unitPrefab, Position, Quaternion.identity);
        if (unitPrefab.TryGetComponent(out IUnit unit))
        {
            unit.Cell = this;
            Unit.Add(unit);
        }
    }
    #endregion

    #region For Unit property
    public void AddUnit(IUnit unit) => Unit.Add(unit);

    public void RemoveUnit(IUnit unit) => Unit.Remove(unit);

    public bool UnitIsEmpty() => Unit.Count == 0;
    #endregion
}
