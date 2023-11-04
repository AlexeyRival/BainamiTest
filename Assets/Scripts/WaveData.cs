using UnityEngine;

[CreateAssetMenu(menuName = "Data/Wave")]
public class WaveData:ScriptableObject
{
    [Tooltip("You can make more then one group per type")]
    public Horde[] enemys;
}

[System.Serializable]
public class Horde
{
    public int count;
    public Enemy enemy;
}