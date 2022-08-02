using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameScreen : MyMonoBehaviour
{
    public static GameScreen instance;
    public const int GAME_PANEL = 0;
    public const int MENU_PANEL = 1;
    public const int SHOP_PANEL = 2;
    public const int START_GAME_PANEL = 3;
    public const int LOADING_GAME_SCREEN_PANEL = 4;

    public List<Transform> visibleMobs = new List<Transform>();

    public Transform gameCanvas;
    public Panel currentPanel;
    public GamePanel gamePanel;
    public MenuPanel menuPanel;
    public StartGamePanel startGamePanel;
    public LoadingScreenPanel loadingScreenPanel;

    public int width, height;

    protected override void Awake()
    {
        if (instance != null)
            Debug.Log("Only 1 GameScreen instance is allowed to be created");
        instance = this;
        base.Awake();
        currentPanel = startGamePanel;
        DontDestroyOnLoad(gameCanvas.gameObject);
    }

    private void Update() {
        UpdateVisibleMob();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPanels();
        Canvas canvas = transform.parent.GetComponent<Canvas>();
        width = (int) canvas.GetComponent<RectTransform>().rect.width;
        height = (int) canvas.GetComponent<RectTransform>().rect.height;
    }

    protected virtual void LoadPanels() {
        currentPanel = startGamePanel;
        gameCanvas = gameObject.transform.parent.transform;
        gamePanel = this.transform.Find("GamePanel").GetComponent<GamePanel>();
        menuPanel = this.transform.Find("MenuPanel").GetComponent<MenuPanel>();
        startGamePanel = this.transform.Find("StartGamePanel").GetComponent<StartGamePanel>();
        loadingScreenPanel = this.transform.Find("LoadingScreenPanel").GetComponent<LoadingScreenPanel>();
        HideAllPanel();
    }

    public virtual void StartScreenGame() {
        startGamePanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(true);
        currentPanel = gamePanel;
    }

    protected virtual void HideAllPanel() {
        menuPanel.gameObject.SetActive(false);
    }

    public void SetPanel(int panelIndex) {
        if (currentPanel != null && currentPanel != gamePanel) {
            currentPanel.Hide();
        }
        switch(panelIndex) {
            case MENU_PANEL:
                currentPanel = menuPanel;
                break;
            case GAME_PANEL:
                currentPanel = gamePanel;
                break;
            case START_GAME_PANEL:
                currentPanel = startGamePanel;
                break;
            case LOADING_GAME_SCREEN_PANEL:
                currentPanel = loadingScreenPanel;
                break;
        }
        Show();
    }

    public void Show() {
        if (currentPanel != null) {
            currentPanel.Show();
        }
    }

    public void Hide() {
        if (currentPanel != null) {
            currentPanel.Hide();
            currentPanel = null;
        }
    }

    protected virtual void UpdateVisibleMob() {
        if (GameManager.instance.currentMap != null) {
            foreach (Transform mob in GameManager.instance.currentMap.spawnedMobs) {
                if (IsMobInCamera(mob)) {
                    if (!visibleMobs.Contains(mob))
                        visibleMobs.Add(mob);
                }
                if (!IsMobInCamera(mob)) {
                    if (visibleMobs.Contains(mob))
                        visibleMobs.Remove(mob);
                }
            }
        }
    }

    public bool IsMobInCamera(Transform mob) {
        Vector3 mobPos = mob.position;
        if (mobPos.x >= CameraFollow.instance.camLeftBounds && mobPos.x <= CameraFollow.instance.camRightBounds &&
        mobPos.y >= CameraFollow.instance.camBottomBounds && mobPos.y <= CameraFollow.instance.camTopBounds)
            return true;
        return false;
    }
}
