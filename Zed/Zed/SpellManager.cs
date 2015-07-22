using LeagueSharp;
using LeagueSharp.SDK.Core.Enumerations;
using LeagueSharp.SDK.Core.Extensions;
using LeagueSharp.SDK.Core.Wrappers;
using SharpDX;

namespace Zed
{
    class SpellManager
    {
        private static Obj_AI_Hero Player = ObjectManager.Player;

        private static Spell _Q, _W, _E, _R;

        public static Spell Q { get { return _Q; } }
        public static Spell W { get { return _W; } }
        public static Spell E { get { return _E; } }
        public static Spell R { get { return _R; } }

        public static void Initialize()
        {
            _Q = new Spell(SpellSlot.Q, 900);
            _W = new Spell(SpellSlot.W, 550);
            _E = new Spell(SpellSlot.E, 270);
            _R = new Spell(SpellSlot.R, 650);


            _Q.SetSkillshot(0.25f, 50f, 1700f, false, SkillshotType.SkillshotLine);
        }
    }
}
