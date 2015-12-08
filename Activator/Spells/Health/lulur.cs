﻿using System;
using Activator.Base;
using LeagueSharp;
using LeagueSharp.Common;

namespace Activator.Spells.Health
{
    class lulur : CoreSpell
    {
        internal override string Name
        {
            get { return "lulur"; }
        }

        internal override string DisplayName
        {
            get { return "Wild Growth | R"; }
        }

        internal override float Range
        {
            get { return 900f; }
        }

        internal override MenuType[] Category
        {
            get { return new[] { MenuType.SelfLowHP, MenuType.SelfCount  }; }
        }

        internal override int DefaultHP
        {
            get { return 20; }
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
                if (hero.Player.Distance(Player.ServerPosition) <= Range)
                {
                    if (!Parent.Item(Parent.Name + "useon" + hero.Player.NetworkId).GetValue<bool>())
                        continue;

                    if (!Player.HasBuffOfType(BuffType.Invulnerability))
                    {
                        if (hero.Player.Health/hero.Player.MaxHealth*100 <=
                            Menu.Item("selflowhp" + Name + "pct").GetValue<Slider>().Value)
                        {
                            if (hero.IncomeDamage > 0)
                                UseSpellOn(hero.Player);
                        }

                        if (hero.Player.CountEnemiesInRange(300) >=
                            Menu.Item("selfcount" + Name).GetValue<Slider>().Value)
                            UseSpellOn(hero.Player);
                    }
                }
            }
        }
    }
}
