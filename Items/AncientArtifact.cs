using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WhisperingDeath.NPCs.Bosses;

namespace WhisperingDeath.Items.Boss
{
	public class AncientArtifact : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("An ancient Gods curse still lingers on this artifact...\nCan only be used in the Desert");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13;
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 20;
			item.rare = ItemRarityID.Orange;
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.UseSound = SoundID.Item44;
			item.consumable = true;
		}
		public override bool CanUseItem(Player player)
		{
			return player.ZoneDesert && NPC.AnyNPCs(ModContent.NPCType<Aakhotep>()) == false;
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Aakhotep>());
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
	}
}