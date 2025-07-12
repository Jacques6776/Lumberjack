using UnityEngine.UIElements;
using UnityEngine;

public class UIStatPanel : MonoBehaviour
{
    private Label strengthLabel, dexterityLabel, intelligenceLabel, wisdomLabel, charismaLabel, constitutionLabel;
    private Label doubleJumpLabel, dashLabel, teleportLabel;
    private Label skillPointsLabel;

    private UIManager uiManager;

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        uiManager.PlayerSkillManager.OnSkillPointsChanged += PopulateLabelText;
        GatherLabelReferences();
        PopulateLabelText();
    }

    private void GatherLabelReferences()
    {
        strengthLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "StatLabel_Strength");
        dexterityLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "StatLabel_Dexterity");
        intelligenceLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "StatLabel_Intelligence");
        wisdomLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "StatLabel_Wisdom");
        charismaLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "StatLabel_Charisma");
        constitutionLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "StatLabel_Constitution");

        doubleJumpLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "AbilityLabel_DoubleJump");
        dashLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "AbilityLabel_Dash");
        teleportLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "AbilityLabel_Teleport");

        skillPointsLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "SkillPointsLabel");
    }

    private void PopulateLabelText()
    {
        strengthLabel.text = "STR - " + uiManager.PlayerSkillManager.Strength.ToString();
        dexterityLabel.text = "DEX - " + uiManager.PlayerSkillManager.Dexterity.ToString();
        intelligenceLabel.text = "INT - " + uiManager.PlayerSkillManager.Intelligence.ToString();
        wisdomLabel.text = "WIS - " + uiManager.PlayerSkillManager.Wisdom.ToString();
        charismaLabel.text = "CHA - " + uiManager.PlayerSkillManager.Charisma.ToString();
        constitutionLabel.text = "CON - " + uiManager.PlayerSkillManager.Constitution.ToString();

        skillPointsLabel.text = "Skill Points: " + uiManager.PlayerSkillManager.SkillPoints.ToString();

        doubleJumpLabel.text = "Double Jump: " + (uiManager.PlayerSkillManager.DoubleJump ? "Unlocked" : "Locked");
        dashLabel.text = "Dash: " + (uiManager.PlayerSkillManager.Dash ? "Unlocked" : "Locked");
        teleportLabel.text = "Teleport: " + (uiManager.PlayerSkillManager.Teleport ? "Unlocked" : "Locked");
    }
}
