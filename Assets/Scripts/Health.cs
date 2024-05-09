using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TypeHealth
{
    Enemy,
    Player,
    Any
}

public sealed class Health : MonoBehaviour
{

    [SerializeField] private TypeHealth _typeHealth = TypeHealth.Player;

    [SerializeField] private AudioSource _healthAudioSource;
    [SerializeField] private AudioClip _playerTakeDamage;

    public TypeHealth TypeHealth => _typeHealth;

    [SerializeField] private float _maxHealth = 100f;

    [SerializeField] private float _currentHealth;

    public float CurrentHealth
    {
        get => _currentHealth;
        private set => _currentHealth = value;
    }

    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _healthText;

    private string _text;

    private void Awake()
    {
        _healthAudioSource = GetComponent<AudioSource>();
        _currentHealth = _maxHealth;
        if (_healthSlider)
        {
            _healthSlider.maxValue = _maxHealth;
            _healthSlider.value = _currentHealth;
        }

        if (!_healthText) return;
        _text = _healthText.text;
        _healthText.text = $"{_text} + {_currentHealth}";
    }

    public void Damage(float count)
    {
        CurrentHealth -= count;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
        if (_healthSlider) _healthSlider.value = _currentHealth;
        if (!_healthText) return;
        _healthText.text = $"{_text} + {_currentHealth}";
    }

    public void Heal(float count)
    {
        CurrentHealth += count;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
        if (_healthSlider) _healthSlider.value = _currentHealth;
        if (!_healthText) return;
        _healthText.text = $"{_text} + {_currentHealth}";
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("enemy_weapon"))
        {
            _healthAudioSource.PlayOneShot(_playerTakeDamage);
            Damage(25f);
        }
    }
}
