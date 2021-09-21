public interface IControl
{
    public void MoveTo(Field.Direction direction, int distance);
}

public interface IPlayerControl : IControl
{
    public void PlantBomb();
}
