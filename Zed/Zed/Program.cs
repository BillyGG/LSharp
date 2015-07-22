using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using SharpDX;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Core.Events;
using LeagueSharp.SDK.Core;
using LeagueSharp.SDK.Core.UI.IMenu;
using LeagueSharp.SDK.Core.UI.IMenu.Values;
using LeagueSharp.SDK.Core.Enumerations;
using LeagueSharp.SDK.Core.Utils;
using System.Windows.Forms;
using Menu = LeagueSharp.SDK.Core.UI.IMenu.Menu;
using LeagueSharp.SDK.Core.Wrappers;
using LeagueSharp.SDK.Core.Extensions;
using LeagueSharp.SDK.Core.Extensions.SharpDX;
using System.Drawing;
using Color = System.Drawing.Color;
using LeagueSharp.SDK.Core.IDrawing;
using System.Diagnostics.CodeAnalysis;

namespace Zed
{

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]

    class Program
    {
        static Obj_AI_Hero Player = ObjectManager.Player;

        //Spells
        public static Spell Q, W, E, R;


        static void Main(string[] args)
        {
            Load.OnLoad += Load_OnLoad;
        }

        static void Load_OnLoad(object sender, EventArgs e)
        {
            if (Player.ChampionName != "Zed")
                return;

            Bootstrap.Init(null);

            Game.PrintChat("<font color=\"#7CFC00\"><b>Zed:</b></font> Loaded");

            Game.OnUpdate += Game_OnUpdate;

            Config.Initialize();
            SpellManager.Initialize();
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {

            var drawQ = Config.drawQ;

            if (drawQ)
            {
                Render.Circle.DrawCircle(
                    ObjectManager.Player.Position,
                    this.E.Range,
                    this.E.IsReady() ? Color.Aqua : Color.Red);
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.IsDead || Player.IsRecalling() || args == null)
                return;
            
            switch(Orbwalker.ActiveMode)
            {
                case OrbwalkerMode.Orbwalk:
                    Combo();
                    break;
                case OrbwalkerMode.Hybrid:
                    Harass();
                    break;
                case OrbwalkerMode.LastHit:
                    LastHit();
                    break;
                case OrbwalkerMode.LaneClear:
                    LaneClear();
                    break;
            }
        }

        private static void Combo()
        {
            if (SpellManager.Q.IsReady() && Config.CQ)
            {
                var t = TargetSelector.GetTarget(SpellManager.Q.Range, DamageType.Physical);
                if (t != null)
                    SpellManager.Q.CastIfHitchanceMinimum(t, HitChance.High);
            }
        }

        private static void Harass()
        {
            throw new NotImplementedException();
        }

        private static void LaneClear()
        {
            throw new NotImplementedException();
        }

        private static void LastHit()
        {
            throw new NotImplementedException();
        }
    }
}
