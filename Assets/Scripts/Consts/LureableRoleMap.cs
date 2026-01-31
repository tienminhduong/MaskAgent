using System.Collections.Generic;

public class LureableRoleMap
{
    public Dictionary<Role, List<Role>> Roles = new()
    {
        { Role.Tranitor, new List<Role> { Role.Security } },
        { Role.Security, new List<Role> { Role.Tranitor } },
        { Role.Staff, new List<Role> { Role.Security, Role.Tranitor } },
        { Role.Director, new List<Role> { Role.Tranitor, Role.Security, Role.Staff } },
    };
}