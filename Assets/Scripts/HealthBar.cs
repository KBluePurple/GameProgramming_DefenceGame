using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;
    private Transform separatorConatiner;

    private void Awake()
    {
        barTransform = transform.Find("bar");
    }

    private void Start()
    {
        separatorConatiner = transform.Find("SeparatorContainer");
        ConstructHealthBarSeparators();

        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        UpdateBar();
        UpdateHealthBarVisible();

        healthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;
    }

    private void ConstructHealthBarSeparators()
    {
        Transform separatorTemplate = separatorConatiner.Find("separatorTemplate");
        separatorTemplate.gameObject.SetActive(false);

        foreach(Transform sep in separatorConatiner)
        {
            if (sep == separatorTemplate) continue;
            Destroy(sep.gameObject);
        }

        int healthAmountPerSeparator = 10;
        float barSize = 2f;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();
        int healthSeparatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeparator);

        for (int i = 0; i < healthAmountPerSeparator; ++i)
        {
            Transform separatorTransform = Instantiate(separatorTemplate, separatorConatiner);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthSeparatorCount, 0, 0);
        }
    }

    private void HealthSystem_OnHealthAmountMaxChanged(object sender, EventArgs e)
    {
        ConstructHealthBarSeparators();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        if (healthSystem.IsFullHealth()) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}
