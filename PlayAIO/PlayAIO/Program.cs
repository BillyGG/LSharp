using System;
using PlayAIO.Champions;
using LeagueSharp;
using LeagueSharp.SDK;

namespace PlayAIO
{
    class Program
    {
        internal static Obj_AI_Hero Player;

        static void Main(string[] args)
        {
            Bootstrap.Init();

            Events.OnLoad += (sender, eventArgs) =>
            {
                switch (ObjectManager.Player.ChampionName)
                {
                    case "LeeSin":
                        new LeeSin();
                        break;
                    default:
                        Console.WriteLine("Champion not Supported");
                        Variables.Orbwalker.Enabled = false;
                        break;
                }
            };
        }
    }
}
