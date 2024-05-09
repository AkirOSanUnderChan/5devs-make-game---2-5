using UnityEngine;

public class CanUseLikeWeapon : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _weaponAudioSource;
    [SerializeField] private AudioClip[] _weaponSFXCollection;
    private BoxCollider _swordCollider;

    public float damageAmount;
    private int _comboCounter;


    private void Start()
    {
        _comboCounter = 1;
        _animator = GetComponentInParent<Animator>();
        _weaponAudioSource = GetComponentInChildren<AudioSource>();
        _swordCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            int randSound = Random.Range(0, _weaponSFXCollection.Length);
            _weaponAudioSource.PlayOneShot(_weaponSFXCollection[randSound]);
            _animator.SetInteger("atack", _comboCounter);
            _comboCounter++;

            if (_comboCounter > 2)
            {
                _comboCounter = 1;
            }
            
        }
        else
        {
            _animator.SetInteger("atack", 0);


        }
    }



}
