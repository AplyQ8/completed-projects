#include <iostream>
#include <string>
#include <queue>
#include <map>
#include <fstream>
#include <cstdlib>
#include <sstream>

using namespace std;
///��������� ������
/// frequency - ������� ��������� ��������
///left � right - ����� ��������� ������
struct Node
{
	char ch;
	int frequency;
	Node* left, * right;
};

///������� ���������� ����� � ������
Node* getNode(char ch, int frequency, Node* left, Node* right)
{
	Node* node = new Node();
	node->ch = ch;
	node->frequency = frequency;
	node->left = left;
	node->right = right;
	return node;
}

///���������� �������� ������� � ��������� � ��������� �����������
///�������� � ��������� ����������� ����� ���������� �������
struct order
{
	bool operator() (Node* l, Node* r)
	{
		return l->frequency > r->frequency;
	}
};

///������������ ��������� �������� ������ �������� �� �� �������
///���������� ����� �����
void CodeTheText(Node* root, string str, map <char, string> &Huffman)
{
	if (root == NULL)
		return;
	///������� ������ ������
	if (!root->left && !root->right)
	{
		Huffman[root->ch] = str;
	}

	CodeTheText(root->left, str + "0", Huffman);
	CodeTheText(root->right, str + "1", Huffman);
}

///����� ������ �������� � ������������� �������������� ������
void DecodeTheText(Node* root, int &index, string str,ofstream &out)
{
    if(out.is_open())
	{
            if (root == NULL)
            {
                return;
            }
            if (!root->left && !root->right)
            {
                out << root->ch;
                return;
            }

            index++;

            if (str[index] == '0')
                DecodeTheText(root->left, index, str,out);
            else
                DecodeTheText(root->right, index, str,out);
	}

}
string to_string(int C) ///�������������� ���������� int � string
{
    stringstream ss;
    ss << C;
    return ss.str();
}
int CountTheSizeOfText(string text,ofstream &out)
{
    int CountOfLetters=0;
    for(int i=0;i<text.length();i++)
        CountOfLetters+=8;
    if(out.is_open())
    {
        out<<'\n'<<"������ �� ������ ������: ";
        out<<CountOfLetters;
    }
    return CountOfLetters;
}

///���������� ������ �������� � � ������������� �������� ������
void buildHuffmanTree(string text,int C)
{
	///������� ������� ��������� ������� � ���������� �� � �����
	map<char, int> frequency;
	for (char ch: text)
	{
		frequency[ch]++;
	}

	///������� ������������ �������, ����� ������� ����� ������
	priority_queue<Node*, vector<Node*>, order>pq;
	///������� ������ ������ ��� ������� ��������� � ���������� �� � ������������ � �� ������������ ��������
	for (auto pair : frequency)
	{
		pq.push(getNode(pair.first, pair.second, nullptr, nullptr));

	}

	while (pq.size() != 1)
	{
		///������� ��� �������� � ��������� �����������
		///(� ������ ��������)
		Node* left = pq.top(); pq.pop();
		Node* right = pq.top(); pq.pop();
		///������� ����� ����, ��������� ������ �������� �����
		///��� ��� ���������
		///��� ����� ����� ����� ����� ����� �� ������
		int sum = left->frequency + right->frequency;
		pq.push(getNode('\0', sum, left, right));
	}

	///������� ��������� �� ������ ������ ��������
	Node* root = pq.top();

	///�����������
	///������� ������ �������� � ��������� �������������� �������� � �����
	map<char, string>Huffman;
	CodeTheText(root, "", Huffman);
	string str1="C:\\Users\\HYPERPC\\Desktop\\CodedFiles\\TheResult";
	string str2=".txt";
	string str0=to_string(C);
	string str3=str1+str0+str2;

    ofstream out;
    out.open(str3,ios::ate);
    if(out.is_open())
        {
            out << "Encoded elements are: " << '\n';

            for (auto pair : Huffman)
            {
                out << pair.first << " " << pair.second << '\n';
            }


            out << "\n The string was: " << text << endl;

            string str = "";
            for (char ch : text)
            {
                str += Huffman[ch];
            }

            out << "\nCoded string is: " << str << '\n';

            int CountCodedLetters = 0;
            for(int i=0;i<str.length();i++) ///������������ ������ ������ ������
                CountCodedLetters++;

            ///������� ������ �������� � ����������
            ///�������������� �������
            int index = -1;
            out << "\nDecoded string is: ";
            while (index < (int)str.size() - 2)
            {
                DecodeTheText(root, index, str, out);
            }

            double K=(double)CountTheSizeOfText(text,out)/(double)CountCodedLetters;
            out<<'\n'<<"������� ������ ������: "<<CountCodedLetters<<'\n';
            out<<'\n'<<"����������� ������: "<<K;
        }

        out.close();
}



