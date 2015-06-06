using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using System.Drawing;

namespace LSharpAlistar
{
    public class AlistarMenu
    {
        public static Menu _menu;

        public static void Initialize()
        {
            _menu = new Menu("LSharp - Alistar", "menu", true);

            var orbwalkerMenu = new Menu("Orbwalker", "orbwalker");
            Alistar.Orbwalker = new Orbwalking.Orbwalker(orbwalkerMenu);
            _menu.AddSubMenu(orbwalkerMenu);

            var targetSelector = new Menu("Target Selector", "TargetSelector");
            TargetSelector.AddToMenu(targetSelector);
            _menu.AddSubMenu(targetSelector);

            var comboMenu = _menu.AddSubMenu(new LeagueSharp.Common.Menu("Combo", "Combo"));
            comboMenu.AddItem(new MenuItem("Alistar.Combo.Q", "Use Q").SetValue(true));
            comboMenu.AddItem(new MenuItem("Alistar.Combo.W", "Use W").SetValue(true));
            comboMenu.AddItem(new MenuItem("Alistar.Combo.E", "Use E").SetValue(true));
            comboMenu.AddItem(new MenuItem("Alistar.Combo.EHealth", "Use E at Health").SetValue(new Slider(30, 0, 100)));
            comboMenu.AddItem(new MenuItem("Alistar.Combo.R", "Use R").SetValue(true));
            comboMenu.AddItem(new MenuItem("Alistar.Combo.RHealth", "Use R at Health").SetValue(new Slider(30, 0, 100)));
            comboMenu.AddItem(new MenuItem("Alistar.Combo.separator", ""));
            comboMenu.AddItem(new MenuItem("ComboActive", "Combo!").SetValue(new KeyBind(32, KeyBindType.Press)));

            var clearMenu = _menu.AddSubMenu(new Menu("Jungle and laneclear", "JLC"));
            clearMenu.AddItem(new MenuItem("Alistar.Clear.Q", "Use Q").SetValue(true));
            clearMenu.AddItem(new MenuItem("Alistar.Clear.QMinions", "Use Q on X Minions").SetValue(true));
            clearMenu.AddItem(new MenuItem("Alistar.Clear.E", "Use E").SetValue(true));

            var healMenu = _menu.AddSubMenu(new Menu("Heal", "SH"));
            healMenu.AddItem(new MenuItem("Alistar.Heal.AutoHeal", "Auto heal").SetValue(true));
            healMenu.AddItem(new MenuItem("Alistar.Heal.HPSelf", "Self heal at >= ").SetValue(new Slider(25, 1, 100)));
            healMenu.AddItem(new MenuItem("Alistar.Heal.HPAlly", "Ally heal at >= ").SetValue(new Slider(25, 1, 100)));

            var miscMenu = _menu.AddSubMenu(new Menu("Drawings", "Misc"));
            miscMenu.AddItem(new MenuItem("Alistar.Draw.off", "[Drawing] Drawings off").SetValue(false));
            miscMenu.AddItem(new MenuItem("Alistar.Draw.q", "Draw Q").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("Alistar.Draw.W", "Draw W").SetValue(new Circle()));
            miscMenu.AddItem(new MenuItem("Alistar.Draw.E", "Draw E").SetValue(new Circle()));
        }
    }
}