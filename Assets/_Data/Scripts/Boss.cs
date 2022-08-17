using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Mob
{
    public BossHealthBar bossHealthBar;

    public override void Death()
    {
        base.Death();
        if (bossHealthBar != null)
            bossHealthBar.Hide();
    }

    public override void ReSpawn()
    {
        base.ReSpawn();
        if (bossHealthBar != null)
            bossHealthBar.Show();
    }

    private void OnDestroy() {
         if (bossHealthBar != null)
            Destroy(bossHealthBar.gameObject);
    }
}
