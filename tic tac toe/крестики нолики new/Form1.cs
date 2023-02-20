using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace крестики_нолики_new
{
    public partial class Form1 : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private int player;
        private bool mode;
        public Form1()
        {
            InitializeComponent();
            var dlg = new Form2();
            dlg.ShowDialog();
            this.mode = dlg.mode;

            player = 1;
            if (mode) //минус лейбл на одного игрока
            {
                label2.Text = "";
            }
            else
            {
                label2.Text = "Текущий ход: Игрок 1";
            }
            
            
            for (int i = 0; i < buttons.Length / 3; i++)
            {
                for (int j = 0; j < buttons.Length / 3; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(200, 200);
                    buttons[i, j].MouseEnter += Button_MouseEnter;
                    buttons[i, j].MouseLeave += Button_MouseLeave;
                }
            }
            setButtons();

        }

        private void setButtons()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j].Location = new Point(12 + 206 * i, 12 + 206 * j);
                    buttons[i, j].Click += b1_Click;


                    this.Controls.Add(buttons[i, j]);//создание поля

                }

            }
        }

        private void b1_Click(object sender, EventArgs e) //смена имени игрока 
        {
            if (!mode)
            {

                switch (player)
                {
                    case 1:
                        sender.GetType().GetProperty("Text").SetValue(sender, "X");
                        player = 0;
                        label2.Text = "Текущий ход: Игрок 2";
                        break;
                    case 0:
                        sender.GetType().GetProperty("Text").SetValue(sender, "O");
                        player = 1;
                        label2.Text = "Текущий ход: Игрок 1";
                        break;
                }
                sender.GetType().GetProperty("Enabled").SetValue(sender, false);
                CheckWin();
            }

            else 
            {
                sender.GetType().GetProperty("Text").SetValue(sender, "X");
                sender.GetType().GetProperty("Enabled").SetValue(sender, false);
                       
                 MoveBot();
                //label2.Text = " ";
                CheckWin();   
            }
            
        }

        private void MoveBot() //пупупупупу заварю ка кофейку
        {
            Random rnd = new Random();
            while (true)
            {
                int i = rnd.Next(3);
                int j = rnd.Next(3);
                if (buttons[i, j].Enabled)
                {
                    buttons[i, j].Text = "O";
                    buttons[i, j].Enabled = false;
                    return;
                }
            }
        }

        private bool CheckWin() //выигрышные комбинации
        {
            bool isWin = false; // проверка на возможность выигрышных
            //Строки
            if(!isWin)
            for (int i = 0; i < 3 && !isWin; i++)
            {
                isWin = F(i, 0, i, 1, i, 2);
            }
            if (!isWin)
            {
                //Столбики
                for (int i = 0; i < 3 && !isWin; i++)
                    isWin = F(0, i, 1, i, 2, i);
            }
            if (!isWin)
            {
                //Глав диагональ
                isWin = F(0, 0, 1, 1, 2, 2);
            }
            if (!isWin)
            {
                //Побочная Диагональ
                isWin = F(0, 2, 1, 1, 2, 0);
            }
            return isWin;
        }

        private bool F(int a1, int a2, int b1, int b2, int c1, int c2)
        {
            if (buttons[a1, a2].Text == buttons[b1, b2].Text && buttons[b1, b2].Text == buttons[c1, c2].Text)
            {
                if (buttons[a1, a2].Text != "")
                {
                    MessageBox.Show("Вы победили!");
                    return true;
                }
                
            }
            else if(IsFilled())
            {
                MessageBox.Show("Ничья!");
                return true;
            }
            return false;
        }

        public bool IsFilled() //проверка на пустые клетки
        {
            
            for(int i = 0; i<3; i++)
                for(int j = 0; j<3; j++)
                {
                    if (buttons[i, j].Text == "")
                        return false;
                }
            return true;
        }

        private void b11_Click(object sender, EventArgs e)  //рестарт
        {
            var dlg = new Form2();
            dlg.ShowDialog();
            this.mode = dlg.mode;
            player = 1;
            label1.Text = "Текущий ход: Игрок 1";

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j].Text = "";
                    buttons[i, j].Enabled = true;
                }
            }

        }

        //Когда на кнопку наводится курсор
        public void Button_MouseEnter(object sender, EventArgs e)
        {
            Button enterButton = (Button)sender;
            if (player == 1)
                enterButton.Text = "X";
            else
                enterButton.Text = "O";
        }

        //Когда с кнопки убирается курсор
        public void Button_MouseLeave(object sender, EventArgs e)
        {
            Button enterButton = (Button)sender;
            if (enterButton.Enabled == true)
                enterButton.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
