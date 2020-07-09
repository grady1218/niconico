using System;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;
using ConsoleApp5;

namespace ConsoleApp5
{
    class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
			Application.Run( new Form1() );
        }
	}
}

class Form1 : Form
{
	DrawMessage message;
	Form2 form2;
	public Form1()
	{
		DoubleBuffered = true;
		TopMost = true;
		ShowInTaskbar = false;
		TransparencyKey = BackColor;
		ControlBox = false;
		FormBorderStyle = FormBorderStyle.None;
		WindowState = FormWindowState.Maximized;

		message = new DrawMessage( Screen.GetBounds(this).Width, Screen.GetBounds(this).Height );

		Button button = new Button()
		{
			Text = "Settings",
			Size = new Size( 100,20 ),
			Location = new Point( 0,50 ),
			BackColor = Color.White
		};

		Controls.Add( button );

		Timer t = new Timer()
		{
			Interval = 13,
			Enabled = true

		};

		t.Tick += UpDate;
		Paint += MesDraw;
        button.MouseClick += Settings;
	}

    private void Settings(object sender, MouseEventArgs e)
    {
		if( form2 == null || form2.IsDisposed)
        {
			form2 = new Form2( message.mes );
			form2.Show();
        }
		Console.WriteLine( "push" );
    }

    private void MesDraw(object sender, PaintEventArgs e)
	{
		message.Draw( e );
	}

	private void UpDate(object sender, EventArgs e)
	{
		if (form2 != null && form2.isChanged)
		{
			message.mes = form2.GetTexts();
			message.RandomGenerate();
			form2.isChanged = false;
		}
		Invalidate();
	}

	[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
	static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	[System.Runtime.InteropServices.DllImport("user32.dll")]
	static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

	private void MostBack()
	{
		IntPtr WinHandle = FindWindow(null, "Program Manager");
		if (WinHandle != null)
		{
			SetParent(this.Handle, WinHandle);
		}
	}
}

class DrawMessage
{
    readonly Brush[] b = {
		Brushes.Blue,
		Brushes.Black,
		Brushes.Red,
		Brushes.Yellow,
		Brushes.Green,
		Brushes.White
	};

	public string[] mes { get; set; }
	int[] selectNumber;
	int width;
	int height;
	int[] colorNum;
	float[] x;
	float[] y;

	int ex;
	int ey;
	int vex = 5;
	int vey = 5;
	public DrawMessage( int w, int h )
	{
		width = w;
		height = h;
	
		selectNumber = new int[10];
		colorNum = new int[selectNumber.Length];
		x = new float[selectNumber.Length];
		y = new float[selectNumber.Length];
		mes = new string[] { "帰りたい", "眠いいいいいいいいいいいいいいい" };

		RandomGenerate();
		Start();
	}

	public void Draw( PaintEventArgs e )
	{
		BallDraw( e );
		if (mes.Length == 0) return;
		for (int count = 0; count < x.Length; count++)
		{
			if (x[count] >= -100)
			{
				//Console.WriteLine( x );
				x[count] -= 5;
				e.Graphics.DrawString(mes[selectNumber[count]], new Font("TB古印体", 30), b[colorNum[count]], x[count], y[count]);
			}
			else
			{
				x[count] = width;
				RandomGenerate( count );
			}
		}
	}

	private void RandomGenerate( int count )
	{
		Random r = new Random();
		selectNumber[count] = r.Next(mes.Length);
		y[count] = r.Next(height);
	}

	public void RandomGenerate()
	{
		Random r = new Random();
		for (int count = 0; count < x.Length; count++)
		{
			selectNumber[count] = r.Next(mes.Length);
			y[count] = r.Next(height);
			colorNum[count] = r.Next( b.Length );
		}
	}

	private void BallDraw( PaintEventArgs e )
    {
		ex += vex;
		ey += vey;
		if (ex > width - 50 || ex < 0) vex *= -1;
		if (ey > height - 50 || ey < 0) vey *= -1;
		e.Graphics.FillEllipse( Brushes.Green, ex, ey, 50, 50 );
	}

	private void Start()
	{
		Random r = new Random();
		for (int count = 0; count < x.Length; count++)
		{
			x[count] = r.Next(width);
			colorNum[count] = r.Next( b.Length );
		}
		ex = r.Next( width );
		ey = r.Next( height );


	}
}