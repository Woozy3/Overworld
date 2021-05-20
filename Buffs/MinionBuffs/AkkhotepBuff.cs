using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; //Not usually recommended by tML Discord, but I like the convenience.
using Overworld.Projectiles.Minions;

namespace Overworld.Buffs.MinionBuffs
{
	class AkkhotepBuff : ModBuff //This needs a buff sprite. 
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Akkhotep Minion");
			Description.SetDefault("A miniature Akkhotep will fight for you.");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<OverworldPlayer>().akkhotepMinion = true;
			if (player.ownedProjectileCounts[ProjectileType<AkkhotepMinion>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
