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
			System.Collections.Generic.List<Vector2> adjAir = new System.Collections.Generic.List<Vector2>(); // List of all air tiles adjacent to the tile checked
			System.Collections.Generic.List<Vector2> cAdj = new System.Collections.Generic.List<Vector2>(); // adj but counting diagonals
			System.Collections.Generic.List<Vector2> cAdjAir = new System.Collections.Generic.List<Vector2>(); // adjAir but counting diagonals
			// TODO: using
			for (int a = -1; a <= 1; a++)
			{
				for (int b = -1; b <= 1; b++)
				{
					if (a != 0 || b != 0)
					{
						if (System.Math.Abs(a) != System.Math.Abs(b))
                        {
							if (Main.tile[i + a, j + b].type == Type)
							{
								adj.Add(new Vector2(a, b));
							}
							if (!Main.tile[i + a, j + b].active())
							{
								adjAir.Add(new Vector2(a, b));
							}
						}
						if (Main.tile[i + a, j + b].type == Type)
						{
							cAdj.Add(new Vector2(a, b));
						}
						if (!Main.tile[i + a, j + b].active())
						{
							cAdjAir.Add(new Vector2(a, b));
						}
					}
				}
			}

			Point spreadTarget = Point.Zero;

			if (Main.rand.Next(16) == 0)
            {
				spreadTarget = new Point(i + Main.rand.Next(3) - 1, j + Main.rand.Next(3) - 1);
            }
			else
            {
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
			}

			if (spreadTarget != Point.Zero && Main.tile[spreadTarget.X, spreadTarget.Y].active())
			{
				WorldGen.PlaceTile(spreadTarget.X, spreadTarget.Y, Type, true, true);
			}

			if (cAdj.Count + cAdjAir.Count == 8)
			{
				if ((cAdj.Count < 3 || cAdj.Count == 7) && adjAir.Count > 0 && Main.rand.Next(3) == 0)
				{
					WorldGen.KillTile(i, j, false, false, true);
				}
				else if (Main.rand.Next(8) == 0 && cAdj.Count != 8)
                {
					WorldGen.KillTile(i, j, false, false, true);
				}
			}
			
			base.RandomUpdate(i, j);
		}
	}
}