using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
namespace Overworld.Items.Accessories
{
	public class RingOfAnubis : ModItem
    {
        public override void SetDefaults()
        {
			item.width = 24;
			item.height = 24;
			item.accessory = true;
			item.expertOnly = true;
			item.expert = true;
			item.rare = -12;
			item.value = 12000; //1 gold, 20 silver
			item.defense = 2; //chose 2 instead of 3. Still higher than shackle, but not quite as high as it could be.
			//This already has a handful of benefits, 3 defense on top would be op asf.
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			OverworldPlayer modPlayer = player.GetModPlayer<OverworldPlayer>();
			modPlayer.spikeShoot = true;
        }
    }
}