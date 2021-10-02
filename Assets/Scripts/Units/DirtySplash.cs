public class DirtySplash : Unit
{
    public void GetDirty()
    {
        int count = Cell.Units.Count;
        for (int i = 0; i < count; i++)
        {
            Cell.Units[0].BecomeDirty();
        }
    }

    public new void BecomeDirty() { }
}
