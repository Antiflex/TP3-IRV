using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    private GameObject _player;
    private GameObject _monster;
    private CharacterStats _playerStats;
    private CharacterStats _monsterStats;
    private MonsterBehavior _monsterBehavior;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _monster = GameObject.FindGameObjectWithTag("Enemy");
        _playerStats = _player.GetComponent<CharacterStats>();
        _monsterStats = _monster.GetComponent<CharacterStats>();
        _monsterBehavior = _monster.GetComponent<MonsterBehavior>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _monsterStats.TakeDamage(_playerStats._attackPower);
        }

        if(!_monsterBehavior.IsInCooldown && _monsterBehavior.IsAttacking)
        {
            _playerStats.TakeDamage(_monsterStats._attackPower);
        }
    }
}
