﻿using System;
using Activator.Base;
using LeagueSharp.Common;

namespace Activator.Items.Consumables
{
    class _2031 : CoreItem
    {
        internal override int Id
        {
            get { return 2031; }
        }

        internal override int Priority
        {
            get { return 3; }
        }

        internal override string Name
        {
            get { return "Refillable Pot"; }
        }

        internal override string DisplayName
        {
            get { return "Refillable Pot"; }
        }

        internal override int Duration
        {
            get { return 101; }
        }

        internal override float Range
        {
            get { return float.MaxValue; }
        }

        internal override MenuType[] Category
        {
            get { return new[] { MenuType.SelfLowHP, MenuType.SelfMuchHP }; }
        }

        internal override MapType[] Maps
        {
            get { return new[] { MapType.Common }; }
        }

        internal override int DefaultHP
        {
            get { return 55; }
        }

        internal override int DefaultMP
        {
            get { return 0; }
        }

        public override void OnTick(EventArgs args)
        {
            if (!Menu.Item("use" + Name).GetValue<bool>() || !IsReady())
                return;

            foreach (var hero in Activator.Allies())
            {
                if (hero.Player.NetworkId == Player.NetworkId)
                {
                    if (hero.Player.HasBuff("ItemCrystalFlask"))
                        return;

                    if (hero.Player.Health / hero.Player.MaxHealth * 100 <=
                        Menu.Item("selflowhp" + Name + "pct").GetValue<Slider>().Value)
                    {
                        if ((hero.IncomeDamage > 0 || hero.MinionDamage > 0 || hero.TowerDamage > 0) ||
                            !Menu.Item("use" + Name + "cbat").GetValue<bool>())
                        {
                            if (!hero.Player.IsRecalling() && !hero.Player.InFountain())
                                UseItem();
                        }
                    }

                    if (hero.IncomeDamage / hero.Player.MaxHealth * 100 >=
                        Menu.Item("selfmuchhp" + Name + "pct").GetValue<Slider>().Value)
                    {
                        if (!hero.Player.IsRecalling() && !hero.Player.InFountain())
                            UseItem();
                    }
                }
            }
        }
    }
}
