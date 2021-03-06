﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ProceduralWorlds.Biomator
{
	[Serializable]
	public class BiomeSurfaceSwitch
	{
		//slope limit
		public bool					slopeEnabled;
		public float				minSlope;
		public float				maxSlope;

		//height limit
		public bool					heightEnabled;
		public float				minHeight;
		public float				maxHeight;

		//param limit
		public bool					paramEnabled;
		public float				minParam;
		public float				maxParam;

		//surface
		public BiomeSurface			surface = new BiomeSurface();

		public BiomeDetails			details = new BiomeDetails();

		public bool					Overlaps(BiomeSurfaceSwitch b2)
		{
			bool slopeOverlaps = (!slopeEnabled || !b2.slopeEnabled) || (slopeEnabled && b2.slopeEnabled && Utils.Overlap(minSlope, maxSlope, b2.minSlope, b2.maxSlope));
			bool heightOverlaps = (!heightEnabled || !b2.heightEnabled) || (heightEnabled && b2.heightEnabled && Utils.Overlap(minHeight, maxHeight, b2.minHeight, b2.maxHeight));
			bool paramOverlaps = (!paramEnabled || !b2.paramEnabled) || (paramEnabled && b2.paramEnabled && Utils.Overlap(minParam, maxParam, b2.minParam, b2.maxParam));

			return slopeOverlaps && heightOverlaps && paramOverlaps;
		}

		public float				GetWeight(float heightRange, float slopeRange, float paramRange)
		{
			float slope = (slopeEnabled) ? (maxSlope - minSlope) / slopeRange : 1;
			float height = (heightEnabled) ? (maxHeight - minHeight) / heightRange : 1;
			float param = (paramEnabled) ? (maxParam - minParam) / paramRange : 1;

			return slope + height + param;
		}

		public bool					Matches(float height, float slope, float param)
		{
			if (heightEnabled && (height < minHeight || height > maxHeight))
				return false;
			if (slopeEnabled && (slope < minSlope || slope > maxSlope))
				return false;
			if (paramEnabled && (param < minParam || param > maxParam))
				return false;
			
			return true;
		}

		public float				GapWidth(BiomeSurfaceSwitch b2)
		{
			float	gap = 0;

			if (heightEnabled)
				gap += Utils.GapWidth(minHeight, maxHeight, b2.minHeight, b2.maxHeight);
			if (slopeEnabled)
				gap += Utils.GapWidth(minSlope, maxSlope, b2.minSlope, b2.maxSlope);
			if (paramEnabled)
				gap += Utils.GapWidth(minParam, maxParam, b2.minParam, b2.maxParam);
			
			return gap;
		}
	}

}