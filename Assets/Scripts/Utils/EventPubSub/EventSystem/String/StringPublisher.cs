using UnityEngine;

namespace SOEventSystem
{
    [CreateAssetMenu(fileName = "StringPublisher", menuName = "Scriptable Objects/Event System/StringPublisher")]
    public class StringPublisher : EventPublisher<string> { }
}