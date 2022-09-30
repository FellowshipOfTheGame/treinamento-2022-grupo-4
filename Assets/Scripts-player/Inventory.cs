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

    [SerializeField]
    private GameObject WeightText;

    private Dictionary<Turret, int> _turretInventory = new();
    private int _inventoryWeight = 0;
    private List<GameObject> _inventoryItems = new();
    
    public int InventorySize => _allTurrets.Count;

    void Start()
    {
        foreach (var turret in _allTurrets)
        {
            _turretInventory.Add(turret, 0);
            var item = Instantiate(_inventoryItem, this.transform);
            item.TryGetComponent<Image>(out var turretIcon);
            if (turret.TryGetComponent<Image>(out var texture))
                turretIcon.sprite = texture.sprite;
            _inventoryItems.Add(item);
            UpdateInventoryValue(turret);
        }

        UpdateSelection(0);
        UpdateWeight();
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
            UpdateInventoryValue(turret);
            UpdateWeight();
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
            UpdateInventoryValue(turret);
            UpdateWeight();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateSelection(int selector)
    {
        foreach (var item in _inventoryItems)
        {
            item.GetComponent<Outline>().effectColor = new Color(255, 0, 0, 0);
        }

        _inventoryItems[selector].GetComponent<Outline>().effectColor = new Color(255, 0, 0, 100);
    }

    private void UpdateInventoryValue(Turret turret)
    {
        var index = _allTurrets.IndexOf(turret);
        _inventoryItems[index].GetComponentInChildren<Text>().text = _turretInventory[turret].ToString();
    }

    private void UpdateWeight()
    {
        WeightText.GetComponent<Text>().text = _inventoryWeight.ToString();
    }
}
