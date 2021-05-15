using Terraria.ID;
using Terraria.ModLoader;

namespace Overworld.Items
{
    public class Bonebreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone breaker");
            Tooltip.SetDefault("Crushes even rockens to dust");
        }
        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 58;
            item.height = 70;
            item.useTime = 28;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 20;
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