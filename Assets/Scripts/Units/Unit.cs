using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    public Cell Cell { get; set; }

    private void Awake()
    {
        Cell = new Cell();
    }

    public virtual void Die()
    {
        Cell.RemoveUnit(this);
        KAP.Pool.Pool.Despawn(gameObject);
    }

    public virtual void BecomeDirty()
    {
        Die();
    }
}
