using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "HumanInfo", menuName = "Scriptable Objects/HumanInfo")]
public class HumanInfo : ScriptableObject
{
    public string Name;
    public int Age;
    public string DOB;
    public Role Role;
    public string Address;
    public SpriteLibraryAsset SpriteLibrary;
    public Sprite profileImage;
}
