using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataLayer_BuisnessLayer
{
    public class Matrix_compression
    {
        public int row, col;
        public double[,] matrix = new double[0, 0];
        public List<string> colName;
        public Matrix_compression(int Row, int Col)
        {
            matrix = new double[Row, Col];
            row = Row;
            col = Col;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            colName = new List<string>();
        }
        public Matrix_compression(double[,] Matr)
        {
            matrix = Matr;
            row = Matr.GetLength(0);
            col = Matr.GetLength(1);
            colName = new List<string>();
        }
        public Matrix_compression(double[,] Matr, List<string> ColName)
        {
            matrix = Matr;
            row = Matr.GetLength(0);
            col = Matr.GetLength(1);
            colName = ColName;
        }
        public Matrix_compression(int Row, int Col, List<string> ColName)
        {
            //excep на Row, Col <0
            matrix = new double[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            row = Row;
            col = Col;
            colName = ColName;
            //excep на list.count != Row
        }
        public double this[int Row, int Col]
        {
            get
            {
                return this.matrix[Row, Col];
            }

            set
            {
                this.matrix[Row, Col] = value;
            }
        }
        public static Matrix_compression MultiMatrix(Matrix_compression firstMatrix, Matrix_compression secondMatrix)
        {
            //excep на firstMatrix.col != secondMatrix.row
            var resMatrix = new Matrix_compression(firstMatrix.row, secondMatrix.col);
            for (int i = 0; i < firstMatrix.row; i++)
            {
                for (int j = 0; j < secondMatrix.col; j++)
                {
                    for (int k = 0; k < firstMatrix.col; k++)
                    {
                        resMatrix[i, j] += firstMatrix.matrix[i, k] * secondMatrix.matrix[k, j];
                    }
                }
            }
            return resMatrix;
        }
        public static Matrix_compression MultiForDist(Matrix_compression first, Matrix_compression second)
        {
            var res = new Matrix_compression(first.row, second.col);
            for (int i = 0; i < res.row; i++)
            {
                for (int k = 0; k < res.col; k++)
                {
                    for (int j = 0; j < first.col; j++)
                    {
                        res[i, k] += (first[i, j] - second[j, k]) * (first[i, j] - second[j, k]);
                    }
                }
            }

            return res;
        }
      
    }
    public class Connections
    {
        public static List<Tuple<int, string>> Connects(Matrix_compression wordMatrix, 
                                                        Matrix_compression normalizedMatrix, 
                                                        Matrix_compression txtMatrix)
        {

            var resMatrix = new Matrix_compression(wordMatrix.row, 
                                                   txtMatrix.col, 
                                                   wordMatrix.colName);
            for (int i = 0; i < wordMatrix.row; i++)
            {
                for (int k = 0; k < txtMatrix.col; k++)
                {
                    resMatrix[i, k] = 0;
                }
            }
            
            resMatrix = Matrix_compression.MultiForDist(wordMatrix, txtMatrix);
            for (int i = 0; i < resMatrix.row; i++)
            {
                for (int k = 0; k < resMatrix.col; k++)
                {
                    resMatrix[i, k] = Math.Round(resMatrix[i, k], 6);
                }
            }
            
            var resTuple = new List<Tuple<int, string>>();
            
            Tuple<int, string> tmp;

            for (int i = 0; i < resMatrix.col; i++)
            {
                int id = 0;
                for (int k = 1; k < resMatrix.row; k++)
                {
                    if (resMatrix[id, i] < resMatrix[k, i])
                    {
                        id = k;
                    }
                }
                tmp = new Tuple<int, string>(i + 1, wordMatrix.colName[id]);
                resTuple.Add(tmp);
            }
            //for (int i = 0; i < resMatrix.col; i++)
            //{
            //    int id = 0;
            //    for (int k = 1; k < resMatrix.row; k++)
            //    {
            //        if (resMatrix[id, i] > resMatrix[k, i])
            //        {
            //            id = k;
            //        }
            //    }
            //    tmp = new Tuple<int, string>(i + 1, wordMatrix.colName[id]);
            //    resTuple.Add(tmp);
            //}

            return resTuple;
        }


        public static List<Tuple<int, string>> CreateResult(double[,] U, 
                                                            double[,] S, 
                                                            double[,] Vt, 
                                                            List<string> word_List)
        {
            var wordMatrix = new Matrix_compression(U, word_List);
            var normMatrix = new Matrix_compression(S);
            var txtMatrix = new Matrix_compression(Vt);



            return Connections.Connects(wordMatrix, normMatrix, txtMatrix); ;
        }

        private static MySqlCommand command = new MySqlCommand();
        private static string sql = "";
        public static void Print(List<Tuple<int, string>> Base)
        {
            DataBase data = new DataBase();
            data.openConnection();

            foreach (Tuple<int, string> i in Base)
            {
                sql= "SELECT field FROM library WHERE word = '" + i.Item2+"'";
                command.Connection = data.getConection();
                command.CommandText = sql;
                string dicField = command.ExecuteScalar().ToString();
                Console.WriteLine("Текст "+i.Item1 + " " +i.Item2+" "+dicField );
            }

            data.closeConnection();
        }
    }
}
