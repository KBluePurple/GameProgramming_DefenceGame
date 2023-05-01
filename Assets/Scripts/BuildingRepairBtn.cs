using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairBtn : MonoBehaviour
{
    [SerializeField]
    private HealthSystem healthSystem;
    [SerializeField]
    private ResourceTypeSO goldResourceType;

    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => { healthSystem.HealFull(); });

        int missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
        int repairCost = missingHealth / 2;

        ResourceAmount[] resourceAmountCost = new ResourceAmount[]
        {
            new ResourceAmount {resourceType = goldResourceType, amount = repairCost }
        };

        if (ResourceManager.Instance.CanAfford(resourceAmountCost))
        {
            ResourceManager.Instance.SpendResources(resourceAmountCost);
            healthSystem.HealFull();
        }
        else
        {
            TooltipUI.Instance.Show("Cannot afford repair cost!", new TooltipUI.TooltipTimer { timer = 2f });
        }
    }
}
