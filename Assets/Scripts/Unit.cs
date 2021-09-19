using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    public Cell Cell { get; set; }

    private void Awake()
    {
        Cell = new Cell();
    }

    public void Die()
    {
        Cell.RemoveUnit(this);
        Destroy(gameObject);
    }
}
