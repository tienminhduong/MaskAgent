using UnityEngine;

[CreateAssetMenu(fileName = "HumanInfo", menuName = "Scriptable Objects/HumanInfo")]
public class HumanInfo : ScriptableObject
{
    public string Name;
    public int Age;
    public string DoB;
    public Role Role;
    public string Address;
}
