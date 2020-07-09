using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp5
{
    class Form2 : Form
    {
        public bool isChanged { get; set; } = false;
        TextBox textBox;
        ListBox listBox;
        public Form2( string[] mes )
        {
            Size = new Size( 500,500 );
            BackColor = Color.FromArgb( 200,200,200 );
            
            textBox = new TextBox()
            {
                Location = new Point(50, 50),
                Size = new Size( 350, 25 ),
            };
            
            listBox = new ListBox()
            {
                Location = new Point( 50, 100 ),
                Size = new Size( 400, 300 ),
            };

            Button button1 = new Button()
            {
                Location = new Point( 400, 50 ),
                Size = new Size( 50, 20 ),
                Text = "追加"
            };

            Button button2 = new Button()
            {
                Location = new Point( 50, 425 ),
                Size = new Size( 100, 20 ),
                Text = "削除",

            };

            Button button3 = new Button()
            {
                Location = new Point( 350, 425 ),
                Size = new Size(100, 20),
                Text = "適用 ",
            };

            foreach (string t in mes)
                listBox.Items.Add( t );

            button1.MouseClick += Button_MouseClick;
            button2.MouseClick += RemoveTextBoxItem;
            button3.MouseClick += AddFinish;

            Controls.Add( textBox );
            Controls.Add( button1 );
            Controls.Add( button2 );
            Controls.Add( button3 );
            Controls.Add( listBox );
        }

        public string[] GetTexts()
        {
            return listBox.Items.Cast<String>().ToArray();
        }

        private void AddFinish(object sender, MouseEventArgs e)
        {
            isChanged = true;
        }

        private void RemoveTextBoxItem(object sender, MouseEventArgs e)
        {
            if(listBox.SelectedIndex != -1)
                listBox.Items.RemoveAt( listBox.SelectedIndex );
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            if( textBox.Text != "" )
            {
                listBox.Items.Add( textBox.Text );
                textBox.Text = "";
            }
        }
    }
}
