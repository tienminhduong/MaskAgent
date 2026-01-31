using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LimitedRoles", menuName = "Scriptable Objects/LimitedRoles")]
public class LimitedRoles : ScriptableObject
{
    public List<Role> Roles = new();
}