using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillButton : MonoBehaviour
{
    //public Image skillImage;
    public Text skillNameText;
    public Text skillDesText;

    public int skillNum;

    public void PressSkillButton()
    {
        SkillManager.instance.activateSkill = transform.GetComponent<Skill>();

        //skillImage.sprite = SkillManager.instance.skills[skillNum].skillSprite;
        skillNameText.text = SkillManager.instance.skills[skillNum].skillName;
        skillDesText.text = SkillManager.instance.skills[skillNum].skillDes;
    }

}
