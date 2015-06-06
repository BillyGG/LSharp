using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.Common.Data;
using SharpDX;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace LSharpAlistar
{
    internal enum Spells
    {
        Q, W, E, R
    }

    internal class Alistar
    {
        private const string ChampName = "Alistar";

        public static Orbwalking.Orbwalker Orbwalker;
        public static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        public static Obj_AI_Base Minionerimo;

        private static Menu _menu;
        private static Spell _q;
        private static Spell _w;
        private static Spell _e;
        private static Spell _r;

        public static Dictionary<Spells, Spell> spells = new Dictionary<Spells, Spell>()
        {
            { Spells.Q, new Spell(SpellSlot.Q, 365) },
            { Spells.W, new Spell(SpellSlot.W, 650) },
            { Spells.E, new Spell(SpellSlot.E, 575) },
            { Spells.R, new Spell(SpellSlot.R, 0) }
        };

        public static void Game_OnGameLoad(EventArgs args)
        {
            if (ObjectManager.Player.BaseSkinName != ChampName)
                return;

            Notifications.AddNotification("LSharp - Alistar Loaded By BillyGG", 5000);

            AlistarMenu.Initialize();
            Game.OnUpdate += OnGameUpdate;
            Drawing.OnDraw += Drawings.Drawing_OnDraw;
            Drawing.OnEndScene += Drawings.OnDrawEndScene;
        }

        private static void OnGameUpdate(EventArgs args)
        {
            switch (Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harass();
                    break;
                default:
                    //add menu item with auto heal slider
                    break;
            }
        }

        private static void Harass()
        {
            throw new NotImplementedException();
        }

        private static void Combo()
        {
            var target = TargetSelector.GetTarget(spells[Spells.W].Range, TargetSelector.DamageType.Magical);
            var qCombo = AlistarMenu._menu.Item("Alistar.Combo.Q").GetValue<bool>();
            var wCombo = AlistarMenu._menu.Item("Alistar.Combo.W").GetValue<bool>();
            var eCombo = AlistarMenu._menu.Item("Alistar.Combo.E").GetValue<bool>();
            var eHealth = AlistarMenu._menu.Item("Alistar.Combo.EHealth").GetValue<Slider>().Value;
            var rCombo = AlistarMenu._menu.Item("Alistar.Combo.R").GetValue<bool>();
            var rHealth = AlistarMenu._menu.Item("Alistar.Combo.RHealth").GetValue<Slider>().Value;

            SpellDataInst qmana = Player.Spellbook.GetSpell(SpellSlot.Q);
            SpellDataInst wmana = Player.Spellbook.GetSpell(SpellSlot.W);


            if (target == null || !target.IsValid)
            {
                return;
            }

            if (qCombo && wCombo && spells[Spells.Q].IsReady() && spells[Spells.W].IsReady() && qmana.ManaCost + wmana.ManaCost <= Player.Mana)
            {
                spells[Spells.W].Cast(target);
                var comboTime = Math.Max(0, Player.Distance(target) - 500) * 10 / 25 + 25;

                Utility.DelayAction.Add((int)comboTime, () => spells[Spells.Q].Cast());
                Utility.DelayAction.Add(1000, () => spells[Spells.E].Cast(Player));
            }
            if (qCombo && !spells[Spells.W].IsReady() && Player.Distance(target) <= spells[Spells.Q].Range && spells[Spells.Q].IsReady())
            {
                spells[Spells.Q].Cast();
            }
            if (eCombo && Player.Health <= rHealth && spells[Spells.E].IsReady())
            {
                spells[Spells.E].Cast(Player);
            }
            if (rCombo && Player.Health <= rHealth && spells[Spells.R].IsReady())
            {
                spells[Spells.R].Cast(Player);
            }
        }

        private static void LaneClear()
        {

        }
    }
}
