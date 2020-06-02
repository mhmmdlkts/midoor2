using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryInterface
{
    void OnEquipOther(int weaponCode, char team);
}


public abstract class Subject : MonoBehaviour
{
    private List<InventoryInterface> _InventoryInterface = new List<InventoryInterface>();

    public void RegisterObserver(InventoryInterface inventoryInterface)
    {
        _InventoryInterface.Add(inventoryInterface);
    }

    public void Notify(int weaponCode, char team)
    {
        foreach (var ii in _InventoryInterface)
        {
            ii.OnEquipOther(weaponCode, team);
        }
    }
}
