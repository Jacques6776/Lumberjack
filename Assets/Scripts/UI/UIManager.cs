using Scripts.Skill_System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;//Needed for the UIDocument element

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ScriptableSkillLibrary skillLibrary;
    public ScriptableSkillLibrary SkillLibrary => skillLibrary;
    [SerializeField]
    private VisualTreeAsset uiTalentButton;
    private PlayerSkillManager playerSkillManager;
    public PlayerSkillManager PlayerSkillManager => playerSkillManager;

    private UIDocument uiDocument;
    public UIDocument UIDocument => uiDocument;

    private VisualElement skillTopRow, skillMiddleRow, skillBottomRow;
    [SerializeField]
    private List<UITalentButton> talentButtons;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        playerSkillManager = FindFirstObjectByType<PlayerSkillManager>();
    }

    private void Start()
    {
        CreateSkillButtons();
    }

    private void CreateSkillButtons()
    {
        var root = uiDocument.rootVisualElement;
        skillTopRow = root.Q<VisualElement>(name: "SkillRowOne");
        skillMiddleRow = root.Q<VisualElement>(name: "SkillRowTwo");
        skillBottomRow = root.Q<VisualElement>(name: "SkillRowThree");

        SpawnButtons(skillTopRow, skillLibrary.GetSkillsOfTier(1));
        SpawnButtons(skillMiddleRow, skillLibrary.GetSkillsOfTier(2));
        SpawnButtons(skillBottomRow, skillLibrary.GetSkillsOfTier(3));
    }

    private void SpawnButtons(VisualElement parent, List<ScriptableSkill> skills)
    {
        foreach (var skill in skills)
        {
            Button clonedButton = uiTalentButton.CloneTree().Q<Button>();
            talentButtons.Add(item: new UITalentButton(clonedButton, skill));
            parent.Add(clonedButton);
        }
    }
}
