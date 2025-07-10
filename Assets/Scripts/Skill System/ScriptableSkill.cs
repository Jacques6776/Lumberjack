using NUnit.Framework;
using System.Collections.Generic;//Need thos for the list
using System.Text; //Need this for stringbuilder
using UnityEngine;

namespace Scripts.Skill_System
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System/New Skill", order = 0)]

    public class ScriptableSkill : ScriptableObject 
    {
        //Setting up for use of Unity UI tool
        public List<UpgradeData> UpgradeData = new List<UpgradeData>();//Can also be done with an array
        public bool isAbility; //Defirentiates the abilities from stat increases
        public string skillName;
        public bool overwriteDescription; //Allows us to make custom descriptions
        [TextArea(1, 4)] public string skillDescription;
        public Sprite skillIcon;
        public List<ScriptableSkill> SkillPrerequisites = new List<ScriptableSkill>();

        public int skillTier; //will be more relevant to the UI building
        public int cost; //How many skillpoints it will take to unlock an ability

        //Automatic generation of skill description
        private void OnValidate()
        {
            if (skillName == "") skillName = name;
            if (UpgradeData.Count == 0) return;
            if (overwriteDescription) return;
            
            GenerateDescription();
        }

        private void GenerateDescription()
        {
            if (isAbility)
            {
                switch (UpgradeData[0].statType)
                {
                    case StatTypes.DoubleJump:
                        skillDescription = $"{skillName} grants double jump.";
                        break;
                    case StatTypes.Dash:
                        skillDescription = $"{skillName} grants dash.";
                            break;
                    case StatTypes.Teleport:
                        skillDescription = $"{skillName} grants teleport.";
                            break;
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();//more efficient way to make long strings READ MORE
                sb.Append($"{skillName} increases ");
                for (int i = 0; i < UpgradeData.Count; i++)
                {
                    sb.Append(UpgradeData[i].statType.ToString());
                    sb.Append(" by ");
                    sb.Append(UpgradeData[i].skillIncreaseAmount.ToString());
                    sb.Append(UpgradeData[i].isPercentage ? "%" : " point(s)");
                    if (i == UpgradeData.Count - 2) sb.Append(" and ");
                    else sb.Append(i < UpgradeData.Count - 1 ? ", " : ".");
                }

                skillDescription = sb.ToString();
            }
        }
    }

    [System.Serializable]
    public class UpgradeData 
    {
        public StatTypes statType;
        public int skillIncreaseAmount;
        public bool isPercentage;
    }

    public enum StatTypes
    {
        Strength,
        Dexterity,
        Intelligence,
        Wisdom,
        Charisma,
        Constitution,
        DoubleJump,
        Dash,
        Teleport
    }
}
