public class Cabbage : Unit, IUnit
{
    public override void Die()
    {
        Field.Singleton.CabbageCount--;
        base.Die();
    }
}
