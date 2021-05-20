using Overworld.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Overworld.Items.Weapons.Summon
{
	public class HieroglyphicTome : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Summons a mini Akkhotep to fight for you.");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; //For people using controllers over a keyboard and mouse
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true; //I'm honestly not sure what this does lul.
		}

		public override void SetDefaults() {
			item.damage = 26;
			item.summon = true;
			item.mana = 10;
			item.width = 26;
			item.height = 28;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 5; //Is a tome I assume it's gonna be useStyle 5.
			item.noMelee = true;
			item.knockBack = 3;
			item.value = 3500*5; //I believe I forgot that value is divided by 5 for other items.
			item.rare = 2; //idk the rarity ID you all want
			item.UseSound = SoundID.Item44; //Imported from ExampleMod
			item.shoot = ModContent.ProjectileType<AkkhotepMinion>();
			item.buffType = ModContent.BuffType<Buffs.AkkhotepMinion>(); //Adds the buff that keeps track of the minion
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			player.AddBuff(item.buffType, 2);
			position = Main.MouseWorld;
			return true;
		}
	}
}