using UnityEngine;

public class KeyboardMoving : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Field field;

    private IUnit _unit;

    private void Start()
    {
        _unit = GetComponent<IUnit>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            _unit.MoveToStartPosition();

        if (!_unit.IsMoving)
        {
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

    private void MoveTo(Field.Direction direction, int distance)
    {
        Cell temp = field.GiveCell(_unit.Cell, direction, distance);
        if (temp != null && temp.Unit == null)
            _unit.MoveTo(temp);
    }
}
