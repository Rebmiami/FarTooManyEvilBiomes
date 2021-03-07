using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using static Terraria.ModLoader.ModContent;

namespace FarTooManyEvilBiomes.Items
{
	public class WandOfAcceleration : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Speeds up random growth, including plants and evil biomes");
		}
		
		public override void SetDefaults()
		{
			item.useTime = 2;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.buyPrice(gold: 1, silver: 50);
			item.rare = ItemRarityID.Blue;
			item.autoReuse = true;
		}
		
		public override bool UseItem(Player player)
		{
			Vector2 mouse = Main.MouseWorld;
			int radius = 3;
			mouse += (new Vector2(Main.rand.Next(radius * 2), Main.rand.Next(radius * 2)) - new Vector2(radius, radius)) * 16;
			Dust.NewDust(mouse, 0, 0, 1);
			Point tile = (mouse / 16).ToPoint();
			// Random tile update code copied from WorldGen.cs with several edits
			int num = 20;
			int num9 = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
			int num10 = WorldGen.genRand.Next(10, (int)Main.worldSurface - 1);
			int num11 = num9 - 1;
			int num12 = num9 + 2;
			int num13 = num10 - 1;
			int num14 = num10 + 2;
			if (num11 < 10)
			{
				num11 = 10;
			}
			if (num12 > Main.maxTilesX - 10)
			{
				num12 = Main.maxTilesX - 10;
			}
			if (num13 < 10)
			{
				num13 = 10;
			}
			if (num14 > Main.maxTilesY - 10)
			{
				num14 = Main.maxTilesY - 10;
			}
			int num5;
			if (Main.tile[tile.X, tile.Y] != null)
			{
				if (Main.tileAlch[Main.tile[tile.X, tile.Y].type])
				{
					WorldGen.GrowAlch(tile.X, tile.Y);
				}
				if (Main.tile[tile.X, tile.Y].liquid > 32)
				{
					if (Main.tile[tile.X, tile.Y].active() && (Main.tile[tile.X, tile.Y].type == 3 || Main.tile[tile.X, tile.Y].type == 20 || Main.tile[tile.X, tile.Y].type == 24 || Main.tile[tile.X, tile.Y].type == 27 || Main.tile[tile.X, tile.Y].type == 73 || Main.tile[tile.X, tile.Y].type == 201))
					{
						WorldGen.KillTile(tile.X, tile.Y);
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, tile.X, tile.Y);
						}
					}
				}
				else if (Main.tile[tile.X, tile.Y].nactive())
				{
					WorldGen.hardUpdateWorld(tile.X, tile.Y);
					if (Main.rand.Next(3000) == 0)
					{
						WorldGen.plantDye(tile.X, tile.Y);
					}
					if (Main.rand.Next(9001) == 0)
					{
						WorldGen.plantDye(tile.X, tile.Y, exoticPlant: true);
					}
					if (Main.tile[tile.X, tile.Y].type == 80)
					{
						if (WorldGen.genRand.Next(15) == 0)
						{
							WorldGen.GrowCactus(tile.X, tile.Y);
						}
					}
					else if (TileID.Sets.Conversion.Sand[Main.tile[tile.X, tile.Y].type])
					{
						if (!Main.tile[tile.X, num13].active())
						{
							if (tile.X < 250 || tile.X > Main.maxTilesX - 250)
							{
								if (WorldGen.genRand.Next(500) == 0)
								{
									int num15 = 7;
									int num16 = 6;
									int num17 = 0;
									for (int num18 = tile.X - num15; num18 <= tile.X + num15; num18 = num5 + 1)
									{
										for (int num19 = num13 - num15; num19 <= num13 + num15; num19 = num5 + 1)
										{
											if (Main.tile[num18, num19].active() && Main.tile[num18, num19].type == 81)
											{
												num5 = num17;
												num17 = num5 + 1;
											}
											num5 = num19;
										}
										num5 = num18;
									}
									if (num17 < num16 && Main.tile[tile.X, num13].liquid == byte.MaxValue && Main.tile[tile.X, num13 - 1].liquid == byte.MaxValue && Main.tile[tile.X, num13 - 2].liquid == byte.MaxValue && Main.tile[tile.X, num13 - 3].liquid == byte.MaxValue && Main.tile[tile.X, num13 - 4].liquid == byte.MaxValue)
									{
										WorldGen.PlaceTile(tile.X, num13, 81, mute: true);
										if (Main.netMode == NetmodeID.Server && Main.tile[tile.X, num13].active())
										{
											NetMessage.SendTileSquare(-1, tile.X, num13, 1);
										}
									}
								}
							}
							else if (tile.X > 400 && tile.X < Main.maxTilesX - 400 && WorldGen.genRand.Next(300) == 0)
							{
								WorldGen.GrowCactus(tile.X, tile.Y);
							}
						}
					}
					else if (Main.tile[tile.X, tile.Y].type == 116 || Main.tile[tile.X, tile.Y].type == 112 || Main.tile[tile.X, tile.Y].type == 234)
					{
						if (!Main.tile[tile.X, num13].active() && tile.X > 400 && tile.X < Main.maxTilesX - 400 && WorldGen.genRand.Next(300) == 0)
						{
							WorldGen.GrowCactus(tile.X, tile.Y);
						}
					}
					else if (Main.tile[tile.X, tile.Y].type == 147 || Main.tile[tile.X, tile.Y].type == 161 || Main.tile[tile.X, tile.Y].type == 163 || Main.tile[tile.X, tile.Y].type == 164 || Main.tile[tile.X, tile.Y].type == 200)
					{
						if (Main.rand.Next(10) == 0 && !Main.tile[tile.X, tile.Y + 1].active() && !Main.tile[tile.X, tile.Y + 2].active())
						{
							int num20 = tile.X - 3;
							int num21 = tile.X + 4;
							int num22 = 0;
							for (int num23 = num20; num23 < num21; num23 = num5 + 1)
							{
								if (Main.tile[num23, tile.Y].type == 165 && Main.tile[num23, tile.Y].active())
								{
									num5 = num22;
									num22 = num5 + 1;
								}
								if (Main.tile[num23, tile.Y + 1].type == 165 && Main.tile[num23, tile.Y + 1].active())
								{
									num5 = num22;
									num22 = num5 + 1;
								}
								if (Main.tile[num23, tile.Y + 2].type == 165 && Main.tile[num23, tile.Y + 2].active())
								{
									num5 = num22;
									num22 = num5 + 1;
								}
								if (Main.tile[num23, tile.Y + 3].type == 165 && Main.tile[num23, tile.Y + 3].active())
								{
									num5 = num22;
									num22 = num5 + 1;
								}
								num5 = num23;
							}
							if (num22 < 2)
							{
								WorldGen.PlaceTight(tile.X, tile.Y + 1, 165);
								WorldGen.SquareTileFrame(tile.X, tile.Y + 1);
								if (Main.netMode == NetmodeID.Server && Main.tile[tile.X, tile.Y + 1].active())
								{
									NetMessage.SendTileSquare(-1, tile.X, tile.Y + 1, 3);
								}
							}
						}
					}
					else if (Main.tile[tile.X, tile.Y].type == 254)
					{
						if (Main.rand.Next((Main.tile[tile.X, tile.Y].frameX + 10) / 10) == 0)
						{
							WorldGen.GrowPumpkin(tile.X, tile.Y, 254);
						}
					}
					else if (Main.tile[tile.X, tile.Y].type == 78 || Main.tile[tile.X, tile.Y].type == 380)
					{
						if (!Main.tile[tile.X, num13].active() && WorldGen.genRand.Next(2) == 0)
						{
							WorldGen.PlaceTile(tile.X, num13, 3, mute: true);
							if (Main.netMode == NetmodeID.Server && Main.tile[tile.X, num13].active())
							{
								NetMessage.SendTileSquare(-1, tile.X, num13, 1);
							}
						}
					}
					else if (Main.tile[tile.X, tile.Y].type == 2 || Main.tile[tile.X, tile.Y].type == 23 || Main.tile[tile.X, tile.Y].type == 32 || Main.tile[tile.X, tile.Y].type == 109 || Main.tile[tile.X, tile.Y].type == 199 || Main.tile[tile.X, tile.Y].type == 352)
					{
						int num24 = Main.tile[tile.X, tile.Y].type;
						if (Main.halloween && WorldGen.genRand.Next(75) == 0 && (num24 == 2 || num24 == 109))
						{
							int num25 = 100;
							int num26 = 0;
							for (int i = tile.X - num25; i < tile.X + num25; i += 2)
							{
								for (int j = tile.Y - num25; j < tile.Y + num25; j += 2)
								{
									if (i > 1 && i < Main.maxTilesX - 2 && j > 1 && j < Main.maxTilesY - 2 && Main.tile[i, j].active() && Main.tile[i, j].type == 254)
									{
										num5 = num26;
										num26 = num5 + 1;
									}
								}
							}
							if (num26 < 6)
							{
								WorldGen.PlacePumpkin(tile.X, num13);
								if (Main.netMode == NetmodeID.Server && Main.tile[tile.X, num13].type == 254)
								{
									NetMessage.SendTileSquare(-1, tile.X, num13, 4);
								}
							}
						}
						if (!Main.tile[tile.X, num13].active() && WorldGen.genRand.Next(12) == 0 && num24 == 2 && WorldGen.PlaceTile(tile.X, num13, 3, mute: true))
						{
							Main.tile[tile.X, num13].color(Main.tile[tile.X, tile.Y].color());
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, tile.X, num13, 1);
							}
						}
						if (!Main.tile[tile.X, num13].active() && WorldGen.genRand.Next(10) == 0 && num24 == 23 && WorldGen.PlaceTile(tile.X, num13, 24, mute: true))
						{
							Main.tile[tile.X, num13].color(Main.tile[tile.X, tile.Y].color());
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, tile.X, num13, 1);
							}
						}
						if (!Main.tile[tile.X, num13].active() && WorldGen.genRand.Next(10) == 0 && num24 == 109 && WorldGen.PlaceTile(tile.X, num13, 110, mute: true))
						{
							Main.tile[tile.X, num13].color(Main.tile[tile.X, tile.Y].color());
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, tile.X, num13, 1);
							}
						}
						if (!Main.tile[tile.X, num13].active() && WorldGen.genRand.Next(10) == 0 && num24 == 199 && WorldGen.PlaceTile(tile.X, num13, 201, mute: true))
						{
							Main.tile[tile.X, num13].color(Main.tile[tile.X, tile.Y].color());
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, tile.X, num13, 1);
							}
						}
						bool flag2 = false;
						for (int num27 = num11; num27 < num12; num27 = num5 + 1)
						{
							for (int num28 = num13; num28 < num14; num28 = num5 + 1)
							{
								if ((tile.X != num27 || tile.Y != num28) && Main.tile[num27, num28].active())
								{
									if (num24 == 32)
									{
										num24 = 23;
									}
									if (num24 == 352)
									{
										num24 = 199;
									}
									if (Main.tile[num27, num28].type == 0 || (num24 == 23 && Main.tile[num27, num28].type == 2) || (num24 == 199 && Main.tile[num27, num28].type == 2) || (num24 == 23 && Main.tile[num27, num28].type == 109))
									{
										WorldGen.SpreadGrass(num27, num28, 0, num24, repeat: false, Main.tile[tile.X, tile.Y].color());
										if (num24 == 23)
										{
											WorldGen.SpreadGrass(num27, num28, 2, num24, repeat: false, Main.tile[tile.X, tile.Y].color());
										}
										if (num24 == 23)
										{
											WorldGen.SpreadGrass(num27, num28, 109, num24, repeat: false, Main.tile[tile.X, tile.Y].color());
										}
										if (num24 == 199)
										{
											WorldGen.SpreadGrass(num27, num28, 2, num24, repeat: false, Main.tile[tile.X, tile.Y].color());
										}
										if (num24 == 199)
										{
											WorldGen.SpreadGrass(num27, num28, 109, num24, repeat: false, Main.tile[tile.X, tile.Y].color());
										}
										if (Main.tile[num27, num28].type == num24)
										{
											WorldGen.SquareTileFrame(num27, num28);
											flag2 = true;
										}
									}
									if (Main.tile[num27, num28].type == 0 || (num24 == 109 && Main.tile[num27, num28].type == 2) || (num24 == 109 && Main.tile[num27, num28].type == 23) || (num24 == 109 && Main.tile[num27, num28].type == 199))
									{
										WorldGen.SpreadGrass(num27, num28, 0, num24, repeat: false, Main.tile[tile.X, tile.Y].color());
										if (num24 == 109)
										{
											WorldGen.SpreadGrass(num27, num28, 2, num24, repeat: false, Main.tile[tile.X, tile.Y].color());
										}
										if (num24 == 109)
										{
											WorldGen.SpreadGrass(num27, num28, 23, num24, repeat: false, Main.tile[tile.X, tile.Y].color());
										}
										if (num24 == 109)
										{
											WorldGen.SpreadGrass(num27, num28, 199, num24, repeat: false, Main.tile[tile.X, tile.Y].color());
										}
										if (Main.tile[num27, num28].type == num24)
										{
											WorldGen.SquareTileFrame(num27, num28);
											flag2 = true;
										}
									}
								}
								num5 = num28;
							}
							num5 = num27;
						}
						if (Main.netMode == NetmodeID.Server && flag2)
						{
							NetMessage.SendTileSquare(-1, tile.X, tile.Y, 3);
						}
					}
					else if (Main.tile[tile.X, tile.Y].type == 20 && WorldGen.genRand.Next(20) == 0)
					{
						bool flag3 = WorldGen.PlayerLOS(tile.X, tile.Y);
						bool flag4 = (Main.tile[tile.X, tile.Y].frameX < 324 || Main.tile[tile.X, tile.Y].frameX >= 540) ? WorldGen.GrowTree(tile.X, tile.Y) : WorldGen.GrowPalmTree(tile.X, tile.Y);
						if (flag4 && flag3)
						{
							WorldGen.TreeGrowFXCheck(tile.X, tile.Y);
						}
					}
					if (Main.tile[tile.X, tile.Y].type == 3 && WorldGen.genRand.Next(20) == 0 && Main.tile[tile.X, tile.Y].frameX != 144)
					{
						if ((Main.tile[tile.X, tile.Y].frameX < 144 && Main.rand.Next(10) == 0) || ((Main.tile[tile.X, tile.Y + 1].type == 78 || Main.tile[tile.X, tile.Y + 1].type == 380) && Main.rand.Next(2) == 0))
						{
							Main.tile[tile.X, tile.Y].frameX = (short)(198 + WorldGen.genRand.Next(10) * 18);
						}
						Main.tile[tile.X, tile.Y].type = 73;
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, tile.X, tile.Y, 3);
						}
					}
					if (Main.tile[tile.X, tile.Y].type == 110 && WorldGen.genRand.Next(20) == 0 && Main.tile[tile.X, tile.Y].frameX < 144)
					{
						Main.tile[tile.X, tile.Y].type = 113;
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, tile.X, tile.Y, 3);
						}
					}
					if (Main.tile[tile.X, tile.Y].type == 32 && WorldGen.genRand.Next(3) == 0)
					{
						int num29 = tile.X;
						int num30 = tile.Y;
						int num31 = 0;
						if (Main.tile[num29 + 1, num30].active() && Main.tile[num29 + 1, num30].type == 32)
						{
							num5 = num31;
							num31 = num5 + 1;
						}
						if (Main.tile[num29 - 1, num30].active() && Main.tile[num29 - 1, num30].type == 32)
						{
							num5 = num31;
							num31 = num5 + 1;
						}
						if (Main.tile[num29, num30 + 1].active() && Main.tile[num29, num30 + 1].type == 32)
						{
							num5 = num31;
							num31 = num5 + 1;
						}
						if (Main.tile[num29, num30 - 1].active() && Main.tile[num29, num30 - 1].type == 32)
						{
							num5 = num31;
							num31 = num5 + 1;
						}
						if (num31 < 3 || Main.tile[tile.X, tile.Y].type == 23)
						{
							switch (WorldGen.genRand.Next(4))
							{
								case 0:
									num5 = num30;
									num30 = num5 - 1;
									break;
								case 1:
									num5 = num30;
									num30 = num5 + 1;
									break;
								case 2:
									num5 = num29;
									num29 = num5 - 1;
									break;
								case 3:
									num5 = num29;
									num29 = num5 + 1;
									break;
							}
							if (!Main.tile[num29, num30].active())
							{
								num31 = 0;
								if (Main.tile[num29 + 1, num30].active() && Main.tile[num29 + 1, num30].type == 32)
								{
									num5 = num31;
									num31 = num5 + 1;
								}
								if (Main.tile[num29 - 1, num30].active() && Main.tile[num29 - 1, num30].type == 32)
								{
									num5 = num31;
									num31 = num5 + 1;
								}
								if (Main.tile[num29, num30 + 1].active() && Main.tile[num29, num30 + 1].type == 32)
								{
									num5 = num31;
									num31 = num5 + 1;
								}
								if (Main.tile[num29, num30 - 1].active() && Main.tile[num29, num30 - 1].type == 32)
								{
									num5 = num31;
									num31 = num5 + 1;
								}
								if (num31 < 2)
								{
									int num32 = 7;
									int num33 = num29 - num32;
									int num34 = num29 + num32;
									int num35 = num30 - num32;
									int num36 = num30 + num32;
									bool flag5 = false;
									for (int num37 = num33; num37 < num34; num37 = num5 + 1)
									{
										for (int num38 = num35; num38 < num36; num38 = num5 + 1)
										{
											if (Math.Abs(num37 - num29) * 2 + Math.Abs(num38 - num30) < 9 && Main.tile[num37, num38].active() && Main.tile[num37, num38].type == 23 && Main.tile[num37, num38 - 1].active() && Main.tile[num37, num38 - 1].type == 32 && Main.tile[num37, num38 - 1].liquid == 0)
											{
												flag5 = true;
												break;
											}
											num5 = num38;
										}
										num5 = num37;
									}
									if (flag5)
									{
										Main.tile[num29, num30].type = 32;
										Main.tile[num29, num30].active(active: true);
										WorldGen.SquareTileFrame(num29, num30);
										if (Main.netMode == NetmodeID.Server)
										{
											NetMessage.SendTileSquare(-1, num29, num30, 3);
										}
									}
								}
							}
						}
					}
					if (Main.tile[tile.X, tile.Y].type == 352 && WorldGen.genRand.Next(3) == 0)
					{
						WorldGen.GrowSpike(tile.X, tile.Y, 352, 199);
					}
				}
				if (Main.tile[tile.X, tile.Y].wall == 81 || Main.tile[tile.X, tile.Y].wall == 83 || (Main.tile[tile.X, tile.Y].type == 199 && Main.tile[tile.X, tile.Y].active()))
				{
					int num39 = tile.X + WorldGen.genRand.Next(-2, 3);
					int num40 = tile.Y + WorldGen.genRand.Next(-2, 3);
					if (Main.tile[num39, num40].wall >= 63 && Main.tile[num39, num40].wall <= 68)
					{
						bool flag6 = false;
						for (int num41 = tile.X - num; num41 < tile.X + num; num41 = num5 + 1)
						{
							for (int num42 = tile.Y - num; num42 < tile.Y + num; num42 = num5 + 1)
							{
								if (Main.tile[tile.X, tile.Y].active())
								{
									int type = Main.tile[tile.X, tile.Y].type;
									if (type == 199 || type == 200 || type == 201 || type == 203 || type == 205 || type == 234 || type == 352)
									{
										flag6 = true;
										break;
									}
								}
								num5 = num42;
							}
							num5 = num41;
						}
						if (flag6)
						{
							Main.tile[num39, num40].wall = 81;
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, num39, num40, 3);
							}
						}
					}
				}
				if (Main.tile[tile.X, tile.Y].wall == 69 || Main.tile[tile.X, tile.Y].wall == 3 || (Main.tile[tile.X, tile.Y].type == 23 && Main.tile[tile.X, tile.Y].active()))
				{
					int num43 = tile.X + WorldGen.genRand.Next(-2, 3);
					int num44 = tile.Y + WorldGen.genRand.Next(-2, 3);
					if (Main.tile[num43, num44].wall >= 63 && Main.tile[num43, num44].wall <= 68)
					{
						bool flag7 = false;
						for (int num45 = tile.X - num; num45 < tile.X + num; num45 = num5 + 1)
						{
							for (int num46 = tile.Y - num; num46 < tile.Y + num; num46 = num5 + 1)
							{
								if (Main.tile[num45, num46].active())
								{
									int type2 = Main.tile[num45, num46].type;
									if (type2 == 22 || type2 == 23 || type2 == 24 || type2 == 25 || type2 == 32 || type2 == 112 || type2 == 163)
									{
										flag7 = true;
										break;
									}
								}
								num5 = num46;
							}
							num5 = num45;
						}
						if (flag7)
						{
							Main.tile[num43, num44].wall = 69;
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, num43, num44, 3);
							}
						}
					}
				}
				if (Main.tile[tile.X, tile.Y].wall == 70 || (Main.tile[tile.X, tile.Y].type == 109 && Main.tile[tile.X, tile.Y].active()))
				{
					int num47 = tile.X + WorldGen.genRand.Next(-2, 3);
					int num48 = tile.Y + WorldGen.genRand.Next(-2, 3);
					if (Main.tile[num47, num48].wall == 63 || Main.tile[num47, num48].wall == 65 || Main.tile[num47, num48].wall == 66 || Main.tile[num47, num48].wall == 68)
					{
						bool flag8 = false;
						for (int num49 = tile.X - num; num49 < tile.X + num; num49 = num5 + 1)
						{
							for (int num50 = tile.Y - num; num50 < tile.Y + num; num50 = num5 + 1)
							{
								if (Main.tile[num49, num50].active())
								{
									int type3 = Main.tile[num49, num50].type;
									if (type3 == 109 || type3 == 110 || type3 == 113 || type3 == 115 || type3 == 116 || type3 == 117 || type3 == 164)
									{
										flag8 = true;
										break;
									}
								}
								num5 = num50;
							}
							num5 = num49;
						}
						if (flag8)
						{
							Main.tile[num47, num48].wall = 70;
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, num47, num48, 3);
							}
						}
					}
				}
				WorldGen.SpreadDesertWalls(num, tile.X, tile.Y);
				if (Main.tile[tile.X, tile.Y].active())
				{
					if ((Main.tile[tile.X, tile.Y].type == 2 || Main.tile[tile.X, tile.Y].type == 52 || (Main.tile[tile.X, tile.Y].type == 192 && WorldGen.genRand.Next(10) == 0)) && WorldGen.genRand.Next(40) == 0 && !Main.tile[tile.X, tile.Y + 1].active() && !Main.tile[tile.X, tile.Y + 1].lava())
					{
						bool flag9 = false;
						for (int num51 = tile.Y; num51 > tile.Y - 10; num51 = num5 - 1)
						{
							if (Main.tile[tile.X, num51].bottomSlope())
							{
								flag9 = false;
								break;
							}
							if (Main.tile[tile.X, num51].active() && Main.tile[tile.X, num51].type == 2 && !Main.tile[tile.X, num51].bottomSlope())
							{
								flag9 = true;
								break;
							}
							num5 = num51;
						}
						if (flag9)
						{
							int num52 = tile.X;
							int num53 = tile.Y + 1;
							Main.tile[num52, num53].type = 52;
							Main.tile[num52, num53].active(active: true);
							Main.tile[num52, num53].color(Main.tile[tile.X, tile.Y].color());
							WorldGen.SquareTileFrame(num52, num53);
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, num52, num53, 3);
							}
						}
					}
					if (Main.tile[tile.X, tile.Y].type == 70)
					{
						int type4 = Main.tile[tile.X, tile.Y].type;
						if (!Main.tile[tile.X, num13].active() && WorldGen.genRand.Next(10) == 0)
						{
							WorldGen.PlaceTile(tile.X, num13, 71, mute: true);
							if (Main.tile[tile.X, num13].active())
							{
								Main.tile[tile.X, num13].color(Main.tile[tile.X, tile.Y].color());
							}
							if (Main.netMode == NetmodeID.Server && Main.tile[tile.X, num13].active())
							{
								NetMessage.SendTileSquare(-1, tile.X, num13, 1);
							}
						}
						if (WorldGen.genRand.Next(100) == 0)
						{
							bool flag10 = WorldGen.PlayerLOS(tile.X, tile.Y);
							bool flag11 = WorldGen.GrowTree(tile.X, tile.Y);
							if (flag11 && flag10)
							{
								WorldGen.TreeGrowFXCheck(tile.X, tile.Y - 1);
							}
						}
						bool flag12 = false;
						for (int num54 = num11; num54 < num12; num54 = num5 + 1)
						{
							for (int num55 = num13; num55 < num14; num55 = num5 + 1)
							{
								if ((tile.X != num54 || tile.Y != num55) && Main.tile[num54, num55].active() && Main.tile[num54, num55].type == 59)
								{
									WorldGen.SpreadGrass(num54, num55, 59, type4, repeat: false, Main.tile[tile.X, tile.Y].color());
									if (Main.tile[num54, num55].type == type4)
									{
										WorldGen.SquareTileFrame(num54, num55);
										flag12 = true;
									}
								}
								num5 = num55;
							}
							num5 = num54;
						}
						if (Main.netMode == NetmodeID.Server && flag12)
						{
							NetMessage.SendTileSquare(-1, tile.X, tile.Y, 3);
						}
					}
					if (Main.tile[tile.X, tile.Y].type == 60)
					{
						int type5 = Main.tile[tile.X, tile.Y].type;
						if (!Main.tile[tile.X, num13].active() && WorldGen.genRand.Next(7) == 0)
						{
							WorldGen.PlaceTile(tile.X, num13, 61, mute: true);
							if (Main.tile[tile.X, num13].active())
							{
								Main.tile[tile.X, num13].color(Main.tile[tile.X, tile.Y].color());
							}
							if (Main.netMode == NetmodeID.Server && Main.tile[tile.X, num13].active())
							{
								NetMessage.SendTileSquare(-1, tile.X, num13, 1);
							}
						}
						else if (WorldGen.genRand.Next(500) == 0 && (!Main.tile[tile.X, num13].active() || Main.tile[tile.X, num13].type == 61 || Main.tile[tile.X, num13].type == 74 || Main.tile[tile.X, num13].type == 69))
						{
							if (WorldGen.GrowTree(tile.X, tile.Y) && WorldGen.PlayerLOS(tile.X, tile.Y))
							{
								WorldGen.TreeGrowFXCheck(tile.X, tile.Y - 1);
							}
						}
						else if (WorldGen.genRand.Next(25) == 0 && Main.tile[tile.X, num13].liquid == 0)
						{
							WorldGen.PlaceJunglePlant(tile.X, num13, 233, WorldGen.genRand.Next(8), 0);
							if (Main.tile[tile.X, num13].type == 233)
							{
								if (Main.netMode == NetmodeID.Server)
								{
									NetMessage.SendTileSquare(-1, tile.X, num13, 4);
								}
								else
								{
									WorldGen.PlaceJunglePlant(tile.X, num13, 233, WorldGen.genRand.Next(12), 1);
									if (Main.tile[tile.X, num13].type == 233 && Main.netMode == NetmodeID.Server)
									{
										NetMessage.SendTileSquare(-1, tile.X, num13, 3);
									}
								}
							}
						}
						bool flag13 = false;
						for (int num56 = num11; num56 < num12; num56 = num5 + 1)
						{
							for (int num57 = num13; num57 < num14; num57 = num5 + 1)
							{
								if ((tile.X != num56 || tile.Y != num57) && Main.tile[num56, num57].active() && Main.tile[num56, num57].type == 59)
								{
									WorldGen.SpreadGrass(num56, num57, 59, type5, repeat: false, Main.tile[tile.X, tile.Y].color());
									if (Main.tile[num56, num57].type == type5)
									{
										WorldGen.SquareTileFrame(num56, num57);
										flag13 = true;
									}
								}
								num5 = num57;
							}
							num5 = num56;
						}
						if (Main.netMode == NetmodeID.Server && flag13)
						{
							NetMessage.SendTileSquare(-1, tile.X, tile.Y, 3);
						}
					}
					if (Main.tile[tile.X, tile.Y].type == 61 && WorldGen.genRand.Next(3) == 0 && Main.tile[tile.X, tile.Y].frameX < 144)
					{
						if (Main.rand.Next(4) == 0)
						{
							Main.tile[tile.X, tile.Y].frameX = (short)(162 + WorldGen.genRand.Next(8) * 18);
						}
						Main.tile[tile.X, tile.Y].type = 74;
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, tile.X, tile.Y, 3);
						}
					}
					if ((Main.tile[tile.X, tile.Y].type == 60 || Main.tile[tile.X, tile.Y].type == 62) && WorldGen.genRand.Next(15) == 0 && !Main.tile[tile.X, tile.Y + 1].active() && !Main.tile[tile.X, tile.Y + 1].lava())
					{
						bool flag14 = false;
						for (int num58 = tile.Y; num58 > tile.Y - 10; num58 = num5 - 1)
						{
							if (Main.tile[tile.X, num58].bottomSlope())
							{
								flag14 = false;
								break;
							}
							if (Main.tile[tile.X, num58].active() && Main.tile[tile.X, num58].type == 60 && !Main.tile[tile.X, num58].bottomSlope())
							{
								flag14 = true;
								break;
							}
							num5 = num58;
						}
						if (flag14)
						{
							int num59 = tile.X;
							int num60 = tile.Y + 1;
							Main.tile[num59, num60].type = 62;
							Main.tile[num59, num60].active(active: true);
							WorldGen.SquareTileFrame(num59, num60);
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, num59, num60, 3);
							}
						}
					}
					if ((Main.tile[tile.X, tile.Y].type == 109 || Main.tile[tile.X, tile.Y].type == 115) && WorldGen.genRand.Next(15) == 0 && !Main.tile[tile.X, tile.Y + 1].active() && !Main.tile[tile.X, tile.Y + 1].lava())
					{
						bool flag15 = false;
						for (int num61 = tile.Y; num61 > tile.Y - 10; num61 = num5 - 1)
						{
							if (Main.tile[tile.X, num61].bottomSlope())
							{
								flag15 = false;
								break;
							}
							if (Main.tile[tile.X, num61].active() && Main.tile[tile.X, num61].type == 109 && !Main.tile[tile.X, num61].bottomSlope())
							{
								flag15 = true;
								break;
							}
							num5 = num61;
						}
						if (flag15)
						{
							int num62 = tile.X;
							int num63 = tile.Y + 1;
							Main.tile[num62, num63].type = 115;
							Main.tile[num62, num63].active(active: true);
							WorldGen.SquareTileFrame(num62, num63);
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, num62, num63, 3);
							}
						}
					}
					if ((Main.tile[tile.X, tile.Y].type == 199 || Main.tile[tile.X, tile.Y].type == 205) && WorldGen.genRand.Next(15) == 0 && !Main.tile[tile.X, tile.Y + 1].active() && !Main.tile[tile.X, tile.Y + 1].lava())
					{
						bool flag16 = false;
						for (int num64 = tile.Y; num64 > tile.Y - 10; num64 = num5 - 1)
						{
							if (Main.tile[tile.X, num64].bottomSlope())
							{
								flag16 = false;
								break;
							}
							if (Main.tile[tile.X, num64].active() && Main.tile[tile.X, num64].type == 199 && !Main.tile[tile.X, num64].bottomSlope())
							{
								flag16 = true;
								break;
							}
							num5 = num64;
						}
						if (flag16)
						{
							int num65 = tile.X;
							int num66 = tile.Y + 1;
							Main.tile[num65, num66].type = 205;
							Main.tile[num65, num66].active(active: true);
							WorldGen.SquareTileFrame(num65, num66);
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendTileSquare(-1, num65, num66, 3);
							}
						}
					}
				}
				TileLoader.RandomUpdate(tile.X, tile.Y, Main.tile[tile.X, tile.Y].type);
				WallLoader.RandomUpdate(tile.X, tile.Y, Main.tile[tile.X, tile.Y].wall);
			}

			//Main.tile[tile.X, tile.Y];
			return true;
		}
	}
}