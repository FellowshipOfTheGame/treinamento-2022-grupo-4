
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private int _maxMoney = 10;
    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private int _money;

    [SerializeField]
    private int _selector = 0;

    public void BuyTurret()
    {
        var turret = _inventory.GetSelectedTurret(_selector);
        var cost = turret.Cost;
        if (_money < cost)
        {
            Debug.Log("Not enough money");
        }
        else
        {
            _money -= cost;
            if (!_inventory.TryAddTurret(turret))
            {
                Debug.Log("Too heavy");
            }
        }
    }

    public Turret GetSelectedTurret()
    {
        return _inventory.GetSelectedTurret(_selector);
    }

    public void AddMoney(int money)
    {
        if (_money + money <= _maxMoney)
            _money += money;
    }

    public void MoveSelector()
    {
        if (_selector + 1 == _inventory.InventorySize) _selector = 0;
        else _selector++;
        _inventory.UpdateSelection(_selector);
    }
}
