using UnityEngine;

public class KeyboardControl : PlayerControl
{
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
}
