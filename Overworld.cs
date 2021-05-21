using Terraria;
using Terraria.ModLoader;

public override void PostSetupContent() {
			Mod bossChecklist = ModLoader.GetMod("BossChecklist");
			if (bossChecklist != null) {
				bossChecklist.Call(
					"AddBoss",
					2.5f,
					new List<int> { ModContent.NPCType<NPCs.Aakhotep>()},
					this, // Mod
					"Aakhotep, The Forgotten One",
          ModContent.NPCType<Aakhotep>(),
					(Func<bool>)(() => OverWorld.downedAakhotep),
					ModContent.ItemType<Items.AncientArtifact>(),
					new List<int> { ModContent.ItemType<Items.Armor.AakhotepMask>(), ModContent.ItemType<Items.AakhotepTrophy>() },
					new List<int> { ModContent.ItemType<Items.Weapons.Melee.BoneSlicer>(), ModContent.ItemType<Items.Weapons.Magic.ScarabSceptre>(), ModContent.ItemType<Items.Weapons.Ranged.SandSplitter>(), },
					$"use an [i:{ModContent.ItemType<Items.AncientArtifact>}] in the desert"
				);
				bossChecklist.Call(
					"AddBoss",
					15.5f,
					ModContent.NPCType<PuritySpirit>(),
					this,
					"Purity Spirit",
					(Func<bool>)(() => ExampleWorld.downedPuritySpirit),
					ItemID.Bunny,
					new List<int> { ModContent.ItemType<Items.Armor.PuritySpiritMask>(), ModContent.ItemType<Items.Armor.BunnyMask>(), ModContent.ItemType<Items.Placeable.PuritySpiritTrophy>(), ModContent.ItemType<Items.Placeable.BunnyTrophy>(), ModContent.ItemType<Items.Placeable.TreeTrophy>() },
					new List<int> { ModContent.ItemType<Items.PurityShield>(), ItemID.Bunny },
					$"Kill a [i:{ItemID.Bunny}] in front of [i:{ModContent.ItemType<Items.Placeable.ElementalPurge>()}]"
				);
			}
		}
