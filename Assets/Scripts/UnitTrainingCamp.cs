using UnityEngine;

namespace DefaultNamespace
{
    public class UnitTrainingCamp : Building
    {
        [SerializeField] private UnitData unitData;
        [SerializeField] private float timerMax;
        private float _timer;

        private void Awake()
        {
            timerMax = unitData.trainingTime;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            
            if (!(_timer >= timerMax)) return;
            _timer -= timerMax;
            Instantiate(unitData.gameObject, transform.position, Quaternion.identity);
        }


        public UnitData GetResourceGeneratorData()
        {
            return unitData;
        }

        public float GetTimerNormalized()
        {
            return _timer / timerMax;
        }

        public float GetAmountGeneratedPerSecond()
        {
            return 1 / timerMax;
        }
    }
}