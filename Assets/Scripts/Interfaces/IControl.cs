public interface IControl
{
    public void MoveTo(KAP.Helper.Direction.Directions direction, int distance);
}

public interface IPlayerControl : IControl
{
    public void PlantBomb();
}
