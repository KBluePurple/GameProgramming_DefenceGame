using System;
using System.Globalization;
using Chronos;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TimeAreaElement[] timeAreaElements;
    [SerializeField] private Text text;
    [SerializeField] private TimeUpgradeUI timeUpgradeUI;

    private int _activeElementIndex = -1;

    private void Awake()
    {
        foreach (var element in timeAreaElements)
            element.button.onClick.AddListener(() => OnClick(Array.IndexOf(timeAreaElements, element)));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _activeElementIndex = -1;

        timeAreaElements[0].area.localTimeScale = 1 - timeUpgradeUI.UpgradeLevel * 0.2f;
        timeAreaElements[0].button.GetComponentInChildren<Text>().text =
            timeAreaElements[0].area.localTimeScale.ToString(CultureInfo.InvariantCulture);
        timeAreaElements[1].area.localTimeScale = 1 + timeUpgradeUI.UpgradeLevel * 0.2f;
        timeAreaElements[1].button.GetComponentInChildren<Text>().text =
            timeAreaElements[1].area.localTimeScale.ToString(CultureInfo.InvariantCulture);

        if (_activeElementIndex == -1) return;

        timeAreaElements[_activeElementIndex].button.Select();
        text.text = timeAreaElements[_activeElementIndex].area.name;

        if (Camera.main == null) return;

        var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        timeAreaElements[_activeElementIndex].area.transform.position = point;
    }

    private void OnClick(int index)
    {
        if (_activeElementIndex == index) return;

        _activeElementIndex = index;
        timeAreaElements[_activeElementIndex].button.Select();
        text.text = timeAreaElements[_activeElementIndex].area.name;
    }
}


[Serializable]
public class TimeAreaElement
{
    public AreaClock2D area;
    public KeyCode keyCode;
    public Button button;
}