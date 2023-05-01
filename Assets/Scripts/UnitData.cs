using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Create UnitData", fileName = "UnitData", order = 0)]
    public class UnitData : ScriptableObject
    {
        public float trainingTime;
        public GameObject gameObject;
        public float speed;
        public int damage;
    }
}