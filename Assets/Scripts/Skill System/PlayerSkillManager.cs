using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events; //for the unityaction method
//Events allow you to call on all methods that subscribe to the event. Can also skip over methods that do not activate
//Decresce coupling. Methods can be more seperate

namespace Scripts.Skill_System
{
    public class PlayerSkillManager : MonoBehaviour
    {
        private int strength, dexterity, intelligence, wisdom, charisma, constitution; //Stats
        private int doubleJump, dash, teleport; //Unlockable abilities    
        [SerializeField]
        private int skillPoints;

        //all below allows for modifications that could be superficial or momentary. The private variable will be the set values
        public int Strength => strength;
        public int Dexterity => dexterity;
        public int Intelligence => intelligence;
        public int Wisdom => wisdom;
        public int Charisma => charisma;
        public int Constitution => constitution;

        public bool DoubleJump => doubleJump > 0;
        public bool Dash => dash > 0;
        public bool Teleport => teleport > 0;

        public int SkillPoints => skillPoints;

        //Event that is invoked when skillpoints change
        public UnityAction OnSkillPointsChanged;

        //the list containing the unlocked skills
        private List<ScriptableSkill> unlockedSkills = new List<ScriptableSkill>();

        private void Awake()
        {
            strength = 10;
            dexterity = 10;
            intelligence = 10;
            wisdom = 10;
            charisma = 10; 
            constitution = 10;
            skillPoints = 10;
        }

        public void GainSkillPoint()
        {
            skillPoints++;
            OnSkillPointsChanged?.Invoke();//Starts the Event
        }

        public bool CanAffordSkill(ScriptableSkill skill)
        {
            return skillPoints >= skill.cost;
        }

        public void UnlockSkill(ScriptableSkill skill)
        {
            if (!CanAffordSkill(skill)) return;

            //Will actually modify the stats
            ModifyStats(skill);
            
            //will add all unlocked skills to a list
            unlockedSkills.Add(skill);
            //Will take the cost of the skillpoint from the total skill points
            skillPoints -= skill.cost;
            OnSkillPointsChanged?.Invoke();
        }

        private void ModifyStats(ScriptableSkill skill)
        {
            //Loop through UpgradeData list (ScriptableSkill) and apply changes
            foreach (UpgradeData data in skill.UpgradeData)
            {
                switch (data.statType)
                {
                    case StatTypes.Strength:
                        //making new method to clean the switch. Will add the modification to each stat individually
                        ModifyStat(ref strength, data);
                        break;
                    case StatTypes.Dexterity:
                        ModifyStat(ref dexterity, data);
                        break;
                    case StatTypes.Intelligence:
                        ModifyStat(ref intelligence, data);
                        break;
                    case StatTypes.Wisdom:
                        ModifyStat(ref wisdom, data);
                        break;
                    case StatTypes.Charisma:
                        ModifyStat(ref charisma, data);
                        break;
                    case StatTypes.Constitution:
                        ModifyStat(ref constitution, data);
                        break;
                    
                    //Handle the ability upgrades
                    case StatTypes.DoubleJump:
                        ModifyStat(ref doubleJump, data);
                        break;
                    case StatTypes.Dash:
                        ModifyStat(ref dash, data);
                        break;
                    case StatTypes.Teleport:
                        ModifyStat(ref teleport, data);
                        break;
                    //default:
                      //  throw new ArgumentOutOfRangeException();
                }
            }
        }

        //Checks if the skill in in the unlocked list
        public bool IsSkillUnlocked(ScriptableSkill skill)
        {
            return unlockedSkills.Contains(skill);
        }

        public bool PreReqsMet(ScriptableSkill skill)
        {
            return skill.SkillPrerequisites.Count == 0 || skill.SkillPrerequisites.All(unlockedSkills.Contains);//Seconf part is a Linq function. System.Linq has been added
        }

        private void ModifyStat(ref int stat, UpgradeData data)//a reference value
        {
            bool isPercent = data.isPercentage;

            if (isPercent)
            {
                //If percentage increase, it will cast the calculation into an int form
                stat += (int)(stat * (data.skillIncreaseAmount / 100f));
            }
            else
            {
                //For the flat number increase
                stat += data.skillIncreaseAmount;
            }
        }
    }
}
