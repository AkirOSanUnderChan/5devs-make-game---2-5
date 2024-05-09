using UnityEngine;

public sealed class CollectableItem : MonoBehaviour
{
    [SerializeField] private GameObject getItem;
    private InventorySystem _inventorySystem;
    private RaycastPresident _raycastPresident;

    [SerializeField] private GameObject[] showWhenHovering;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        _inventorySystem = player.GetComponent<InventorySystem>();
        _raycastPresident = player.GetComponent<RaycastPresident>();
    }
    
    private void Update()
    {
        if (!_raycastPresident.isRaycast)
        {
            foreach (var t in showWhenHovering)
                t.SetActive(false);
            return;
        }
        
        foreach (var t in showWhenHovering)
            t.SetActive(_raycastPresident.DetectGameObject == gameObject);

        if (_raycastPresident.DetectGameObject != gameObject) return;

        if (!Input.GetButton("Collect")) return;
        if (_inventorySystem.AddItemToSlot(getItem))
            Destroy(gameObject);
    }
}
