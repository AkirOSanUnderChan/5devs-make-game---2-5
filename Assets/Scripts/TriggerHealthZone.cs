using UnityEngine;

public class TriggerHealthZone : MonoBehaviour
{
    private enum TypeAction
    {
        Damage,
        Heal,
    }
    
    private enum TypeActivate
    {
        Enter,
        Stay,
        Exit
    }

    [SerializeField] private TypeHealth _typeHealth = TypeHealth.Player;
    [SerializeField] private TypeAction _typeAction = TypeAction.Damage;
    [SerializeField] private TypeActivate _typeActivate = TypeActivate.Enter;
    [SerializeField] private float _count = 1f;


    private void Damage(GameObject obj)
    {
        Health health = obj.GetComponent<Health>();
        if (!health) return;
        if (health.TypeHealth == _typeHealth || _typeHealth == TypeHealth.Any || health.TypeHealth == TypeHealth.Any)
            health.Damage(_count);
    }
    
    private void Heal(GameObject obj)
    {
        Health health = obj.GetComponent<Health>();
        if (!health) return;
        if (health.TypeHealth == _typeHealth || _typeHealth == TypeHealth.Any || health.TypeHealth == TypeHealth.Any)
            health.Heal(_count);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_typeActivate != TypeActivate.Enter) return;
        switch (_typeAction)
        {
            case TypeAction.Damage:
                Damage(other.gameObject);
                break;
            case TypeAction.Heal:
                Heal(other.gameObject);
                break;
        }
    }
    
    public void OnTriggerStay(Collider other)
    {
        if (_typeActivate != TypeActivate.Stay) return;
        switch (_typeAction)
        {
            case TypeAction.Damage:
                Damage(other.gameObject);
                break;
            case TypeAction.Heal:
                Heal(other.gameObject);
                break;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (_typeActivate != TypeActivate.Exit) return;
        switch (_typeAction)
        {
            case TypeAction.Damage:
                Damage(other.gameObject);
                break;
            case TypeAction.Heal:
                Heal(other.gameObject);
                break;
        }
    }
}
