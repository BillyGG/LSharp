using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace MadlifeThresh
{
    public class Drawings
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            if (MadlifeThresh.Player.IsDead)
                return;

            var drawOff = MadlifeThreshMenu._menu.Item("Thresh.Draw.enable").GetValue<bool>();
            var drawQ = MadlifeThreshMenu._menu.Item("Thresh.Draw.q").GetValue<Circle>();
            var drawW = MadlifeThreshMenu._menu.Item("Thresh.Draw.W").GetValue<Circle>();
            var drawE = MadlifeThreshMenu._menu.Item("Thresh.Draw.E").GetValue<Circle>();
            var drawR = MadlifeThreshMenu._menu.Item("Thresh.Draw.R").GetValue<Circle>();

            if (drawOff)
                return;

            if (drawQ.Active)
                if (MadlifeThresh.spells[Spells.Q].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, MadlifeThresh.spells[Spells.Q].Range, Color.White);

            if (drawW.Active)
                if (MadlifeThresh.spells[Spells.W].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, MadlifeThresh.spells[Spells.W].Range, Color.White);

            if (drawE.Active)
                if (MadlifeThresh.spells[Spells.E].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, MadlifeThresh.spells[Spells.E].Range, Color.White);

            if (drawR.Active)
                if (MadlifeThresh.spells[Spells.R].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, MadlifeThresh.spells[Spells.R].Range, Color.White);
        }

        public static void OnDrawEndScene(EventArgs args)
        {
            if (MadlifeThresh.Player.IsDead)
                return;
        }
    }
}
