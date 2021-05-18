using Terraria.ID;
using Terraria.ModLoader;

namespace Overworld.Items
{
    public class FrostjadePick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostjade Pickaxe");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.damage = 15;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 18;
            item.useAnimation = 10;
            item.pick = 45;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}