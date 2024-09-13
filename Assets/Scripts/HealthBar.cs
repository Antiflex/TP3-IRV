using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private CharacterStats _characterStats;
    private Slider _healthBarSlider;

    void Start()
    {
        _characterStats = GetComponentInParent<CharacterStats>();
        _healthBarSlider = GetComponentInChildren<Slider>();
        _healthBarSlider.maxValue = _characterStats._maxHealth;
    }

    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthBarSlider.value = _characterStats._currentHealth;
    }
}
