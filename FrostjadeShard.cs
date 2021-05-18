using Terraria.ID;
using Terraria.ModLoader;

namespace Overworld.Items
{
	public class FrostjadeShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostjade Shard");
			Tooltip.SetDefault("A shard of jade chilled to the core");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 100;
			item.rare = ItemRarityID.Blue;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<Ore.FrostjadeShardTile>();
		}
	}
}
