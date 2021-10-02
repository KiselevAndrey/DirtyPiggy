public class Pig : MovingUnit, IUnit
{
    public static System.Action MovingAction;

    #region Override
    public override void Die()
    {
        GameManager.Singleton.EndGame();
    }

    public override void MoveToStartPosition()
    {
        base.MoveToStartPosition();
        _forwardDirection = KAP.Helper.Direction.Directions.Right;
    }

    protected override void EndMoving()
    {
        base.EndMoving();

        MovingAction?.Invoke();
    }
    #endregion
}
