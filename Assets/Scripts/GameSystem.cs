using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    private int _money;
    private int _inventoryWeight;
    private List<int> _turrets;

    [SerializeField]
    private static int _maxInventoryWeight = 20;
    [SerializeField]
    private static int _maxMoney = 10;

}
