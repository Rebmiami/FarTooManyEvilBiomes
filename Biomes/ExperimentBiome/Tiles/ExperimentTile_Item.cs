using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FarTooManyEvilBiomes.Biomes.ExperimentBiome.Tiles
{
	public class ExperimentTile_Item : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Tastes like a Terraria modder's blood");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = mod.TileType("ExperimentTile");
		}
	}
}