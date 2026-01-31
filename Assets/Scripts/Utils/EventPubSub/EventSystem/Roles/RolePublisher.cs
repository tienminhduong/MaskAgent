using UnityEngine;

namespace SOEventSystem
{
    [CreateAssetMenu(fileName = "RolePublisher", menuName = "Scriptable Objects/Event System/RolePublisher")]
    public class RolePublisher : EventPublisher<Role>
    {
        public void RaiseStaffRole()
        {
            base.RaiseEvent(Role.Director);
        }
        public void TestRaiseRole(Role role)
        {
            base.RaiseEvent(role);
        }
        public void TestFloat(float value)
        {
            Debug.Log("RolePublisher received float: " + value);
        }
    }
}