public class Pig : MovingUnit, IUnit
{
    public override void Die()
    {
        GameManager.Singleton.EndGame();
    }
}
