using Scripts.Skill_System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[System.Serializable]
public class UITalentButton
{
    private Button button;
    private ScriptableSkill skill;
    private bool isUnlocked = false;

    public static UnityAction<ScriptableSkill> OnSkillButtonClicked;

    public UITalentButton(Button assignedButton, ScriptableSkill assignedSkill)
    {
        button = assignedButton;
        button.clicked += OnClick;
        skill = assignedSkill;
        if (assignedSkill.skillIcon) button.style.backgroundImage = new StyleBackground(assignedSkill.skillIcon);
    }

    private void OnClick()
    {
        OnSkillButtonClicked?.Invoke(skill);
    }
}
