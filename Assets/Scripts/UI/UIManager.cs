using Scripts.Skill_System;
using UnityEngine;
using UnityEngine.UIElements;//Needed for the UIDocument element

public class UIManager : MonoBehaviour
{
    private PlayerSkillManager playerSkillManager;
    public PlayerSkillManager PlayerSkillManager => playerSkillManager;

    private UIDocument uiDocument;
    public UIDocument UIDocument => uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        playerSkillManager = FindFirstObjectByType<PlayerSkillManager>();
    }
}
