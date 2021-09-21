using UnityEngine;

public class KeyboardControl : MonoBehaviour, IPlayerControl
{
    [Header("References")]
    [SerializeField] private GameObject bombPrefab;

    private IMovingUnit _unit;

    private void Start()
    {
        _unit = GetComponent<IMovingUnit>();
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
                MoveTo(Field.Direction.Up, verticalInput);
            }
            else if (horizontalInput != 0)
            {
                MoveTo(Field.Direction.Right, horizontalInput);
            }
        }
    }

    public void MoveTo(Field.Direction direction, int distance)
    {
        Cell temp = Field.singleton.GiveCell(_unit.Cell, direction, distance);
        if (temp != null && temp.UnitsIsEmpty())
            _unit.MoveTo(temp);
    }

    public void PlantBomb()
    {
        _unit.Cell.SpawningUnit(bombPrefab);
    }
}
