using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreasementInfo : MyMonoBehaviour
{
    public static IncreasementInfo instance;

    public Text line1;
    public Text line2;
    public Text increase1Text;

    protected override void Awake() {
        base.Awake();
        if (instance != null)
            Debug.Log("Only 1 IncreasementInfo instance is allowed to be created");
        instance = this;
    }

    protected override void LoadComponents()
    {
        line1 = transform.Find("Info/Line1").GetComponent<Text>();
        line2 = transform.Find("Info/Line2").GetComponent<Text>();
        increase1Text = transform.Find("Increase1/Text").GetComponent<Text>();
    }

    public void SetData(Transform potential) {
        Show();
        int rootValue = PlayerController.instance.character.GetPropertyByName(potential.name);
        line1.text = "Sử dụng ";
        if (potential.name == "HealthPoint") {
            line1.text += rootValue + 200 + " tiềm năng";
            line2.text = "để tăng 20 HP gốc";
            increase1Text.text = "Tăng\n20HP\n-" + (rootValue + 200);
        }
        if (potential.name == "ManaPoint") {
            line1.text += rootValue + 200 + " tiềm năng";
            line2.text = "để tăng 20 MP gốc";
            increase1Text.text = "Tăng\n20MP\n-" + (rootValue + 200);
        }
        if (potential.name == "Damage") {
            line1.text += rootValue * 10 + " tiềm năng";
            line2.text = "để tăng 1 tấn công gốc";
            increase1Text.text = "Tăng\n1\nsức đánh\n-" + (rootValue * 10);
        }
        if (potential.name == "Crit") {
            line1.text += rootValue * 1000 + " tiềm năng";
            line2.text = "để tăng 1 chí mạng gốc";
            increase1Text.text = "Tăng\n1\nchí mạng\n-" + (rootValue * 1000);
        }
        transform.position = potential.position;
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
