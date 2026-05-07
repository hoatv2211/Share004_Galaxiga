using System;

namespace Spine
{
	public class SkeletonClipping
	{
		public ExposedList<float> ClippedVertices
		{
			get
			{
				return this.clippedVertices;
			}
		}

		public ExposedList<int> ClippedTriangles
		{
			get
			{
				return this.clippedTriangles;
			}
		}

		public ExposedList<float> ClippedUVs
		{
			get
			{
				return this.clippedUVs;
			}
		}

		public bool IsClipping
		{
			get
			{
				return this.clipAttachment != null;
			}
		}

		public int ClipStart(Slot slot, ClippingAttachment clip)
		{
			if (this.clipAttachment != null)
			{
				return 0;
			}
			this.clipAttachment = clip;
			int worldVerticesLength = clip.worldVerticesLength;
			float[] items = this.clippingPolygon.Resize(worldVerticesLength).Items;
			clip.ComputeWorldVertices(slot, 0, worldVerticesLength, items, 0, 2);
			SkeletonClipping.MakeClockwise(this.clippingPolygon);
			this.clippingPolygons = this.triangulator.Decompose(this.clippingPolygon, this.triangulator.Triangulate(this.clippingPolygon));
			foreach (ExposedList<float> exposedList in this.clippingPolygons)
			{
				SkeletonClipping.MakeClockwise(exposedList);
				exposedList.Add(exposedList.Items[0]);
				exposedList.Add(exposedList.Items[1]);
			}
			return this.clippingPolygons.Count;
		}

		public void ClipEnd(Slot slot)
		{
			if (this.clipAttachment != null && this.clipAttachment.endSlot == slot.data)
			{
				this.ClipEnd();
			}
		}

		public void ClipEnd()
		{
			if (this.clipAttachment == null)
			{
				return;
			}
			this.clipAttachment = null;
			this.clippingPolygons = null;
			this.clippedVertices.Clear(true);
			this.clippedTriangles.Clear(true);
			this.clippingPolygon.Clear(true);
		}

		public void ClipTriangles(float[] vertices, int verticesLength, int[] triangles, int trianglesLength, float[] uvs)
		{
			ExposedList<float> exposedList = this.clipOutput;
			ExposedList<float> exposedList2 = this.clippedVertices;
			ExposedList<int> exposedList3 = this.clippedTriangles;
			ExposedList<float>[] items = this.clippingPolygons.Items;
			int count = this.clippingPolygons.Count;
			int num = 0;
			exposedList2.Clear(true);
			this.clippedUVs.Clear(true);
			exposedList3.Clear(true);
			for (int i = 0; i < trianglesLength; i += 3)
			{
				int num2 = triangles[i] << 1;
				float num3 = vertices[num2];
				float num4 = vertices[num2 + 1];
				float num5 = uvs[num2];
				float num6 = uvs[num2 + 1];
				num2 = triangles[i + 1] << 1;
				float num7 = vertices[num2];
				float num8 = vertices[num2 + 1];
				float num9 = uvs[num2];
				float num10 = uvs[num2 + 1];
				num2 = triangles[i + 2] << 1;
				float num11 = vertices[num2];
				float num12 = vertices[num2 + 1];
				float num13 = uvs[num2];
				float num14 = uvs[num2 + 1];
				for (int j = 0; j < count; j++)
				{
					int num15 = exposedList2.Count;
					if (!this.Clip(num3, num4, num7, num8, num11, num12, items[j], exposedList))
					{
						float[] items2 = exposedList2.Resize(num15 + 6).Items;
						float[] items3 = this.clippedUVs.Resize(num15 + 6).Items;
						items2[num15] = num3;
						items2[num15 + 1] = num4;
						items2[num15 + 2] = num7;
						items2[num15 + 3] = num8;
						items2[num15 + 4] = num11;
						items2[num15 + 5] = num12;
						items3[num15] = num5;
						items3[num15 + 1] = num6;
						items3[num15 + 2] = num9;
						items3[num15 + 3] = num10;
						items3[num15 + 4] = num13;
						items3[num15 + 5] = num14;
						num15 = exposedList3.Count;
						int[] items4 = exposedList3.Resize(num15 + 3).Items;
						items4[num15] = num;
						items4[num15 + 1] = num + 1;
						items4[num15 + 2] = num + 2;
						num += 3;
						break;
					}
					int count2 = exposedList.Count;
					if (count2 != 0)
					{
						float num16 = num8 - num12;
						float num17 = num11 - num7;
						float num18 = num3 - num11;
						float num19 = num12 - num4;
						float num20 = 1f / (num16 * num18 + num17 * (num4 - num12));
						int num21 = count2 >> 1;
						float[] items5 = exposedList.Items;
						float[] items6 = exposedList2.Resize(num15 + num21 * 2).Items;
						float[] items7 = this.clippedUVs.Resize(num15 + num21 * 2).Items;
						for (int k = 0; k < count2; k += 2)
						{
							float num22 = items5[k];
							float num23 = items5[k + 1];
							items6[num15] = num22;
							items6[num15 + 1] = num23;
							float num24 = num22 - num11;
							float num25 = num23 - num12;
							float num26 = (num16 * num24 + num17 * num25) * num20;
							float num27 = (num19 * num24 + num18 * num25) * num20;
							float num28 = 1f - num26 - num27;
							items7[num15] = num5 * num26 + num9 * num27 + num13 * num28;
							items7[num15 + 1] = num6 * num26 + num10 * num27 + num14 * num28;
							num15 += 2;
						}
						num15 = exposedList3.Count;
						int[] items8 = exposedList3.Resize(num15 + 3 * (num21 - 2)).Items;
						num21--;
						for (int l = 1; l < num21; l++)
						{
							items8[num15] = num;
							items8[num15 + 1] = num + l;
							items8[num15 + 2] = num + l + 1;
							num15 += 3;
						}
						num += num21 + 1;
					}
				}
			}
		}