void frontlist()

{

    cout<<"\t\t�������� ������\t"<<endl;

    cout<<" �� ���������� <��������� � ��������� ������������ ��������� ������> \t"<<endl;

    cout<<"�� ���� ��������� ���������, � ������� �������� ������ ������ \t"<<endl;

    cout<<" ������ ��������������� � ������������ ��������� � \t"<<endl;

    cout<<"\t���������������� �������� ������\n\n\n "<<endl;

    cout<<"����������: ������� ������ ��������������\t"<<endl;
    cout<<"2 ����, �������\t"<<endl;
    cout<<"������ ��4212"<<endl;
    system("pause");
    system("cls");

}
void menu()
{

    cout<<"1.���� �1"<<endl;
    cout<<"2.���� �2"<<endl;
    cout<<"3.���� �3"<<endl;
    cout<<"4.���� �4"<<endl;
    cout<<"5.���� �5"<<endl;
    cout<<"6.���� �6"<<endl;
    cout<<"7.���� �7"<<endl;
    cout<<"8.���� �8"<<endl;
    cout<<"9.����� �� ���������"<<endl;
}

int main()
{
    setlocale(LC_ALL,"rus");
    frontlist();
    int C;
    do
    {
        menu();
        cout<<"������� ����� ����� ��� ������: "<<endl;
        cin>>C;
        switch(C)
        {
        case 1:
            {
                system("cls");

                ifstream FileToRead("C:\\Users\\HYPERPC\\Desktop\\FilesToCode\\File1.txt");
                if(!FileToRead)
                {
                    cout<<"The file has not been found"<<endl;
                    return 1;
                }
                string text;
                getline(FileToRead,text);
                buildHuffmanTree(text,C);
                cout<<"Successfully!"<<endl;
                break;
            }
        case 2:
            {
                system("cls");
                ifstream FileToRead("C:\\Users\\HYPERPC\\Desktop\\FilesToCode\\File2.txt");
                if(!FileToRead)
                {
                    cout<<"The file has not been found"<<endl;
                    return 1;
                }
                string text;
                getline(FileToRead,text);
                buildHuffmanTree(text,C);
                cout<<"Successfully!"<<endl;
                break;
            }
        case 3:
            {
                system("cls");
                ifstream FileToRead("C:\\Users\\HYPERPC\\Desktop\\FilesToCode\\File3.txt");
                if(!FileToRead)
                {
                    cout<<"The file has not been found"<<endl;
                    return 1;
                }
                string text;
                getline(FileToRead,text);
                buildHuffmanTree(text,C);
                cout<<"Successfully!"<<endl;

                break;
            }
        case 4:
            {
                system("cls");
                ifstream FileToRead("C:\\Users\\HYPERPC\\Desktop\\FilesToCode\\File4.txt");
                if(!FileToRead)
                {
                    cout<<"The file has not been found"<<endl;
                    return 1;
                }
                string text;
                getline(FileToRead,text);
                buildHuffmanTree(text,C);
                cout<<"Successfully!"<<endl;

                break;
            }
        case 5:
            {
                system("cls");
                ifstream FileToRead("C:\\Users\\HYPERPC\\Desktop\\FilesToCode\\File5.txt");
                if(!FileToRead)
                {
                    cout<<"The file has not been found"<<endl;
                    return 1;
                }
                string text;
                getline(FileToRead,text);
                buildHuffmanTree(text,C);
                cout<<"Successfully!"<<endl;

                break;
            }
        case 6:
            {
                system("cls");
                ifstream FileToRead("C:\\Users\\HYPERPC\\Desktop\\FilesToCode\\File6.txt");
                if(!FileToRead)
                {
                    cout<<"The file has not been found"<<endl;
                    return 1;
                }
                string text;
                getline(FileToRead,text);
                buildHuffmanTree(text,C);
                cout<<"Successfully!"<<endl;

                break;
            }
        case 7:
            {
                system("cls");
                ifstream FileToRead("C:\\Users\\HYPERPC\\Desktop\\FilesToCode\\File7.txt");
                if(!FileToRead)
                {
                    cout<<"The file has not been found"<<endl;
                    return 1;
                }
                string text;
                getline(FileToRead,text);
                buildHuffmanTree(text,C);
                cout<<"Successfully!"<<endl;

                break;
            }
        case 8:
            {
                system("cls");
                ifstream FileToRead("C:\\Users\\HYPERPC\\Desktop\\FilesToCode\\File8.txt");
                if(!FileToRead)
                {
                    cout<<"The file has not been found"<<endl;
                    return 1;
                }
                string text;
                getline(FileToRead,text);
                buildHuffmanTree(text,C);
                cout<<"Successfully!"<<endl;

                break;
            }
        }

    }while(C!=9);
}
