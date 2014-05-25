using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using _2048Game;
using _2048G;

namespace _2048G
{
    public partial class Game2048 : Form
    {
        public Game2048()
        {
            InitializeComponent();
        }
        public Game engine = new Game();
        public Scores score =new Scores();
        private void Game2048_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Visible = false;
            engine.pics[0] = Image.FromFile("images\\0.jpg");
            engine.pics[1] = Image.FromFile("images\\2.jpg");
            engine.pics[2] = Image.FromFile("images\\4.jpg");
            engine.pics[3] = Image.FromFile("images\\8.jpg");
            engine.pics[4] = Image.FromFile("images\\16.jpg");
            engine.pics[5] = Image.FromFile("images\\32.jpg");
            engine.pics[6] = Image.FromFile("images\\64.jpg");
            engine.pics[7] = Image.FromFile("images\\128.jpg");
            engine.pics[8] = Image.FromFile("images\\256.jpg");
            engine.pics[9] = Image.FromFile("images\\512.jpg");
            engine.pics[10]= Image.FromFile("images\\1024.jpg");
            engine.init();
            engine.IOinint();
            for (int i = 0; i < 16; i++)
            {
                 engine.PictureBox[i] = new PictureBox();
                Controls.Add(engine.PictureBox[i]);
                engine.PictureBox[i].Size = new Size(70, 70);
                engine.PictureBox[i].TabStop = false;
                engine.PictureBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                engine.PictureBox[i].BorderStyle = BorderStyle.None;
                engine.PictureBox[i].Image = Image.FromFile("images\\0.jpg");
            }
            for (int i = 0; i < 4;i++)
            {
                engine.PictureBoxMask[i] = new PictureBox();
                Controls.Add(engine.PictureBoxMask[i]);
                engine.PictureBoxMask[i].Size = new Size(70, 70);
                engine.PictureBoxMask[i].TabStop = false;
                engine.PictureBoxMask[i].SizeMode = PictureBoxSizeMode.StretchImage;
                engine.PictureBoxMask[i].BorderStyle = BorderStyle.None;
            }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        engine.PictureBox[i * 4 + j].Location = new Point(i * 73 + 15, j * 73 + 30);
                    }
                }
        }
        private void Game2048_KeyUp(object sender, KeyEventArgs e)
        {
            Show();
            if (engine.lose == false)
            {
                if (e.KeyData == Keys.Down)
                {
                    engine.ShiftDown();
                }
                if (e.KeyData == Keys.Up)
                {
                }
                if (e.KeyData == Keys.Right)
                {
                }
                if (e.KeyData == Keys.Left)
                {
                }
                toolStripStatusLabel1.Text = "Score : " + (engine.score * 2).ToString();
            }
            else
            {
                getlose();
            }
        }

        void getlose()
        {
            lblreset.Visible = true;
            lblexit.Visible = true;
            lbllose.Visible = true;
            lblendscore.Visible = true;
            lblendscore.Text = "Your Score : " + (engine.score*2).ToString();
            score.ExcludeScore();
            if(score.scores[0]<engine.score*2||
               score.scores[1] < engine.score * 2||
                score.scores[2] < engine.score * 2)
            {
                toolStripStatusLabel1.Text = "After type name press enter to continue.";
                textBox1.Visible = true;
                textBox1.Enabled = true;
            }
        }

        void reset_engine()
        {
            lblreset.Visible = false;
            lbllose.Visible = false;
            lblexit.Visible = false;
            lblendscore.Visible = false;
            toolStripStatusLabel1.Text = "Score : " + (engine.score * 2).ToString();
            textBox1.Enabled = false;
            textBox1.Visible = false;
            engine.lose = false;
            engine.score = 0;
            engine.reset();
            engine.show();
        }

        private void lblexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblreset_Click(object sender, EventArgs e)
        {
            reset_engine();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reset_engine();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                score.InsertScore(engine.score*2, textBox1.Text);
                textBox1.Enabled = false;
                textBox1.Visible = false;
                toolStripStatusLabel1.Text = "Your Score saved succesfully.";
            }
        }

        private void topScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            score.ExcludeScore();
            MessageBox.Show("Scoreboard :\n"+
               "1-" + score.names[0] + "   :   " + score.scores[0].ToString() + "\n" +
               "2-" + score.names[1] + "   :   " + score.scores[1].ToString() + "\n" +
               "3-" + score.names[2] + "   :   " + score.scores[2].ToString()
                );
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (engine.SaveGame())
                toolStripStatusLabel1.Text = "Save succesfully.";
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (engine.LoadGame())
                toolStripStatusLabel1.Text = "Load succesfully.";
        }

        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (engine.LoadGame())
                toolStripStatusLabel1.Text = "Load succesfully.";
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (engine.SaveGame())
                toolStripStatusLabel1.Text = "Save succesfully.";
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2048 Game created by Masoud Rezekhanlo.\n"
                +"Powered by VC# 2013\n"+"Framework Version: 2.0\n"
                +"2048 V 1.0.0","2048 About");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }
}

