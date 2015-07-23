using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.Common.Data;
using SharpDX;
using System.Drawing;

namespace MadlifeThresh
{
    enum Spells
    {
        Q, Q2, W, E, R
    }

    internal class MadlifeThresh
    {

        public static Orbwalking.Orbwalker Orbwalker;
        public static CharacterData CharData;
        public static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        public static Obj_AI_Base Minionerimo;

        private static MadlifeThreshMenu _menu;
        private static Spell _q;
        private static Spell _q2;
        private static Spell _w;
        private static Spell _e;
        private static Spell _r;

        public static String ThreshQBuff = "threshqfakeknockup";
        public static float FirstQTime;

        public static float FlashRange = 450f;

        public static Dictionary<Spells, Spell> spells = new Dictionary<Spells, Spell>()
        {
            { Spells.Q, new Spell(SpellSlot.Q, 1100) },
            { Spells.Q2, new Spell(SpellSlot.Q, 1400) },
            { Spells.W, new Spell(SpellSlot.W, 950) },
            { Spells.E, new Spell(SpellSlot.E, 400) },
            { Spells.R, new Spell(SpellSlot.R, 450) }
        };

        public static void Game_OnGameLoad(EventArgs args)
        {
            spells[Spells.Q].SetSkillshot(0.500f, 70f, 1900f, true, SkillshotType.SkillshotLine);
            spells[Spells.Q2].SetSkillshot(0.500f, 70f, 1900f, true, SkillshotType.SkillshotLine);

            MadlifeThreshMenu.Initialize();
            Game.OnUpdate += OnGameUpdate;
            //Drawing.OnDraw += Drawings.Drawing_OnDraw;
            //Drawing.OnEndScene += Drawings.OnDrawEndScene;
        }

        private static void OnGameUpdate(EventArgs args)
        {
            switch (Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
                case Orbwalking.OrbwalkingMode.LaneClear:
                    Clear();
                    break;
                case Orbwalking.OrbwalkingMode.LastHit:
                    LastHit();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harass();
                    break;
            }
        }

        private static void OnPossibleToInterrupt(Obj_AI_Hero target, Interrupter2.InterruptableTargetEventArgs args)
        {
            if (MadlifeThreshMenu._menu.Item("Thresh.Interrupt.E").GetValue<bool>() && spells[Spells.E].IsReady() && spells[Spells.E].IsInRange(target))
            {
                spells[Spells.E].Cast(target.ServerPosition);
            }
        }

        private static void OnEnemyGapClose(ActiveGapcloser gapcloser)
        {
            if (gapcloser.Sender.IsAlly)
            {
                return;
            }

            if (MadlifeThreshMenu._menu.Item("Thresh.Gapclose.E").GetValue<bool>() && spells[Spells.E].IsReady() && spells[Spells.E].IsInRange(gapcloser.Start))
            {
                spells[Spells.E].Cast(Player.Position.Extend(gapcloser.Sender.Position, 250));
            }
            if (MadlifeThreshMenu._menu.Item("Thresh.Gapclose.R").GetValue<bool>() && spells[Spells.R].IsReady() && spells[Spells.R].IsInRange(gapcloser.Start))
            {
                spells[Spells.R].Cast();
            }
        }

        //private static void MadlifeHook()
        //{
        //    throw new NotImplementedException();
        //}

        private static void ThrowLantern()
        {
            if (spells[Spells.W].IsReady() && MadlifeThreshMenu._menu.Item("Thresh.Combo.W").GetValue<bool>())
            {
                var NearAllies = Player.GetAlliesInRange(spells[Spells.W].Range) //W.Range instead of 1200, also there is no "On most damaged"
                                .Where(x => !x.IsMe)
                                .Where(x => !x.IsDead)
                                .Where(x => x.Distance(Player.Position) <= spells[Spells.W].Range + 250)
                                .FirstOrDefault();

                if (NearAllies == null) return;

                spells[Spells.W].Cast(NearAllies.Position);


            }

        }

        private static void Combo()
        {
            //Q Logic #1
            //if (spells[Spells.Q].IsReady() && MadlifeThreshMenu._menu.Item("Thresh.Combo.Q").GetValue<bool>())
            //{
            //    var t = TargetSelector.GetTarget(spells[Spells.Q].Range, TargetSelector.DamageType.Magical);
            //    spells[Spells.Q].CastIfHitchanceEquals(t, HitChance.High);

            //    if (MadlifeThreshMenu._menu.Item("Thresh.Combo.Q2").GetValue<bool>())
            //    {
            //        Utility.DelayAction.Add(250 - Game.Ping / 2 + 10, () => spells[Spells.Q2].Cast());
            //    }
            //}
            var T = TargetSelector.GetTarget(spells[Spells.Q].Range, TargetSelector.DamageType.Magical);

            if (spells[Spells.Q].IsReady() && MadlifeThreshMenu._menu.Item("Thresh.Combo.Q").GetValue<bool>())
            {

                spells[Spells.Q].CastIfHitchanceEquals(T, HitChance.High);
                if (T.HasBuff(ThreshQBuff))
                {
                    FirstQTime = System.Environment.TickCount;
                }
            }

            if (spells[Spells.Q2].IsReady() && MadlifeThreshMenu._menu.Item("Thresh.Combo.Q2").GetValue<bool>() &&
                System.Environment.TickCount - FirstQTime > 3000f && T.HasBuff(ThreshQBuff))
            {
                spells[Spells.Q2].Cast();
            }

            if (spells[Spells.W].IsReady() && MadlifeThreshMenu._menu.Item("Thresh.Combo.W").GetValue<bool>())
            {
                ThrowLantern();
            }

            if (spells[Spells.E].IsReady() && MadlifeThreshMenu._menu.Item("Thresh.Combo.E").GetValue<bool>() && Vector3.Distance(T.Position, Player.Position) < spells[Spells.E].Range)
            {
                if (MadlifeThreshMenu._menu.Item("Thresh.E").GetValue<bool>())
                {
                    spells[Spells.E].Cast(T.Position.Extend(Player.Position, Vector3.Distance(T.Position, Player.Position) + 400));
                }
                else
                {
                    spells[Spells.E].Cast(T.Position);
                }
            }

        }

        private static void Harass()
        {
            return;
        }

        private static void Clear()
        {
            return;
        }

        private static void LastHit()
        {
            return;
        }
    }
}
