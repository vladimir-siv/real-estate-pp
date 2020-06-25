using System;
using System.Collections.Generic;

using repplib;

namespace predictor
{
	public class LinearRegressor
	{
		public delegate double UpdateFunc(int wi, double[] weigts, List<(double[], double)> points);
		public static readonly UpdateFunc MSE = (int wi, double[] w, List<(double[], double)> ps) =>
		{
			var update = 0.0;

			foreach (var p in ps)
			{
				var yh = w[0]; // * 1
				for (var i = 0; i < w.Length - 1; ++i)
					yh += w[i + 1] * p.Item1[i];
				update += (yh - p.Item2) * (wi == 0 ? 1 : p.Item1[wi - 1]);
			}

			return update / ps.Count;
		};

		public int Inputs { get; private set; }

		private List<(double[], double)> data = new List<(double[], double)>();
		private double[] w;

		public LinearRegressor(int inputs)
		{
			if (inputs <= 0) throw new ArgumentException("Number of inputs must be positive.");
			Inputs = inputs;
			w = new double[inputs + 1];
		}

		public void Add(double[] x, double y)
		{
			if (x.Length != Inputs) throw new ArgumentException("Input array length mismatch.");
			data.Add((x, y));
		}

		public void Train(UpdateFunc update, double lr = 0.1, int epochs = 1000)
		{
			var tw = new double[w.Length];

			for (var i = 0; i < w.Length; ++i)
				w[i] = Math2.Random(-1, +1);

			for (var epoch = 0; epoch < epochs; ++epoch)
			{
				for (var i = 0; i < w.Length; ++i)
					tw[i] = w[i] - lr * update(i, w, data);

				for (var i = 0; i < w.Length; ++i) w[i] = tw[i];
			}
		}

		public double Predict(double[] x)
		{
			if (x.Length != Inputs) throw new ArgumentException("Input array length mismatch.");

			var yh = w[0]; // * 1

			for (var i = 0; i < w.Length - 1; ++i)
				yh += w[i + 1] * x[i];

			return yh;
		}
	}
}
