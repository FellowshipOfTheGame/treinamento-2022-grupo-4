
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject MoneyText;

    private void Start()
    {
        UpdateMoney();
    }

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
            if (_inventory.TryAddTurret(turret))
            {
                UpdateMoney();
            }
            else
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
        {
            _money += money;
            UpdateMoney();
        }
    }

    public void MoveSelector()
    {
        if (_selector + 1 == _inventory.InventorySize) _selector = 0;
        else _selector++;
        _inventory.UpdateSelection(_selector);
    }

    private void UpdateMoney()
    {
        MoneyText.GetComponent<Text>().text = _money.ToString();
    }
}
