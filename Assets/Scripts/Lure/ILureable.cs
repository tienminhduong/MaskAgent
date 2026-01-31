using UnityEngine;

public interface ILureable
{
    public HumanInfo HumanInfo { get; }

    bool IsLureable(Role lurerRole)
    {
        return LureableRoleMap.Roles[lurerRole].Contains(HumanInfo.Role);
    }

    bool OnLured(Role lurerRole);
}
