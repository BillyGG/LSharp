using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using SharpDX;
using PredictionInput = LeagueSharp.Common.PredictionInput;
using SkillshotType = LeagueSharp.SDK.Enumerations.SkillshotType;
using Spell = LeagueSharp.SDK.Spell;

namespace PlayAIO.Utilities
{
    public static class Prediction
    {
        public static MenuList<string> PredictionMode;
        public static object GetPrediction(this Spell spell, Obj_AI_Base target)
        {
            switch (PredictionMode.SelectedValue)
            {
                case "SDK":
                    {
                        return spell.GetPrediction(target);
                    }
                case "Common":
                    {
                        var commonSpell = new Spell(spell.Slot, spell.Range);
                        commonSpell.SetSkillshot(spell.Delay, spell.Width, spell.Speed, spell.Collision, GetCommonSkillshotType(spell.Type));
                        return commonSpell.GetPrediction(target);
                    }
                default:
                    {
                        return spell.GetPrediction(target);
                    }
            }
        }

        private static SkillshotType GetCommonSkillshotType(SkillshotType type)
        {
            switch (type)
            {
                case SkillshotType.SkillshotCircle:
                    return SkillshotType.SkillshotCircle;
                case SkillshotType.SkillshotCone:
                    return SkillshotType.SkillshotCone;
                case SkillshotType.SkillshotLine:
                    return SkillshotType.SkillshotLine;
                default:
                    return SkillshotType.SkillshotLine;
            }
        }
    }
}
