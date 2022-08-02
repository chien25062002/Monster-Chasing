using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Panel
{
    protected virtual void LoadInventoryData() {
        // for override
    }

    public override void Show()
    {
        LoadInventoryData();
        base.Show();
    }
}
