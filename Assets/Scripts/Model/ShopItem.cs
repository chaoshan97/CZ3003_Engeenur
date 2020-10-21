using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public int cost;
    public string property;
    public int no;
    public string description;

    public ShopItem(int cost, string property, int no, string description) {
        this.cost = cost;
        this.property = property;
        this.no = no;
        this.description = description;
    }
}
