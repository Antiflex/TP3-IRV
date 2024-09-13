using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private bool _isAttacking = false;
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }

    private bool _isInCooldown = false;
    public bool IsInCooldown { get => _isInCooldown; set => _isInCooldown=value; }


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (IsAttacking && !IsInCooldown)
        {
            _animator.SetBool("isAttacking", true);
            StartCoroutine(nameof(Cooldown));
        }
        else
        {
            _animator.SetBool("isAttacking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsAttacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsAttacking = false;
        }
    }

    IEnumerator Cooldown()
    {
        IsInCooldown = true;
        yield return new WaitForSeconds(2f);
        IsInCooldown = false ;

    }
}
