using UnityEngine;

[CreateAssetMenu(fileName = "LVLSO")]
public class LVLSO : ScriptableObject
{
    [Header("Counts")]
    [SerializeField, Min(1)] private int minCabbageCount;
    [SerializeField, Min(1)] private int maxCabbageCount;
    [SerializeField, Min(0)] private int dogCount;
    [SerializeField, Min(0)] private int fermerCount;

    #region Properties
    public int CabbageCount { get => Random.Range(minCabbageCount, maxCabbageCount + 1); }
    public int DogCount { get => dogCount; }
    public int FermerCount { get => fermerCount; }
    #endregion


}
