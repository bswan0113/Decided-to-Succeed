using System.Collections.Generic;
using ScriptableObjects.Action;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Cutscene", menuName = "ScriptableObject/Cutscene")]
    public class Cutscene : ScriptableObject
    {
        [SerializeField]
        public List<IAction> actions;
    }
}