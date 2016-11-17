using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;
using System.Collections.Generic;

namespace ExecuteSharp
{
    public class Program
    {
        internal Menu _menu;
        internal float drawingX;
        internal float drawingY;
        internal string executeText = "Execute time: ";
        internal int executeTime = 0;

        private Obj_AI_Hero Player => ObjectManager.Player;
        private Dictionary<int, Obj_AI_Base> lastDamage = new Dictionary<int, Obj_AI_Hero>();

        public void Main(String[] args)
        {
            Initialize();
        }

        public void Initialize()
        {
            _menu = new Menu("Execute#", "menu", true);

            _menu.AddItem(new MenuItem("enabled", "Enabled").SetValue(true));
            _menu.AddItem(new MenuItem("drawx", "Pos X").SetValue(new Slider(800, 1000)));
            _menu.AddItem(new MenuItem("drawy", "Pos Y").SetValue(new Slider(500, 1000)));

            _menu.AddToMainMenu();

            Console.WriteLine("Menu Loaded");

            Drawing.OnDraw += Drawing_OnDraw;
            AttackableUnit.OnDamage += Game_OnDamage;
        }

        private void Game_OnDamage(AttackableUnit unit, AttackableUnitDamageEventArgs args)
        {
            if (unit.IsMe)
            {
                Obj_AI_Base source;

                if (lastDamage.TryGetValue(args.SourceNetworkId, out source))
                {

                }
            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            drawingX = (_menu.Item("drawx").GetValue<Slider>().Value * 0.001f) * Drawing.Width;
            drawingY = (_menu.Item("drawy").GetValue<Slider>().Value * 0.001f) * Drawing.Height;

            var textPos = new Vector2(drawingX, drawingY);
            var wts = Drawing.WorldToScreen(Player.Position);

            var exeTime = 13; //13 Seconds
            var start = DateTime.UtcNow;
            var endTime = start.AddSeconds(exeTime);

            Drawing.DrawText(wts.X - 70, wts.Y + 20, Color.Red, executeText + executeTime);
        }
    }
}