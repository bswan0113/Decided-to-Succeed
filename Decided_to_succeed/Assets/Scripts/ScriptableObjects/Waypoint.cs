// ScriptableObjects/Waypoint.cs

using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Waypoint", menuName = "ScriptableObject/Waypoint")]
    public class Waypoint : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private Vector3 _position;

        public string Id => _id;
        public Vector3 Position => _position;
    }
}