using UnityEngine;

public class HeroSkill : MonoBehaviour
{
    // Hero skills
    public HeroSkillDatabase.Skill[] HeroSkills { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Init();
    }

    // Set basic parameters
    private void Init()
    {
        // Paladin
        if (name.Equals(HeroDatabase.Paladin))
        {
            // Copy skills from database
            HeroSkills = (HeroSkillDatabase.Skill[])HeroSkillDatabase.PaladinSkills.Clone();
            // Search proper skill
            for (int cnt = 0; cnt < HeroSkills.Length; cnt++)
            {
                // Set starting stats
                HeroSkills[cnt].Effect = 0f;
                HeroSkills[cnt].EnergyCost = 0f;
            }
        }
    }
}