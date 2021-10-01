public class Pig : MovingUnit, IUnit
{
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
    #endregion
}
