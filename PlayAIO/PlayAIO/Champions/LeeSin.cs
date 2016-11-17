using System;
using System.Collections.Generic;
using System.Linq;

using LeagueSharp;
using LeagueSharp.Data.Enumerations;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.TSModes;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

using SharpDX;

namespace PlayAIO.Champions
{
    internal class LeeSin : ChampionManager
    {

        #region Menu
        private Menu ComboMenu;
        private Menu HarassMenu;
        private Menu ClearMenu;
        private Menu KSMenu;
        private Menu Misc;

        MenuBool ComboQ;
        MenuBool ComboQ2;
        MenuBool ComboW;
        MenuBool ComboE;
        MenuBool ComboR;

        MenuBool HarassQ;
        MenuBool HarassQ2;
        MenuBool HarassE;

        MenuBool ClearQ;
        MenuBool ClearQ2;
        MenuBool ClearW;
        MenuBool ClearE;

        MenuBool KSQ;
        MenuBool KSWardJump;
        MenuBool KSR;
        #endregion

        #region Spell Check
        private static bool QOne => Q.Instance.SData.Name.ToLower().Contains("one");
        private static bool WOne => W.Instance.SData.Name.ToLower().Contains("one");
        private static bool EOne => E.Instance.SData.Name.ToLower().Contains("one");
        #endregion

        public LeeSin()
        {
            #region Spells
            Q = new Spell(SpellSlot.Q, 1100).SetSkillshot(0.25f, 60, 1800, true, SkillshotType.SkillshotLine);
            Q2 = new Spell(Q.Slot, 1300);
            W = new Spell(SpellSlot.W, 700);
            E = new Spell(SpellSlot.E, 425).SetTargetted(0.25f, float.MaxValue);
            E2 = new Spell(E.Slot, 570);
            R = new Spell(SpellSlot.R, 375);
            R2 = new Spell(R.Slot).SetSkillshot(0.325f, 0, 950, false, SkillshotType.SkillshotLine);
            Q.DamageType = Q2.DamageType = W.DamageType = R.DamageType = DamageType.Physical;
            E.DamageType = DamageType.Magical;
            Q.MinHitChance = HitChance.VeryHigh;
            #endregion


            Ward.Init();
            Game.OnUpdate += OnUpdate;
        }

        private static void OnUpdate(EventArgs args)
        {
            //if (Player.IsDead || MenuGUI.IsChatOpen || MenuGUI.IsShopOpen || Player.IsRecalling())
            //{
            //    return;
            //}
            KillSteal();
            //switch (Variables.Orbwalker.GetActiveMode())
            //{
            //    case OrbwalkingMode.Combo:
            //        Combo();
            //        break;
            //    case OrbwalkingMode.Hybrid:
            //        Harass();
            //        break;
            //    case OrbwalkingMode.LaneClear:
            //        Clear();
            //        break;
            //}
        }

        private static void Combo()
        {
        }

        private static void Harass()
        {
        }

        private static void Clear()
        {
        }

        private static void KillSteal()
        {
            if (QOne && Q.IsReady())
            {
                var target = Q.GetTarget(Q.Width / 2);
                if (target.Health <= Q.GetDamage(target))
                {
                    Q.Cast(target);
                }
            }
        }

        private static double GetQ2Dmg(Obj_AI_Base target, double subHp)
        {
            var dmg = new[] { 50, 80, 110, 140, 170 }[Q.Level - 1] + 0.9 * Player.FlatPhysicalDamageMod
                      + 0.08 * (target.MaxHealth - (target.Health - subHp));
            return Player.CalculateDamage(
                target,
                DamageType.Physical,
                target is Obj_AI_Minion ? Math.Min(dmg, 400) : dmg) + subHp;
        }

        private class Ward
        {
            internal static void Init()
            {

            }
        }
    }
}
