using Terraria.ID;
using Terraria.ModLoader;

namespace Overworld.Items
{
    public class FrostjadeDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrostJade Dagger");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.damage = 24;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 3;
            item.knockBack = 15;
            item.value = 10000;
            item.rare = 1;
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