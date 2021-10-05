public class BtnsPlayerControl : PlayerControl
{
    private void Awake()
    {
        BtnsControl.MoveUpAction += MoveUP;
        BtnsControl.MoveDownAction += MoveDown;
        BtnsControl.MoveLeftAction += MoveLeft;
        BtnsControl.MoveRightAction += MoveRight;
        BtnsControl.PlantBombAction += PlantBomb;
    }

    private void OnDestroy()
    {
        BtnsControl.MoveUpAction += MoveUP;
        BtnsControl.MoveDownAction += MoveDown;
        BtnsControl.MoveLeftAction += MoveLeft;
        BtnsControl.MoveRightAction += MoveRight;
        BtnsControl.PlantBombAction += PlantBomb;
    }

    public void MoveUP() => MoveTo(KAP.Helper.Direction.Up, 1);
    public void MoveDown() => MoveTo(KAP.Helper.Direction.Down, 1);
    public void MoveLeft() => MoveTo(KAP.Helper.Direction.Left, 1);
    public void MoveRight() => MoveTo(KAP.Helper.Direction.Right, 1);
}
