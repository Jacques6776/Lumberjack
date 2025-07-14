using Scripts.Skill_System;
using UnityEngine;
using UnityEngine.UIElements;

public class UISkillDescriptionPanel : MonoBehaviour
{
    private UIManager uiManager;
    private ScriptableSkill assignedSkill;
    private VisualElement skillImage;
    private Label skillNameLabel, skillDescriptionLabel, skillCostLabel, skillPreReqLabel;
    private Button purchaseSkillButton;

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
    }

    private void OnEnable()
    {
        UITalentButton.OnSkillButtonClicked += PopulateLabelText;
    }

    private void OnDisable()
    {
        UITalentButton.OnSkillButtonClicked -= PopulateLabelText;
        if (purchaseSkillButton != null) purchaseSkillButton.clicked -= PurchaseSkill;
    }

    private void Start()
    {
        GatherLabelReferences();
        var skill = uiManager.SkillLibrary.GetSkillsOfTier(1)[0];
        PopulateLabelText(skill);
    }

    private void GatherLabelReferences()
    {
        skillImage = uiManager.UIDocument.rootVisualElement.Q<VisualElement>(name: "SkillIcon");
        skillNameLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "SkillNameLabel");
        skillDescriptionLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "SkillDescriptionLabel");
        skillCostLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "SkillCostLabel");
        skillPreReqLabel = uiManager.UIDocument.rootVisualElement.Q<Label>(name: "PreReqLabel");
        purchaseSkillButton = uiManager.UIDocument.rootVisualElement.Q<Button>(name: "BuySkillButton");
        purchaseSkillButton.clicked += PurchaseSkill;
    }

    private void PurchaseSkill()
    {
        if (uiManager.PlayerSkillManager.CanAffordSkill(assignedSkill))
        {
            uiManager.PlayerSkillManager.UnlockSkill(assignedSkill);
            PopulateLabelText(assignedSkill);
        }
    }

    private void PopulateLabelText(ScriptableSkill skill)
    {
        if (!skill) return;
        assignedSkill = skill;

        if (assignedSkill.skillIcon) 
            skillImage.style.backgroundImage = new StyleBackground(assignedSkill.skillIcon);
        skillNameLabel.text = assignedSkill.skillName;
        skillDescriptionLabel.text = assignedSkill.skillDescription;
        skillCostLabel.text = $"Cost: {skill.cost}";

        if (assignedSkill.SkillPrerequisites.Count > 0)
        {
            skillPreReqLabel.text = "Requirements: ";
            foreach (var preReq in assignedSkill.SkillPrerequisites)
            {
                string punctuation = preReq == assignedSkill.SkillPrerequisites[assignedSkill.SkillPrerequisites.Count - 1] ? "" : ", ";
                skillPreReqLabel.text += $" {preReq.skillName}{punctuation}";
            }
        }
        else
        {
            skillPreReqLabel.text = "";
        }

        if (uiManager.PlayerSkillManager.IsSkillUnlocked(assignedSkill))
        {
            purchaseSkillButton.text = "Purchased";
            purchaseSkillButton.SetEnabled(false);
        }
        else if (!uiManager.PlayerSkillManager.PreReqsMet(assignedSkill))
        {
            purchaseSkillButton.text = "Requirements Not Met";
            purchaseSkillButton.SetEnabled(false);
        }
        else if (!uiManager.PlayerSkillManager.CanAffordSkill(assignedSkill))
        {
            purchaseSkillButton.text = "Can't Afford";
            purchaseSkillButton.SetEnabled(false);
        }
        else
        {
            purchaseSkillButton.text = "Purchased";
            purchaseSkillButton.SetEnabled(true);
        }
    }
}
