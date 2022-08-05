using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePanel : Panel
{
    [SerializeField] protected Button menuButton;
    [SerializeField] protected Button attackButton;
    [SerializeField] protected Button changeFocusButton;
    [SerializeField] protected FixedJoystick fixedJoystick;
    [SerializeField] protected Transform skillBar;
    [SerializeField] protected Button skill1;
    [SerializeField] protected Button skill2;
    [SerializeField] protected Button skill3;
    [SerializeField] protected Button skill4;
    [SerializeField] protected Button skill5;
    [SerializeField] protected Button currentSkill;
    private int centerX, centerY;

    public MyImage focusImage;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        this.LoadSkillBar();
        this.LoadButton();
        focusImage = MyImage.CreateImage("FocusArrow");
        GUILayout.Width(1920);
        GUILayout.Height(1080);
    }

    protected virtual void LoadButton() {
        menuButton = this.transform.Find("MenuButton").GetComponent<Button>();
        menuButton.onClick.AddListener(delegate {GameScreen.instance.SetPanel(GameScreen.MENU_PANEL);});
        attackButton = this.transform.Find("AttackButton").GetComponent<Button>();
        attackButton.onClick.AddListener(delegate {SkillClicked(currentSkill);});

        changeFocusButton = this.transform.Find("ChangeFocus").Find("ChangeFocusButton").GetComponent<Button>();
        changeFocusButton.onClick.AddListener(delegate {PlayerController.instance.character.ChangeFocus();});
    }

    protected virtual void LoadSkillBar() {
        skillBar = this.transform.Find("SkillBar");
        skill1 = skillBar.Find("Skill1").Find("Skill1Button").GetComponent<Button>();
        skill2 = skillBar.Find("Skill2").Find("Skill2Button").GetComponent<Button>();
        skill3 = skillBar.Find("Skill3").Find("Skill3Button").GetComponent<Button>();
        skill4 = skillBar.Find("Skill4").Find("Skill4Button").GetComponent<Button>();
        skill5 = skillBar.Find("Skill5").Find("Skill5Button").GetComponent<Button>();

        skill1.GetComponent<ObjectSkill>().SetData(PlayerController.instance.character.skills[0]);
        skill2.GetComponent<ObjectSkill>().SetData(PlayerController.instance.character.skills[1]);
        skill1.onClick.AddListener(delegate {SkillClicked(skill1);});
        skill2.onClick.AddListener(delegate {SkillClicked(skill2);});

        currentSkill = skill1;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = fixedJoystick.Horizontal;
        float moveVertical = fixedJoystick.Vertical;
        PlayerController.instance.playerMovement.moveX = moveHorizontal;
        PlayerController.instance.playerMovement.moveY = moveVertical;
    }

    public void UpdateCharacter() {
        
    }

    public void SkillClicked(Button skill) {
        if (PlayerController.instance.character.mobFocus == null)
            return;
        if (!skill.GetComponent<ObjectSkill>().isCooldown) {
            currentSkill = skill;
            PlayerController.instance.character.Attack(skill.GetComponent<ObjectSkill>().skillName);
            skill.GetComponent<ObjectSkill>().cooldownTimer = skill.GetComponent<ObjectSkill>().cooldown;
            skill.GetComponent<ObjectSkill>().isCooldown = true;
            StartCoroutine(UpdateSkill(skill));
        }
    }

    IEnumerator UpdateSkill(Button skill) {
        ObjectSkill objectSkill = skill.GetComponent<ObjectSkill>();
        while (true) {
            objectSkill.cooldownTimer -= Time.deltaTime;
            skill.gameObject.transform.Find("SkillFreezing").GetComponent<Image>().fillAmount = objectSkill.cooldownTimer / objectSkill.cooldown;
            if (skill.GetComponent<ObjectSkill>().cooldownTimer <= 0) {
                PlayerController.instance.character.GetSkillByName(skill.GetComponent<ObjectSkill>().skillName).isCooldown = false;
                objectSkill.isCooldown = false;
                skill.gameObject.transform.Find("SkillFreezing").GetComponent<Image>().fillAmount = 0;
                break;
            }
            yield return null;
        }
    }

    private void OnGUI() {
        if (PlayerController.instance.character.mobFocus != null) {
            Vector3 pos = PlayerController.instance.character.mobFocus.transform.position;
            pos.y += 3.5f;
            Vector3 position = Camera.main.WorldToScreenPoint(pos);
            position.y = GameScreen.instance.height - position.y;
            GUI.DrawTexture(new Rect(position.x - focusImage.width, position.y - focusImage.height, focusImage.width, focusImage.height), focusImage.texture);
        }
    }
}
