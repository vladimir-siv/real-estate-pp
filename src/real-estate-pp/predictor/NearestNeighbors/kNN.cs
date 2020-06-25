using System;
using System.Collections.Generic;

using repplib;

namespace predictor
{
	public class kNN
	{
		public delegate double Distance(double[] xt, double[] xo);
		public static Distance L1 => (xt, xo) =>
		{
			var dist = 0.0;

			for (var i = 0; i < xt.Length; ++i)
			{
				dist += Math.Abs(xt[i] - xo[i]);
			}

			return dist;
		};
		public static Distance L2 => (xt, xo) =>
		{
			var dist = 0.0;

			for (var i = 0; i < xt.Length; ++i)
			{
				dist += Math.Pow(xt[i] - xo[i], 2);
			}

			return Math.Sqrt(dist);
		};

		public int K { get; private set; }
		public Distance DistanceFunc { get; set; }

		private List<(double[], Category)> data = new List<(double[], Category)>();
		private Heap<double, Category> heap = new Heap<double, Category>(0.0);

		public void Add(double[] x, Category y)
		{
			data.Add((x, y));
		}

		public Category Predict(double[] x) => Predict(x, (int)Math.Ceiling(Math.Sqrt(data.Count)));
		public Category Predict(double[] x, int k)
		{
			if (k <= 0) return Predict(x);

			K = k;

			heap.Clear();
			foreach (var point in data) heap.Insert(DistanceFunc(x, point.Item1), point.Item2);

			var final = Category.None;
			var finalcnt = 0;

			var counts = new int[] { 0, 0, 0, 0, 0, 0 };

			for (var i = 0; i < k; ++i)
			{
				var neighbor = heap.RemoveMin().Data;
				var nindex = (int)neighbor;
				++counts[nindex];

				if (counts[nindex] > finalcnt)
				{
					final = neighbor;
					finalcnt = counts[nindex];
				}
			}

			return final;
		}
	}
}
