using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;


namespace LSharpVayne
{
    public class Drawings
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            if (Vayne.Player.IsDead)
                return;

            var drawOff = VayneMenu._menu.Item("Vayne.Draw.off").GetValue<bool>();
            var drawQ = VayneMenu._menu.Item("Vayne.Draw.q").GetValue<Circle>();
            var drawW = VayneMenu._menu.Item("Vayne.Draw.W").GetValue<Circle>();
            var drawE = VayneMenu._menu.Item("Vayne.Draw.E").GetValue<Circle>();
            var drawR = VayneMenu._menu.Item("Vayne.Draw.R").GetValue<Circle>();

            if (drawOff)
                return;

            if (drawQ.Active)
                if (Vayne.spells[Spells.Q].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Vayne.spells[Spells.Q].Range, Color.White);

            if (drawW.Active)
                if (Vayne.spells[Spells.W].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Vayne.spells[Spells.W].Range, Color.White);

            if (drawE.Active)
                if (Vayne.spells[Spells.E].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Vayne.spells[Spells.E].Range, Color.White);

            if (drawR.Active)
                if (Vayne.spells[Spells.R].Level > 0)
                    Render.Circle.DrawCircle(ObjectManager.Player.Position, Vayne.spells[Spells.R].Range, Color.White);
        }

        public static void OnDrawEndScene(EventArgs args)
        {
            if (Vayne.Player.IsDead)
                return;
        }
    }
}