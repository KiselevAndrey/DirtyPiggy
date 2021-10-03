using UnityEngine;

public class KeyboardControl : MonoBehaviour, IPlayerControl
{
    [Header("References")]
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Animations animations;

    private IMovingUnit _unit;

    private void Start()
    {
        _unit = GetComponent<IMovingUnit>();
    }

    private void OnEnable()
    {
        animations.ChangeSprite(KAP.Helper.Direction.Right);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            _unit.MoveToStartPosition();

        if (!_unit.IsMoving)
        {
            if (Input.GetKeyUp(KeyCode.Space))
                PlantBomb();

            int verticalInput = (int)Input.GetAxis("Vertical");
            int horizontalInput = (int)Input.GetAxis("Horizontal");

            if(verticalInput != 0)
            {
                MoveTo(KAP.Helper.Direction.Up, verticalInput);
                animations.ChangeSprite(verticalInput > 0 ? KAP.Helper.Direction.Up : KAP.Helper.Direction.Down);
            }
            else if (horizontalInput != 0)
            {
                MoveTo(KAP.Helper.Direction.Right, horizontalInput);
                animations.ChangeSprite(horizontalInput > 0 ? KAP.Helper.Direction.Right : KAP.Helper.Direction.Left);
            }
        }
    }

    public void MoveTo(KAP.Helper.Direction.Directions direction, int distance)
    {
        Cell temp = Field.Singleton.GiveCell(_unit.Cell, direction, distance);
        if (temp != null && temp.UnitsIsEmpty())
        {
            _unit.MoveTo(temp);
        }
        //if (temp != null && !temp.UnitsIsEmpty())
        //    temp.PrintUnits();
    }

    public void PlantBomb()
    {
        _unit.Cell.SpawningUnit(bombPrefab);
    }
}
