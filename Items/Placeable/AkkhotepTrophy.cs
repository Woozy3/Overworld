using Terraria.ModLoader;
using Terraria.ID;

namespace Overworld.Items.Placeable
{
	public class AkkhotepTrophy : ModItem
	{
		public override void SetDefaults() {
			item.width = 30;
			item.height = 30;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 50000; //1 gold when sold
			item.rare = ItemRarityID.Blue;
			item.createTile = ModContent.TileType<Tiles.AkkhotepTrophy>();
			item.placeStyle = 2;
		}
	}
}