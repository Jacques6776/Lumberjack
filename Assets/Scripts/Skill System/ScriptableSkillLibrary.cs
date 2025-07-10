using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Skill_System
{
    [CreateAssetMenu(fileName = "New Skill Library", menuName = "Skill System/New Skill Library", order = 0)]
    public class ScriptableSkillLibrary : ScriptableObject
    {
        public List<ScriptableSkill> SkillLibrary;

        //Allows us to have all skills in the library, this method will allow us to find tier specific skills
        public List<ScriptableSkill> GetSkillsOfTier(int tier)
        {
            return SkillLibrary.Where(skill => skill.skillTier == tier).ToList();
        }
    }
}
