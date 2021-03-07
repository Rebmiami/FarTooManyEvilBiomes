using Microsoft.Xna.Framework;
//using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FarTooManyEvilBiomes.Biomes.ExperimentBiome.Tiles
{
	public class ExperimentTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = 11;
			drop = mod.ItemType("ExperimentTile_Item");

			AddMapEntry(new Color(0, 150, 0));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void RandomUpdate(int i, int j)
		{
			System.Collections.Generic.List<Vector2> adj = new System.Collections.Generic.List<Vector2>(); // List of all tile positions adjacent to the tile checked

			for (int a = -1; a <= 1; a++)
			{
				for (int b = -1; b <= 1; b++)
				{
					if (a == 0 && b == 0 || System.Math.Abs(a) == System.Math.Abs(b))
					{

					}
					else
					{
						if (Main.tile[i + a, j + b].type == Type)
						{
							adj.Add(new Vector2(a, b));
						}
					}
				}
			}

			Point spreadTarget = Point.Zero;

			if (adj.Count == 1)
			{
				Point opposite = Vector2.Negate(adj[0]).ToPoint();
				spreadTarget = new Point(i + opposite.X, j + opposite.Y);
			}
			else if (Main.rand.Next(3) == 0 && adj.Count > 0)
            {
				Point opposite = Vector2.Negate(adj[Main.rand.Next(adj.Count)]).ToPoint();
				spreadTarget = new Point(i + opposite.X, j + opposite.Y);
			}

			if (spreadTarget != Point.Zero && Main.tile[spreadTarget.X, spreadTarget.Y].active())
            {
				WorldGen.PlaceTile(spreadTarget.X, spreadTarget.Y, Type, true, true);
			}
			
			base.RandomUpdate(i, j);
		}
	}
}