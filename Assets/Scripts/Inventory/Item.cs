using UnityEngine;

public sealed class Item : MonoBehaviour
{
    [SerializeField] private string nameItem = "Just Item!";
    [SerializeField] private GameObject iconItem;
    [SerializeField] private float unscaleIcon = 3f;

    public GameObject IconItem
    {
        get => iconItem;
        private set => iconItem = value;
    }
    
    public string NameItem
    {
        get => nameItem;
        private set => nameItem = value;
    }
    
    public float UnscaleIcon
    {
        get => unscaleIcon;
        private set => unscaleIcon = value;
    }
}
