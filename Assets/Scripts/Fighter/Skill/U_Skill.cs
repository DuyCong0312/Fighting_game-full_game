using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Skill : BaseSkill
{
    protected override KeyCode SkillKey => KeyCode.U;

    protected override string GroundAnimationTrigger => CONSTANT.Uskill;

    protected override string AirAnimationName => CONSTANT.UKskill;

    protected override void ActiveSkill()
    {
        if (Input.GetKeyDown(SkillKey))
        {
            playerRage.GetRage(5f); 
            PerformSkill();
        }
    }
}
