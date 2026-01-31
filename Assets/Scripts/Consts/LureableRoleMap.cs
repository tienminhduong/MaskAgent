using System.Collections.Generic;

public class LureableRoleMap
{
    private static Dictionary<Role, List<Role>> roles = new()
    {
        { Role.Janitor, new List<Role> { Role.Security } },
        { Role.Security, new List<Role> { Role.Janitor } },
        { Role.Staff, new List<Role> { Role.Security, Role.Janitor } },
        { Role.Director, new List<Role> { Role.Janitor, Role.Security, Role.Staff } },
    };

    public static Dictionary<Role, List<Role>> Roles => roles;
}