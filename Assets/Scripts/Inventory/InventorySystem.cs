using UnityEngine;

public sealed class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameObject[] itemInSlots;

    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject slotsPointer;

    [SerializeField] private Transform handPoint;
    [SerializeField] private SwordController _swordController;

    private sbyte _currentSlot;
    private GameObject[] iconInSlots;

    private void Awake()
    {
        iconInSlots = new GameObject[slots.Length];
        _swordController = GetComponentInChildren<SwordController>();
    }
    
    private void Update()
    {
        ChangeCurrentSlot();
        if (itemInSlots[_currentSlot] != null)
            itemInSlots[_currentSlot].SetActive(true);
    }

    private void ChangeCurrentSlot()
    {
        int lengthSlots = itemInSlots.Length;
        for (sbyte i = 0; i < lengthSlots; i++)
        {
            if (!Input.GetKeyDown((i + 1).ToString())) continue;
            if (i == _currentSlot) return;
            if (itemInSlots[_currentSlot] != null)
                itemInSlots[_currentSlot].SetActive(false);
            _currentSlot = i;
            if (slotsPointer == null && i > slots.Length) return;
            Vector3 positionPointer = slotsPointer.transform.position;
            positionPointer.x = slots[i].transform.position.x;
            slotsPointer.transform.position = positionPointer;
        }
    }

    public bool AddItemToSlot(GameObject item)
    {
        if (item == null) return false;
        int lengthSlots = itemInSlots.Length;
        for (sbyte i = 0; i < lengthSlots; i++)
        {
            if (itemInSlots[i] != null) continue;
            Transform transformParent;
            if (handPoint != null) transformParent = handPoint;
            else transformParent = transform;
            GameObject obj = Instantiate(item, transformParent.position, transformParent.rotation, transformParent);
            Item itemComponent = obj.GetComponent<Item>();
            // i need to rotate this bcs... fuck u
            Transform weaponTransform = obj.transform;
            _swordController.GetNewComponentofWeapon();
            weaponTransform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
            //
            if (itemComponent != null)
            {
                if (itemComponent.IconItem != null && i < slots.Length)
                {
                    GameObject icon = Instantiate(itemComponent.IconItem, slots[i].transform.position, slots[i].transform.rotation, slots[i].transform);
                    icon.transform.localScale = slots[i].transform.localScale / itemComponent.UnscaleIcon;
                    iconInSlots[i] = icon;
                }
            }
            obj.SetActive(false);
            itemInSlots[i] = obj;
            return true;
        }

        return false;
    }
    
    public bool RemoveItemFromSlot(GameObject item)
    {
        if (item == null) return false;
        int lengthSlots = itemInSlots.Length;
        for (sbyte i = 0; i < lengthSlots; i++)
        {
            if (itemInSlots[i] != item) continue;
            itemInSlots[i] = null;
            Destroy(iconInSlots[i]);
            iconInSlots[i] = null;
            Destroy(item);
            return true;
        }

        return false;
    }
    
    public bool RemoveItemFromSlot(int index)
    {
        return RemoveItemFromSlot(itemInSlots[index]);
    }
}
