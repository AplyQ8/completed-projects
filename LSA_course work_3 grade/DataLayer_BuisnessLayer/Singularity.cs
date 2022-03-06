using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_BuisnessLayer
{
    public class Singularity
    {
		private static double[,] Matrix_Transpose(double[,] matrix)
		{

			int m = matrix.GetLength(0);
			int n = matrix.GetLength(1);

			double[,] transposed_matrix = new double[n, m];
			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < n; j++)
				{
					transposed_matrix[j, i] = matrix[i, j];
				}
			}
			return transposed_matrix;
		}
		public static double[,] Matrix_multiplier(double[,] matrix1, double[,] matrix2)
		{
			if (matrix1.GetLength(1) != matrix2.GetLength(0))
				throw new Exception("Матрицы нельзя перемножить");

			double[,] result = new double[matrix1.GetLength(0), matrix2.GetLength(1)];

			for (int i = 0; i < matrix1.GetLength(0); i++)
			{
				for (int j = 0; j < matrix2.GetLength(1); j++)
				{
					for (int k = 0; k < matrix2.GetLength(0); k++)
					{
						result[i, j] += matrix1[i, k] * matrix2[k, j];
					}
				}
			}

			return result;
		}

		public static void jacobi(int n, double[,] a, ref double[] d, ref double[,] v)
		{
			if (n == 0)
			{
				return;
			}
			double[] b = new double[n + n];
			//double[] z = b + n;
			uint i;
			uint j;
			for (i = 0; i < n; ++i)
			{
				b[i + n] = 0.0;
				b[i] = d[i] = a[i, i];
				for (j = 0; j < n; ++j)
				{
					v[i, j] = i == j ? 1.0 : 0.0;
				}
			}
			for (i = 0; i < 50; ++i)
			{
				double sm = 0.0;
				uint p;
				uint q;
				for (p = 0; p < n - 1; ++p)
				{
					for (q = p + 1; q < n; ++q)
					{
						sm += Math.Abs(a[p, q]);
					}
				}
				if (sm == 0)
				{
					break;
				}
				double tresh = i < 3 ? 0.2 * sm / (n * n) : 0.0;
				for (p = 0; p < n - 1; ++p)
				{
					for (q = p + 1; q < n; ++q)
					{
						double g = 1e12 * Math.Abs(a[p, q]);
						if (i >= 3 && Math.Abs(d[p]) > g && Math.Abs(d[q]) > g)
						{
							a[p, q] = 0.0;
						}
						else
						{
							if (Math.Abs(a[p, q]) > tresh)
							{
								double theta = 0.5 * (d[q] - d[p]) / a[p, q];
								double t = 1.0 / (Math.Abs(theta) + Math.Sqrt(1.0 + theta * theta));
								if (theta < 0)
								{
									t = -t;
								}
								double c = 1.0 / Math.Sqrt(1.0 + t * t);
								double s = t * c;
								double tau = s / (1.0 + c);
								double h = t * a[p, q];
								b[p + n] -= h;
								b[q + n] += h;
								d[p] -= h;
								d[q] += h;
								a[p, q] = 0.0;
								for (j = 0; j < p; ++j)
								{
									g = a[j, p];
									h = a[j, q];
									a[j, p] = g - s * (h + g * tau);
									a[j, q] = h + s * (g - h * tau);
								}
								for (j = p + 1; j < q; ++j)
								{
									g = a[p, j];
									h = a[j, q];
									a[p, j] = g - s * (h + g * tau);
									a[j, q] = h + s * (g - h * tau);
								}
								for (j = q + 1; j < n; ++j)
								{
									g = a[p, j];
									h = a[q, j];
									a[p, j] = g - s * (h + g * tau);
									a[q, j] = h + s * (g - h * tau);
								}
								for (j = 0; j < n; ++j)
								{
									g = v[j, p];
									h = v[j, q];
									v[j, p] = g - s * (h + g * tau);
									v[j, q] = h + s * (g - h * tau);
								}
							}
						}
					}
				}
				for (p = 0; p < n; ++p)
				{
					d[p] = (b[p] += b[p + n]);
					b[p + n] = 0.0;
				}
			}
			b = null;
		}
		public static void Swap(ref double[,] vector, int index_of_first_column, int index_of_second_column)
		{
			for (int i = 0; i < vector.GetLength(0); i++)
			{
				double temp = vector[i, index_of_first_column];
				vector[i, index_of_first_column] = vector[i, index_of_second_column];

				vector[i, index_of_second_column] = temp;
			}
		}
		public void Swap(ref double[] eigenvalues, int index_of_first_change, int index_of_second_change)
		{
			double temp = eigenvalues[index_of_first_change];
			eigenvalues[index_of_first_change] = eigenvalues[index_of_second_change];
			eigenvalues[index_of_second_change] = temp;
		}

		public void Sort(ref double[] eigenvalues, ref double[,] vectors)
		{
			for (int i = 0; i < eigenvalues.Length; i++)
			{
				for (int j = 0; j < eigenvalues.Length - 1; j++)
				{
					if (eigenvalues[j] < eigenvalues[j + 1])
					{
						Swap(ref eigenvalues, j, j + 1);
						Swap(ref vectors, j, j + 1);
					}
				}
			}
		}
		public double[] Singular_Values(double[] eignvalues)
		{
			for (int i = 0; i < eignvalues.Length; i++)
			{
				eignvalues[i] = Math.Round(Math.Sqrt(eignvalues[i]), 3);
			}
			return eignvalues;
		}
		public double[,] Sigma(double[,] origin_matrix, double[] eignvalues, int count_of_usable_rows)
		{
			double[] singular_values = Singular_Values(eignvalues);
			double[,] S = new double[count_of_usable_rows, count_of_usable_rows];

			int count_of_singular_values = 0;
			for (int i = 0; i < S.GetLength(0); i++)
			{
				for (int j = 0; j < S.GetLength(1); j++)
				{
					if (i == j)
					{
						S[i, j] = singular_values[count_of_singular_values];
						count_of_singular_values++;
					}
					if (count_of_singular_values == singular_values.Length)
						break;
				}
			}

			return S;
		}

		public double[,] V_transposed(double[,] origin_matrix, double[,] vectors, double[,] AtA, int count_of_usable_rows)
		{
			double[,] V_transposed = new double[count_of_usable_rows, origin_matrix.GetLength(1)];

			int count_of_eignvalues = 0;
			int i = 0;
			while (i < count_of_usable_rows)
			{
				for (int j = 0; j < V_transposed.GetLength(1); j++)
				{
					V_transposed[i, j] = vectors[j, i];
				}
				i++;
				count_of_eignvalues++;
			}

			return V_transposed;
		}

		public double[,] U_matrix(double[,] origin_matrix, double[,] vectors, double[,] AAt, int count_of_usable_rows)
		{
			double[,] u_matrix = new double[origin_matrix.GetLength(0), count_of_usable_rows];

			int count_of_eignvalues = 0;
			int i = 0;
			while (i < u_matrix.GetLength(0))
			{
				for (int j = 0; j < count_of_usable_rows; j++)
				{
					u_matrix[i, j] = vectors[i, j];
				}
				i++;
				count_of_eignvalues++;
			}



			return u_matrix;
		}
		public void Decomposition(double[,] matrix, out double[,] U, out double[,] S, out double[,] Vt, int count_of_usable_rows)
		{
			double[,] transposed_matrix = Matrix_Transpose(matrix);
			double[,] AtA = Matrix_multiplier(transposed_matrix, matrix); //матрица для S и Vt
			double[] eigenvalues = new double[AtA.GetLength(0)];
			double[,] vector = new double[AtA.GetLength(0), AtA.GetLength(1)];
			jacobi(AtA.GetLength(0), AtA, ref eigenvalues, ref vector);
			Sort(ref eigenvalues, ref vector);

			Vt = V_transposed(matrix, vector, AtA, count_of_usable_rows);
			S = Sigma(matrix, eigenvalues, count_of_usable_rows);


			double[,] AAt = Matrix_multiplier(matrix, transposed_matrix);
			double[] eigenvaluesAAt = new double[AAt.GetLength(0)];
			double[,] vectorAAt = new double[AAt.GetLength(0), AAt.GetLength(1)];
			jacobi(AAt.GetLength(0), AAt, ref eigenvaluesAAt, ref vectorAAt);
			Sort(ref eigenvaluesAAt, ref vectorAAt);

			U = U_matrix(matrix, vectorAAt, AAt, count_of_usable_rows);


		}
	}
}
