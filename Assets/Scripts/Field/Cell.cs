using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    #region Properties
    public Matrix Index { get; set; }

    public List<IUnit> Units { get; private set; }

    public bool CanSeeding { get; set; }

    public Vector3 Position => transform.position;
    #endregion

    private void Awake()
    {
        CanSeeding = true;
        Units = new List<IUnit>();
        Index = new Matrix();
    }

    #region Seeding
    public void SpawningUnit(GameObject unitPrefab)
    {
        if (Instantiate(unitPrefab, Position, Quaternion.identity).TryGetComponent(out IUnit unit))
        {
            unit.Cell = this;
            Units.Add(unit);
        }
    }
    #endregion

    #region For Unit property
    public void AddUnit(IUnit unit) => Units.Add(unit);

    public void RemoveUnit(IUnit unit) => Units.Remove(unit);

    public bool UnitsIsEmpty() => Units.Count == 0;

    public bool UnitsIsEmpty(IUnit exeptionUnit)
    {
        System.Type unitType = exeptionUnit.GetType();
        int count = Units.Count;
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].GetType() == unitType) 
                count--;
        }
        return count == 0;
    }

    public void PrintUnits()
    {
        print("-------------------");
        for (int i = 0; i < Units.Count; i++)
        {
            print(Units[i]);
        }
    }
    #endregion

    public new string ToString() => name + " " + Index.ToString();
}
