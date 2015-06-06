using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;


namespace LSharpAlistar
{
    public class Drawings
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            if (Alistar.Player.IsDead)
                return;

            var drawOff = AlistarMenu._menu.Item("Alistar.Draw.off").GetValue<bool>();
            var drawQ = AlistarMenu._menu.Item("Alistar.Draw.q").GetValue<Circle>();
            var drawW = AlistarMenu._menu.Item("Alistar.Draw.W").GetValue<Circle>();
            var drawE = AlistarMenu._menu.Item("Alistar.Draw.E").GetValue<Circle>();
            var drawR = AlistarMenu._menu.Item("Alistar.Draw.R").GetValue<Circle>();

            if (drawOff)
                return;

            if (drawQ.Active)
                if (Alistar.spells[Spells.Q].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Alistar.spells[Spells.Q].Range, Color.White);

            if (drawW.Active)
                if (Alistar.spells[Spells.W].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Alistar.spells[Spells.W].Range, Color.White);

            if (drawE.Active)
                if (Alistar.spells[Spells.E].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Alistar.spells[Spells.E].Range, Color.White);

            if (drawR.Active)
                if (Alistar.spells[Spells.R].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Alistar.spells[Spells.R].Range, Color.White);
        }

        public static void OnDrawEndScene(EventArgs args)
        {
            if (Alistar.Player.IsDead)
                return;
        }
    }
}