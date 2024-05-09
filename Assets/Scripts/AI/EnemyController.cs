using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Rigidbody[] _ragdollRigidBodies;
    private Animator _animator;
    private NavMeshAgent _agent;

    private CapsuleCollider _enemyCollider;
    private AudioSource _enemyAudioSource;

    [SerializeField] private BoxCollider _enemySwordCollider;

    [SerializeField] private ParticleSystem _blodParticle;
    [SerializeField] private AudioClip _takeDamageSFX;

    [SerializeField] private Transform _player;
    private bool _playerIsVisible;

    [Header("Enemy stats")]
    [SerializeField] private float _enemyCurrentHP;
    [SerializeField] private float _enemyMaxHP;
    [SerializeField] private float _enemyMaxVisionReach;
    [SerializeField] private float _EnemyAtackDistance;

    [SerializeField] private bool _enemyIsDead;
    



    void Awake()
    {
        _enemySwordCollider = _enemySwordCollider.GetComponent<BoxCollider>();
        _enemyCurrentHP = _enemyMaxHP;
        _ragdollRigidBodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyAudioSource = GetComponent<AudioSource>();
        _enemyCollider = GetComponent<CapsuleCollider>();
        _blodParticle = _blodParticle.GetComponent<ParticleSystem>();
        DisableRagdoll();
    }
    private void Start()
    {



        if (_player != null)
        {
            _playerIsVisible = true;
        }
        else
        {
            _playerIsVisible= false;
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, _player.gameObject.transform.position);
        if (!_enemyIsDead)
        {
            if (_playerIsVisible)
            {
                if (distanceToPlayer <= _enemyMaxVisionReach)
                {
                    _agent.SetDestination(_player.gameObject.transform.position);
                    _animator.SetBool("idle", false);
                    _animator.SetBool("run", true);
                    _animator.SetBool("atack", false);

                }
                else
                {
                    _animator.SetBool("idle", true);
                    _animator.SetBool("run", false);
                    _animator.SetBool("atack", false);

                }
                if (distanceToPlayer <= _EnemyAtackDistance)
                {
                    _animator.SetBool("atack", true);
                    _animator.SetBool("idle", false);
                    _animator.SetBool("run", false);
                }

            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().gameObject.CompareTag("player_sword"))
        {
            var playerSword = other.gameObject.GetComponent<CanUseLikeWeapon>();
            TakeDamage(playerSword.damageAmount);
            _blodParticle.transform.position = other.gameObject.transform.position;
            _blodParticle.Play();


        }
    }



    public void TakeDamage(float damageToTake)
    {
        Debug.Log("enemy take damage :" + damageToTake);
        _enemyCurrentHP -= damageToTake;
        _enemyAudioSource.PlayOneShot(_takeDamageSFX);
        if (_enemyCurrentHP <= 0)
        {
            _enemyCollider.enabled = false;
            _enemySwordCollider.enabled = false;
            EnableRagdoll();
            _enemyIsDead = true;
            _agent.Stop();
        }

    }

    private void DisableRagdoll()
    {
        _animator.enabled = true;
        foreach (var rigidbody in _ragdollRigidBodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    private void EnableRagdoll()
    {
        _animator.enabled = false;
        foreach (var rigidbody in _ragdollRigidBodies)
        {
            rigidbody.isKinematic = false;
        }
    }






}
