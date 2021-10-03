using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Matrix _index;
    #region Properties
    public Matrix Index 
    { 
        get => _index;
        set
        {
            _index = new Matrix();
            _index = value;
        } 
    }

    public List<IUnit> Units { get; private set; }

    public bool CanSeeding { get; set; }

    public Vector3 Position => transform.position;
    #endregion

    private void Awake()
    {
        CanSeeding = true;
        Units = new List<IUnit>();
    }

    #region Spaning
    public void SpawningUnit(GameObject unitPrefab, bool register = true)
    {
        if (KAP.Pool.Pool.Spawn(unitPrefab, Position).TryGetComponent(out IUnit unit))
        {
            unit.Cell = this;
            if(register)
                Units.Add(unit);
        }
    }

    public void SpawningMovingUnit(GameObject unitPrefab, List<Cell> startedCellList)
    {
        if(KAP.Pool.Pool.Spawn(unitPrefab, Position).TryGetComponent(out IMovingUnit movingUnit))
        {
            movingUnit.HomeCell = this;
            movingUnit.SetStartCellList(startedCellList);
            movingUnit.MoveToStartPosition();
        }
    }
    #endregion

    #region Properties for Unit 
    public void AddUnit(IUnit unit) => Units.Add(unit);

    public void RemoveUnit(IUnit unit) => Units.Remove(unit);
    #endregion

    #region Units is empty
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
    #endregion

    #region Find Unit
    public bool TryFindUnit(System.Type unitType)
    {
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].GetType() == unitType)
                return true;
        }
        return false;
    }

    public bool TryFindUnit(System.Type unitType, out IUnit unit)
    {
        unit = null;

        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].GetType() == unitType)
            {
                unit = Units[i];
                return true;
            }
        }

        return false;
    }
    #endregion

    public void PrintUnits()
    {
        print("-------------------");
        for (int i = 0; i < Units.Count; i++)
        {
            print(Units[i]);
        }
    }

    public new string ToString() => name + " " + Index.ToString();
}
