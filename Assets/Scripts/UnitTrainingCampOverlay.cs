using DefaultNamespace;
using TMPro;
using UnityEngine;

public class UnitTrainingCampOverlay : MonoBehaviour
{
    [SerializeField] private UnitTrainingCamp unitTrainingCamp;

    private Transform _barTransform;

    private void Start()
    {
        var resourceGeneratorData = unitTrainingCamp.GetResourceGeneratorData();

        _barTransform = transform.Find("bar");
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite =
            resourceGeneratorData.gameObject.GetComponent<SpriteRenderer>().sprite;
        transform.Find("text").GetComponent<TextMeshPro>()
            .SetText(unitTrainingCamp.GetAmountGeneratedPerSecond().ToString("f1"));
    }

    private void Update()
    {
        _barTransform.localScale = new Vector3(1 - unitTrainingCamp.GetTimerNormalized(), 1, 1);
    }
}