		internal bool Clip(float x1, float y1, float x2, float y2, float x3, float y3, ExposedList<float> clippingArea, ExposedList<float> output)
		{
			ExposedList<float> exposedList = output;
			bool result = false;
			ExposedList<float> exposedList2;
			if (clippingArea.Count % 4 >= 2)
			{
				exposedList2 = output;
				output = this.scratch;
			}
			else
			{
				exposedList2 = this.scratch;
			}
			exposedList2.Clear(true);
			exposedList2.Add(x1);
			exposedList2.Add(y1);
			exposedList2.Add(x2);
			exposedList2.Add(y2);
			exposedList2.Add(x3);
			exposedList2.Add(y3);
			exposedList2.Add(x1);
			exposedList2.Add(y1);
			output.Clear(true);
			float[] items = clippingArea.Items;
			int num = clippingArea.Count - 4;
			int num2 = 0;
			for (;;)
			{
				float num3 = items[num2];
				float num4 = items[num2 + 1];
				float num5 = items[num2 + 2];
				float num6 = items[num2 + 3];
				float num7 = num3 - num5;
				float num8 = num4 - num6;
				float[] items2 = exposedList2.Items;
				int num9 = exposedList2.Count - 2;
				int count = output.Count;
				int i = 0;
				while (i < num9)
				{
					float num10 = items2[i];
					float num11 = items2[i + 1];
					float num12 = items2[i + 2];
					float num13 = items2[i + 3];
					bool flag = num7 * (num13 - num6) - num8 * (num12 - num5) > 0f;
					if (num7 * (num11 - num6) - num8 * (num10 - num5) > 0f)
					{
						if (!flag)
						{
							float num14 = num13 - num11;
							float num15 = num12 - num10;
							float num16 = (num15 * (num4 - num11) - num14 * (num3 - num10)) / (num14 * (num5 - num3) - num15 * (num6 - num4));
							output.Add(num3 + (num5 - num3) * num16);
							output.Add(num4 + (num6 - num4) * num16);
							goto IL_222;
						}
						output.Add(num12);
						output.Add(num13);
					}
					else
					{
						if (flag)
						{
							float num17 = num13 - num11;
							float num18 = num12 - num10;
							float num19 = (num18 * (num4 - num11) - num17 * (num3 - num10)) / (num17 * (num5 - num3) - num18 * (num6 - num4));
							output.Add(num3 + (num5 - num3) * num19);
							output.Add(num4 + (num6 - num4) * num19);
							output.Add(num12);
							output.Add(num13);
							goto IL_222;
						}
						goto IL_222;
					}
					IL_224:
					i += 2;
					continue;
					IL_222:
					result = true;
					goto IL_224;
				}
				if (count == output.Count)
				{
					break;
				}
				output.Add(output.Items[0]);
				output.Add(output.Items[1]);
				if (num2 == num)
				{
					goto Block_7;
				}
				ExposedList<float> exposedList3 = output;
				output = exposedList2;
				output.Clear(true);
				exposedList2 = exposedList3;
				num2 += 2;
			}
			exposedList.Clear(true);
			return true;
			Block_7:
			if (exposedList != output)
			{
				exposedList.Clear(true);
				int j = 0;
				int num20 = output.Count - 2;
				while (j < num20)
				{
					exposedList.Add(output.Items[j]);
					j++;
				}
			}
			else
			{
				exposedList.Resize(exposedList.Count - 2);
			}
			return result;
		}

		private static void MakeClockwise(ExposedList<float> polygon)
		{
			float[] items = polygon.Items;
			int count = polygon.Count;
			float num = items[count - 2] * items[1] - items[0] * items[count - 1];
			int i = 0;
			int num2 = count - 3;
			while (i < num2)
			{
				float num3 = items[i];
				float num4 = items[i + 1];
				float num5 = items[i + 2];
				float num6 = items[i + 3];
				num += num3 * num6 - num5 * num4;
				i += 2;
			}
			if (num < 0f)
			{
				return;
			}
			int j = 0;
			int num7 = count - 2;
			int num8 = count >> 1;
			while (j < num8)
			{
				float num9 = items[j];
				float num10 = items[j + 1];
				int num11 = num7 - j;
				items[j] = items[num11];
				items[j + 1] = items[num11 + 1];
				items[num11] = num9;
				items[num11 + 1] = num10;
				j += 2;
			}
		}

		internal readonly Triangulator triangulator = new Triangulator();

		internal readonly ExposedList<float> clippingPolygon = new ExposedList<float>();

		internal readonly ExposedList<float> clipOutput = new ExposedList<float>(128);

		internal readonly ExposedList<float> clippedVertices = new ExposedList<float>(128);

		internal readonly ExposedList<int> clippedTriangles = new ExposedList<int>(128);

		internal readonly ExposedList<float> clippedUVs = new ExposedList<float>(128);

		internal readonly ExposedList<float> scratch = new ExposedList<float>();

		internal ClippingAttachment clipAttachment;

		internal ExposedList<ExposedList<float>> clippingPolygons;
	}
}
