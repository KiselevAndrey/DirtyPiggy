using UnityEngine;

public class PlayerControl : MonoBehaviour, IPlayerControl
{
    [Header("References")]
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] protected Animations animations;

    protected IMovingUnit _unit;

    private void Start()
    {
        _unit = GetComponent<IMovingUnit>();
    }

    private void OnEnable()
    {
        animations.ChangeSprite(KAP.Helper.Direction.Right);
    }

    public void MoveTo(KAP.Helper.Direction.Directions direction, int distance)
    {
        Cell temp = Field.Singleton.GiveCell(_unit.Cell, direction, distance);
        if (temp != null && temp.UnitsIsEmpty())
        {
            animations.ChangeSprite(direction);
            _unit.MoveTo(temp);
        }
        //if (temp != null && !temp.UnitsIsEmpty())
        //    temp.PrintUnits();
    }

    public void PlantBomb()
    {
        if(!_unit.Cell.TryFindUnit(bombPrefab.GetType()))
            _unit.Cell.SpawningUnit(bombPrefab);
    }
}
