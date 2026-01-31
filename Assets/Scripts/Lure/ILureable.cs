using UnityEngine;

public interface ILureable
{
    public HumanInfo HumanInfo { get; }

    public bool IsLureable(Role lurerRole)
    {
        return LureableRoleMap.Roles[lurerRole].Contains(HumanInfo.Role);
    }
}
