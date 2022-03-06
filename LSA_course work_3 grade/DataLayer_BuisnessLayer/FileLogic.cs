using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_BuisnessLayer
{
    public class FileLogic
    {
        private List<string> text = new List<string>();

        public void GetFile(string path)
        {
            text.Clear();
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] array = streamReader.ReadLine().Split();
                    for (int i = 0; i < array.Length; i++)
                    {
                        text.Add(array[i]);
                    }
                }
            }
            CheckForMarks(text);
            ToLowerReg(text);
        }

        public List<string> GetText()
        {
            return text;
        }

        public void ShowFile()
        {
            foreach (string i in text)
            {
                Console.WriteLine(i);
            }
        }

        private string[] punctuationMarks = { ",", ".", ":", ";", "!", "?", "#", "*", "{", "}", "(", ")" };
        private int foundS1 = -1;
        private void CheckForMarks(List<string> text)
        {
            for (int j = 0; j < text.Count; j++)
            {
                for (int i = 0; i < punctuationMarks.Length; i++)
                {
                    foundS1 = text[j].IndexOf(punctuationMarks[i]);
                    if (foundS1 != -1)
                    {
                        text[j] = text[j].Remove(foundS1, 1);
                    }
                }
            }
        }

        private void ToLowerReg(List<string> text)
        {
            for (int i = 0; i < text.Count; i++)
            {
                text[i] = text[i].ToLower();
            }
        }
    }
}
