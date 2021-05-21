using Terraria.ModLoader;
using Terraria.ID;

namespace WhisperingDeath.Items.Boss
{
	[AutoloadEquip(EquipType.Head)]
	public class AakhotepMask : ModItem
	{
        public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
			item.rare = ItemRarityID.Orange;
			item.vanity = true;
		}

		public override bool DrawHead()
		{
			return false;
		}
	}
}