using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MyMonoBehaviour
{
    public static UIManager instance;

    public const int MENU_PANEL = 0;
    public const int SHOP_PANEL = 1;

    public Canvas gameCanvas;
    public FixedJoystick fixedJoystick;
    public Panel currentPanel;
    public MenuPanel menuPanel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (instance != null)
            Debug.Log("Only 1 UIManager is allowed to be created");
        instance = this;
        gameCanvas = transform.Find("Canvas").GetComponent<Canvas>();
        fixedJoystick = FindObjectOfType<FixedJoystick>();

        LoadPanels();
    }

    protected virtual void LoadPanels() {
        menuPanel = gameCanvas.gameObject.transform.Find("MenuPanel").GetComponent<MenuPanel>();
        menuPanel.gameObject.SetActive(false);
    }

    public void SetPanel(int panel) {
        switch (panel) {
            case MENU_PANEL:
                currentPanel = menuPanel;
                break;
        }
    }

    public void ShowPanel() {
        currentPanel.Show();
    }

    public void HidePanel() {
        currentPanel.Hide();
        currentPanel = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
