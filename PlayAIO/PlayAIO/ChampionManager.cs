using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using PlayAIO.Champions;
using Menu = LeagueSharp.SDK.UI.Menu;
using Color = System.Drawing.Color;
using SharpDX;

namespace PlayAIO
{
    public class ChampionManager
    {
        public Menu PlayUtility;
        public Menu MainMenu { get; set; }
        public Menu DrawingMenu { get; set; }
        public Menu ChampionMenu { get; set; }
        public Menu SpellsMenu { get; set; }
        public MenuBool DrawQ;
        public MenuColor DrawQc;
        public MenuBool DrawW;
        public MenuColor DrawWc;
        public MenuBool DrawE;
        public MenuColor DrawEc;
        public MenuBool DrawR;
        public MenuColor DrawRc;

        public static TargetSelector TargetSelector { get; } = Variables.TargetSelector;
        public static Orbwalker Orbwalker { get; } = Variables.Orbwalker;

        public IEnumerable<Obj_AI_Hero> ValidTargets { get { return GameObjects.EnemyHeroes.Where(enemy => enemy.IsHPBarRendered); } }

        internal static Obj_AI_Hero Player;
        
        public virtual void OnProcessSpellCast(GameObject sender, GameObjectProcessSpellCastEventArgs args) { }
        public virtual void OnDraw(EventArgs args) { }
        public virtual void InitializeMenu() { }

        #region Spells and Items
        public static Spell Q { get; set; }
        public static Spell Q2 { get; set; }
        public static Spell W { get; set; }
        public static Spell W2 { get; set; }
        public static Spell E { get; set; }
        public static Spell E2 { get; set; }
        public static Spell R { get; set; }
        public static Spell R2 { get; set; }

        internal const int FlashRange = 425, IgniteRange = 600, SmiteRange = 570;

        internal static SpellSlot Flash, Ignite, Smite;

        internal static Items.Item Bilgewater, BotRuinedKing, Youmuu, Tiamat, Hydra, Titanic;
        #endregion Spells

        public ChampionManager()
        {
            MainMenu = new Menu("playaio", "PlayAIO - " + ObjectManager.Player.ChampionName, true, ObjectManager.Player.ChampionName);
            MainMenu.Add(new MenuSeparator("mainsep", "Main"));
            PlayUtility = MainMenu.Add(new Menu("aynutility", "AYN Utilites: "));
            Utilities.Prediction.PredictionMode = PlayUtility.Add(new MenuList<string>("prediction", "Prediction Type: ", new[] { "SDK", "Common" }));

            DrawingMenu = MainMenu.Add(new Menu("drawingmenu", "Drawings: "));
            DrawQ = DrawingMenu.Add(new MenuBool("drawq", "Draw Q: ", false));
            DrawW = DrawingMenu.Add(new MenuBool("draww", "Draw W: ", false));
            DrawE = DrawingMenu.Add(new MenuBool("drawe", "Draw E: ", false));
            DrawR = DrawingMenu.Add(new MenuBool("drawr", "Draw R: ", false));

            MainMenu.Add(new MenuSeparator("champsep", "Champion"));
            ChampionMenu = MainMenu.Add(new Menu("championmenu", ObjectManager.Player.ChampionName + " Settings: "));

            MainMenu.Add(new MenuSeparator("spellsep", "Spells"));
            SpellsMenu = MainMenu.Add(new Menu("spellmenu", "Spells Settings: "));

            DelayAction.Add(15000, () => Orbwalker.Enabled = true);

            Drawing.OnDraw += args =>
            {
                if (ObjectManager.Player.IsDead)
                {
                    return;
                }
                if (DrawQ && Q.IsReady())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, Q.Range, Color.White);
                }
                if (DrawW && W.IsReady())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, W.Range, Color.White);
                }
                if (DrawE && E.IsReady())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, E.Range, Color.White);
                }
                if (DrawR && R.IsReady())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, R.Range, Color.White);
                }
            };
        }
    }
}
