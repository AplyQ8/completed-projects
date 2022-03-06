using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer_BuisnessLayer;
using System.IO;

namespace CommandMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase dataBase = new DataBase();
            FileLogic fileLogic = new FileLogic();
            FrequencyAndSentimentAnalys frequency = new FrequencyAndSentimentAnalys();

            string[] array = Directory.GetFiles(@"texts_for_analysis");

            for (int i = 0; i < array.Length; i++)
            {
                fileLogic.GetFile(array[i]);
                frequency.GetSentimantAnalys(fileLogic.GetText(), i);
                frequency.GetFrequency(fileLogic.GetText(), i);
                
            }

            Singularity svd = new Singularity();
            svd.Decomposition(frequency.MakeFreqMatrix(), out double[,] U, out double[,] S, out double[,] Vt, 2);

            frequency.ShowFreq();

            var restuple = DataLayer_BuisnessLayer.Connections.CreateResult(U, S, Vt, frequency.Word_List());

            DataLayer_BuisnessLayer.Connections.Print(restuple);
            frequency.ShowSentAnalys();
            Console.ReadKey();
        }
    }
}
