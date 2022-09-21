using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int _maxInventoryWeight = 20;

    [SerializeField]
    private List<Turret> _allTurrets;

    [SerializeField]
    private GameObject _inventoryItem;

    private Dictionary<Turret, int> _turretInventory = new();
    private int _inventoryWeight = 0;
    
    public int InventorySize => _allTurrets.Count;

    void Start()
    {
        foreach (var turret in _allTurrets)
        {
            _turretInventory.Add(turret, 0);
            var item = Instantiate(_inventoryItem, this.transform);
            item.TryGetComponent<Image>(out var turretIcon);
            var texture = PrefabUtility.GetIconForGameObject(turret.gameObject);
            turretIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }

    public Turret GetSelectedTurret(int selector)
    {
        return _turretInventory.ElementAt(selector).Key;
    }

    public bool TryAddTurret(Turret turret)
    {
        Debug.Log(turret.GetHashCode());
        if (_inventoryWeight + turret.Weight <= _maxInventoryWeight)
        {
            _turretInventory[turret]++;
            _inventoryWeight += turret.Weight;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryRemoveTurret(Turret turret)
    {
        if (_turretInventory[turret] > 0)
        {
            _turretInventory[turret]--;
            _inventoryWeight -= turret.Weight;
            return true;
        }
        else
        {
            return false;
        }
    }
}
