using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.ComponentModel;
using System.IO;
using _2048G;
using _2048Game;

namespace _2048Game
{
    public class Scores
    {
        public string CODE = "2048 Created by Masoud Rezakhanlo V1.0.0";
        public int[] scores = new int[3];
        public string[] names = new string[3];
        public void ExcludeScore()
        {
            StreamReader reader = new StreamReader("scores\\Scores.tscr");
            for(int i=0;i<3;i++)
            {
                names[i] =reader.ReadLine();
            }
            for(int i=0 ;i<3;i++)
            {
                scores[i] = Convert.ToInt32(reader.ReadLine().ToString());
            }
            reader.Close();
        }
        public void InsertScore(int newscore,string newname)
        {
            ExcludeScore();
            bool flag = true;
            StreamWriter writer = new StreamWriter("scores\\scores.tscr");
            
            if (newscore > scores[0])
            {
                writer.WriteLine(newname);
                writer.WriteLine(names[1]);
                writer.WriteLine(names[2]);
                writer.WriteLine(newscore);
                writer.WriteLine(scores[1]);
                writer.WriteLine(scores[2]);
                flag = true;
            }
            if (flag==false||newscore > scores[1])
            {
                writer.WriteLine(names[0]);
                writer.WriteLine(newname);
                writer.WriteLine(names[2]);
                writer.WriteLine(scores[0]);
                writer.WriteLine(newscore);
                writer.WriteLine(scores[2]);
                flag = true;
            }
            if(flag==false||newscore>scores[2])
            {
                writer.WriteLine(names[0]);
                writer.WriteLine(names[1]);
                writer.WriteLine(newname);
                writer.WriteLine(scores[0]);
                writer.WriteLine(scores[1]);
                writer.WriteLine(newscore);

            }
            writer.Close();
        }
    }
    public class cordinate
    {
        public int[] x = new int[16];
        public int[] y = new int[16];
    }
    public class Game : cordinate
    {
        
        public string CODE = "2048 Created by Masoud Rezakhanlo V1.0.0";
        public Image[] pics = new Image[12];
        public PictureBox[] PictureBox = new PictureBox[16];
        public PictureBox[] PictureBoxMask = new PictureBox[4];
        public Timer timer1=new Timer();
        public int [][] table = new int[4][];
        public int score = 0;
        public bool lose = false;
        public bool IslockRight = true;
        public bool IslockLeft = true;
        public bool IslockUp = true;
        public bool IslockDown = true;

        public OpenFileDialog openfile = new OpenFileDialog();
        public SaveFileDialog savefile = new SaveFileDialog();

        public void IOinint()
        {
            openfile.Filter = "2048 Game files (*.2048)|*.2048|All files (*.*)|*.*" ;
            savefile.Filter = "2048 Game files (*.2048)|*.2048";
        }
        public bool SaveGame()
        {
            savefile.ShowDialog();
            string address = savefile.FileName.ToString();
            if (address != null)
            {
                StreamWriter writer = new StreamWriter(address);
                writer.WriteLine(CODE);
                writer.WriteLine(score/2);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        writer.WriteLine(table[i][j]);
                    }
                }
                writer.Close();
            }
            return (true);
        }
        public bool LoadGame()
        {
            openfile.ShowDialog();
            string address = openfile.FileName.ToString();
            if (address != null)
            {
                string code;
                StreamReader reader = new StreamReader(address);
                code=reader.ReadLine().ToString();
                
                if(code==CODE)
                {
                    score = Convert.ToInt32(reader.ReadLine().ToString());
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            table[i][j] = Convert.ToInt32(reader.ReadLine());
                        }
                    }
                }
                
                reader.Close();
            }
            show();
            return (true);
        }
        public int pow2(int x)
        {
            for (int pow = 0; pow < 14; pow++)
                if (Convert.ToInt32(Math.Pow(2, pow)) == x)
                    return (pow);
            return (0);
        }

        public void reset()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    table[i][j] = 0;
                }
            }
        }
        public void show()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[j][i] != 0)
                    {
                        int ins;
                        ins = pow2(table[j][i]);
                        
                        PictureBox[i * 4 + j].Image = pics[ins];
                    }
                    else
                    {
                        PictureBox[i * 4 + j].Image = pics[0];
                    }
                }
            }
        }

        public void init()
        {
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                table[i] = new int[4];
                table[i] = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    table[i][j] = 0;
                }
            }

            table[rnd.Next(0, 3)][rnd.Next(0, 3)] = 2;
            table[rnd.Next(0, 3)][rnd.Next(0, 3)] = 2;
        }

        public void AddValue(bool arg)
        {
            if (!arg)
            {
                cordinate P = new cordinate();
                int avalible = 0;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (table[i][j] == 0)
                        {
                            P.x[avalible] = i;
                            P.y[avalible] = j;
                            avalible++;
                        }
                    }
                }

                if (avalible != 0)
                {
                    Random rnd = new Random();
                    int choose = rnd.Next(0, 10);
                    int np = rnd.Next(0, avalible);

                    if (choose < 2)
                        table[P.x[np]][P.y[np]] = 2;
                    else
                        table[P.x[np]][P.y[np]] = 4;
                }
                else
                {
                    bool flag = false;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (table[i][j] == table[i][j + 1] ||
                                table[j][i] == table[j + 1][i])
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                            break;
                    }

                    if (!flag)
                    {
                        lose = true;
                    }
                }
            }
        }

        //-----------------------------------------------------------------------------------
        public void islockdown()
        {
            IslockDown = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j >= 0; j--)
                {
                    if (table[j + 1][i] == 0 && table[j][i] != 0)
                        IslockDown = false;
                    if (table[j + 1][i] == table[j][i] && table[j + 1][i] != 0)
                        IslockDown = false;
                }
            }
        }
        
        public void ShiftDown()
        {
        }

        private void DownRenderer(int from,int to)
        {
            
        }
    }
}

