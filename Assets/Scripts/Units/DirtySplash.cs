public class DirtySplash : Unit
{
    public void GetDirty()
    {
        for (int i = 0; i < Cell.Units.Count; i++)
        {
           Cell.Units[i].BecomeDirty();
        }
    }

    public new void BecomeDirty() { }
}
