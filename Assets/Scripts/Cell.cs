using UnityEngine;

public class Cell : MonoBehaviour
{
    #region Properties
    public Matrix Index { get; set; }

    public IUnit Unit { get; set; }

    public bool CanSeeding { get; set; }

    public Vector3 Position => transform.position;
    #endregion

    public void Print() => print(Index.Row + " " + Index.Column);

    private void Awake()
    {
        CanSeeding = true;
    }

    #region Seeding
    public bool Seeding(GameObject plantingPrefab)
    {
        if(CanSeeding && Unit == null)
        {
            SpawningUnit(plantingPrefab);
        }
        return false;
    }

    public void SpawningUnit(GameObject unitPrefab, bool registration = true)
    {
        Instantiate(unitPrefab);
        if (unitPrefab.TryGetComponent(out IUnit unit))
        {
            unit.Cell = this;
            if (registration) Unit = unit;
        }
    }
    #endregion
}
