using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK.Core.Enumerations;
using LeagueSharp.SDK.Core.UI.IMenu;
using LeagueSharp.SDK.Core.UI.IMenu.Values;
using Keys = System.Windows.Forms.Keys;

namespace Zed
{
    class Config
    {

        public static Menu Settings = new Menu("Zed", "Zed", true);

        public static void Initialize()
        {
            //Combo
            {
                var combo = new Menu("Combo", "Combo");
                combo.Add(new MenuBool("CQ", "Use Q", true));
                combo.Add(new MenuBool("CW", "Use W", true));
                combo.Add(new MenuBool("CE", "Use E", true));
                combo.Add(new MenuBool("CR", "Use R", true));
                combo.Add(new MenuSeparator("ss2", "W Settings"));
                combo.Add(new MenuBool("WgapClose", "Use W to Gap Close", true));
                Settings.Add(combo);
            }

            //Harass
            {
                var harass = new Menu("Harass", "Harass");
                harass.Add(new MenuBool("HQ", "Use Q", true));
                harass.Add(new MenuBool("HE", "Use E", true));
                harass.Add(new MenuKeyBind("useLong", "Use Long Harass", Keys.U, KeyBindType.Toggle));
                Settings.Add(harass);
            }

            //Farm
            {
                var farm = new Menu("Farm", "Farm");
                farm.Add(new MenuSeparator("ss", "Lane Clear"));
                farm.Add(new MenuBool("FQ", "Use Q", true));
                farm.Add(new MenuBool("FE", "Use E", true));
                farm.Add(new MenuSlider("eHit", "If E hit >= x", 3, 1, 6));
                
                farm.Add(new MenuSeparator("ss2", "Jungle Clear"));
                farm.Add(new MenuBool("JQ", "Use Q", true));
                farm.Add(new MenuBool("JE", "Use E", true));
                Settings.Add(farm);
            }

            //Items
            {
                var items = new Menu("Items", "Items");
                items.Add(new MenuBool("useBotrk", "Use Blade of the Ruined King", true));
                items.Add(new MenuSlider("botrkMyHP", "If my HP <= x (101 to always)", 70, 1, 101));
                items.Add(new MenuSlider("botrkEnemies", "If Enemies in Range >= x (6 to always)", 2, 1, 6));
                items.Add(new MenuBool("useYoumuus", "Use Youmuu's Ghost Blade", true));
                Settings.Add(items);
            }

            //Drawing
            {
                var drawings = new Menu("Drawings", "Drawings");
                drawings.Add(new MenuBool("drawQ", "Draw Q", true));
                drawings.Add(new MenuBool("drawW", "Draw W", true));
                drawings.Add(new MenuBool("drawE", "Draw E", true));
                drawings.Add(new MenuBool("drawR", "Draw R", true));
                Settings.Add(drawings);
            }

            {
                Settings.Attach();
            }
        }

        public static bool CQ { get { return Settings["combo"]["CQ"].GetValue<MenuBool>().Value; } }
        public static bool CW { get { return Settings["combo"]["CW"].GetValue<MenuBool>().Value; } }
        public static bool CE { get { return Settings["combo"]["CE"].GetValue<MenuBool>().Value; } }
        public static bool CR { get { return Settings["combo"]["CR"].GetValue<MenuBool>().Value; } }
        public static bool WgapClose { get { return Settings["combo"]["WgapClose"].GetValue<MenuBool>().Value; } }


        public static bool HQ { get { return Settings["harass"]["HQ"].GetValue<MenuBool>().Value; } }
        public static bool HE { get { return Settings["harass"]["HE"].GetValue<MenuBool>().Value; } }
        public static MenuKeyBind useLong { get { return Settings["harass"]["useLong"].GetValue<MenuKeyBind>(); } }


        public static bool FQ { get { return Settings["farm"]["FQ"].GetValue<MenuBool>().Value; } }
        public static bool FE { get { return Settings["farm"]["FE"].GetValue<MenuBool>().Value; } }
        public static int eHit { get { return Settings["farm"]["eHit"].GetValue<MenuSlider>().Value; } }

        public static bool JQ { get { return Settings["farm"]["JQ"].GetValue<MenuBool>().Value; } }
        public static bool JE { get { return Settings["farm"]["JE"].GetValue<MenuBool>().Value; } }


        public static bool useBotrk { get { return Settings["items"]["useBotrk"].GetValue<MenuBool>().Value; } }
        public static int botrkMyHp { get { return Settings["items"]["botrkMyHp"].GetValue<MenuSlider>().Value; } }
        public static int botrkEnemies { get { return Settings["items"]["botrkEnemies"].GetValue<MenuSlider>().Value; } }
        public static bool useYoumuus { get { return Settings["items"]["useYoumuus"].GetValue<MenuBool>().Value; } }


        public static bool drawQ { get { return Settings["drawings"]["drawQ"].GetValue<MenuBool>().Value; } }
        public static bool drawW { get { return Settings["drawings"]["drawW"].GetValue<MenuBool>().Value; } }
        public static bool drawE { get { return Settings["drawings"]["drawE"].GetValue<MenuBool>().Value; } }
        public static bool drawR { get { return Settings["drawings"]["drawR"].GetValue<MenuBool>().Value; } }
    }
}
