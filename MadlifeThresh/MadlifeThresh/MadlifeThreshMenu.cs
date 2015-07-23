using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace MadlifeThresh
{
    public class MadlifeThreshMenu
    {

        public static Menu _menu;

        public static void Initialize()
        {
            _menu = new Menu("Madlife Thresh", "menu", true);

            //Orbwalker
            var orbwalkerMenu = new Menu("Orbwalker", "orbwalker");
            MadlifeThresh.Orbwalker = new Orbwalking.Orbwalker(orbwalkerMenu);
            _menu.AddSubMenu(orbwalkerMenu);

            //TargetSelector
            var targetSelector = new Menu("Target Selector", "TargetSelector");
            TargetSelector.AddToMenu(targetSelector);
            _menu.AddSubMenu(targetSelector);

            //Combo
            var comboMenu = _menu.AddSubMenu(new Menu("Combo", "Combo"));
            comboMenu.AddItem(new MenuItem("Thresh.Combo.Q", "Use Q").SetValue(true));
            comboMenu.AddItem(new MenuItem("Thresh.Combo.Q2", "Use Q2").SetValue(true));
            comboMenu.AddItem(new MenuItem("Thresh.Combo.W", "Use W to Ally on Engage").SetValue(true));
            comboMenu.AddItem(new MenuItem("Thresh.Combo.E", "Use E").SetValue(true));
            comboMenu.AddItem(new MenuItem("Thresh.Combo.R", "Use R").SetValue(true));
            comboMenu.AddItem(new MenuItem("Thresh.Combo.R.Config", "Use R only when enemies >= x").SetValue(new Slider(2, 1, 5)));
            comboMenu.AddItem(new MenuItem("Thresh.Combo.separator", ""));
            comboMenu.AddItem(new MenuItem("Thresh.Combo.Ignite", "Use Ignite").SetValue(true));

            //Madlife Hook Menu
            var mlhMenu = _menu.AddSubMenu(new Menu("MLH", "Madlife Hook Config"));
            mlhMenu.AddItem(new MenuItem("Thresh.mlh.active", "Press this key to throw a Madlife hook").SetValue(new KeyBind("G".ToCharArray()[0], KeyBindType.Press)));
            mlhMenu.AddItem(new MenuItem("Thresh.mlh.flash", "Throw at Flash Range").SetValue(true));
            mlhMenu.AddItem(new MenuItem("Thresh.mlh.dashes", "Throw at Dash Range (Ezreal, Graves, Lucian)").SetValue(false));

            //Clear

            //Harass
            var harassMenu = _menu.AddSubMenu(new Menu("Harass", "Harass"));
            harassMenu.AddItem(new MenuItem("Thresh.Harass.Q", "Use Q").SetValue(true));
            harassMenu.AddItem(new MenuItem("Thresh.Harass.Q2", "Use Q2").SetValue(true));
            harassMenu.AddItem(new MenuItem("Thresh.Harass.E", "Use E").SetValue(true));

            //E Settings
            var eMenu = _menu.AddSubMenu(new Menu("E Menu", "EMenu"));
            eMenu.AddItem(new MenuItem("Thresh.E", "Pull ON/Push OFF").SetValue(true));

            //Interrupt
            var interruptMenu = _menu.AddSubMenu(new Menu("Interrupt", "Interrupt"));
            interruptMenu.AddItem(new MenuItem("Thresh.Interrupt.Q", "Interrupt with Q").SetValue(true));
            interruptMenu.AddItem(new MenuItem("Thresh.Interrupt.E", "Interrupt with E").SetValue(true));

            //Gapclose
            var gapcloseMenu = _menu.AddSubMenu(new Menu("GapClose", "GapClose"));
            gapcloseMenu.AddItem(new MenuItem("Thresh.GapClose.Q", "Q on Gapclose").SetValue(true));
            gapcloseMenu.AddItem(new MenuItem("Thresh.GapClose.E", "E on Gapclose").SetValue(true));
            gapcloseMenu.AddItem(new MenuItem("Thresh.GapClose.R", "R on Gapclose").SetValue(false));

            //Ult
            var ultMenu = _menu.AddSubMenu(new Menu("Ult Settings", "UltSettings"));
            ultMenu.AddItem(new MenuItem("Thresh.UltSettings.AutoUlt", "Auto Ult when enemies > X").SetValue(new Slider(3, 1, 6)));
            ultMenu.AddItem(new MenuItem("Thresh.UltSettings.AutoUltCont", "Change to 6 to disable AutoUlt"));

            //Drawings
            var drawMenu = _menu.AddSubMenu(new Menu("Drawings", "Drawings"));
            drawMenu.AddItem(new MenuItem("Thresh.Draw.enable", "Enable Drawings").SetValue(true));
            drawMenu.AddItem(new MenuItem("Thresh.Draw.q", "Draw Q").SetValue(new Circle()));
            drawMenu.AddItem(new MenuItem("Thresh.Draw.w", "Draw W").SetValue(new Circle()));
            drawMenu.AddItem(new MenuItem("Thresh.Draw.e", "Draw E").SetValue(new Circle()));
            drawMenu.AddItem(new MenuItem("Thresh.Draw.r", "Draw R").SetValue(new Circle()));

            _menu.AddToMainMenu();

            Console.WriteLine("Menu Loaded");

        }
    }
}
