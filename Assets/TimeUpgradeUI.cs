using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUpgradeUI : MonoBehaviour
{
    [SerializeField] private ResourceAmount[] _upgradeCost = new ResourceAmount[5];
    public int UpgradeLevel { get; private set; } = 1;

    public void Upgrade()
    {
        if (!ResourceManager.Instance.CanAfford(_upgradeCost)) return;
        
        ResourceManager.Instance.SpendResources(_upgradeCost);
        UpgradeLevel++;
    }
}