using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.Common.Data;
using SharpDX;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace LSharpVayne
{
    internal enum Spells
    {
        Q, W, E, R
    }

    internal class Vayne
    {
        private const string ChampName = "Vayne";

        public static Orbwalking.Orbwalker Orbwalker;
        public static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        public static Obj_AI_Base Minionerimo;

        private static Menu _menu;
        private static Spell _q;
        private static Spell _w;
        private static Spell _e;
        private static Spell _r;

        public static Dictionary<Spells, Spell> spells = new Dictionary<Spells, Spell>()
        {
            { Spells.Q, new Spell(SpellSlot.Q, 300f) },
            { Spells.W, new Spell(SpellSlot.W) },
            { Spells.E, new Spell(SpellSlot.E, 590f) },
            { Spells.R, new Spell(SpellSlot.R) }
        };

        public static void Game_OnGameLoad(EventArgs args)
        {
            if (ObjectManager.Player.BaseSkinName != ChampName)
                return;

            Notifications.AddNotification("LSharp - Vayne Loaded By BillyGG", 5000);

            VayneMenu.Initialize();
            Game.OnUpdate += OnGameUpdate;
            Drawing.OnDraw += Drawings.Drawing_OnDraw;
            Drawing.OnEndScene += Drawings.OnDrawEndScene;
        }

        private static void OnGameUpdate(EventArgs args)
        {

            if (ObjectManager.Player.IsDead)
            {
                return;
            }

            switch (Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harass();
                    break;
                case Orbwalking.OrbwalkingMode.LaneClear:
                    LaneClear();
                    break;
                case Orbwalking.OrbwalkingMode.LastHit:
                    LastHit();
                    break;
                default:
                    break;
            }
        }

        //static void Obj_AI_Hero_OnPlayAnimation(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args)
        //{
        //    return;
        //    if (!sender.IsMe)
        //    {
        //        return;
        //    }
        //    if (args.Animation.Contains("Attack"))
        //    {
        //        LeagueSharp.Common.Utility.DelayAction.Add((25), () =>
        //        {
        //            if (ObjectManager.Player.IsAttackingPlayer)
        //            {
        //                LeagueSharp.Common.Utility.DelayAction.Add((int)(ObjectManager.Player.AttackCastDelay * 1000 + 15), () => OrbwalkingAfterAttack(ObjectManager.Player, Orbwalker.GetTarget()));
        //            }
        //        });
        //    }
        //}

        //private static void OrbwalkingAfterAttack(Obj_AI_Hero player, AttackableUnit attackableUnit)
        //{
        //}

        private static void LastHit()
        {
            throw new NotImplementedException();
        }

        private static void LaneClear()
        {
            throw new NotImplementedException();
        }

        private static void Harass()
        {
            throw new NotImplementedException();
        }

        private static bool CanCondemnStun(Obj_AI_Base target, Vector3 startPos = default(Vector3), bool casting = true)
        {
            if (startPos == default(Vector3))
            {
                startPos = Player.ServerPosition;
            }

            var knockbackPos = startPos.Extend(
                target.ServerPosition,
                startPos.Distance(target.ServerPosition) + 400);

            var flags = NavMesh.GetCollisionFlags(knockbackPos);
            var collision = flags.HasFlag(CollisionFlags.Building) || flags.HasFlag(CollisionFlags.Wall);

            if (!casting || !NavMesh.IsWallOfGrass(knockbackPos, 200))
            {
                return collision;
            }

            var wardItem = Items.GetWardSlot();

            if (1 > 0)
            {
                return collision;
            }

            if (wardItem != default(InventorySlot))
            {
                Player.Spellbook.CastSpell(wardItem.SpellSlot, knockbackPos);
            }
            else if (Items.CanUseItem(ItemData.Scrying_Orb_Trinket.Id))
            {
                Items.UseItem(ItemData.Scrying_Orb_Trinket.Id, knockbackPos);
            }
            else if (Items.CanUseItem(ItemData.Farsight_Orb_Trinket.Id))
            {
                Items.UseItem(ItemData.Farsight_Orb_Trinket.Id, knockbackPos);
            }

            return collision;
        }

        private static void Combo()
        {
            var target = TargetSelector.GetTarget(Orbwalking.GetRealAutoAttackRange(Player) + 300, TargetSelector.DamageType.Physical);

            if (!target.IsValidTarget())
            {
                return;
            }

            if (spells[Spells.Q].IsReady() && !Orbwalking.CanAttack() && Player.Distance(target) > Orbwalking.GetRealAutoAttackRange(Player))
            {
                spells[Spells.Q].Cast(target.Position);
            }
             
        }
    }
}