using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    public float _maxHealth = 100f;
    public float _currentHealth;
    [SerializeField]
    public float _attackPower = 10f;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }

    // Méthode de mort
    public void Die()
    {
        Debug.Log(gameObject.name + " est mort !");
        gameObject.SetActive(false);
    }
}
