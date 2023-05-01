using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
    [SerializeField] private Building building;

    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => { Destroy(building.gameObject); });
        BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
        foreach(ResourceAmount resourceAmount in buildingType.constructionResourceCostArray)
        {
            ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * 0.6f));
        }
        // Destroy(building.gameObject);
    }


}
