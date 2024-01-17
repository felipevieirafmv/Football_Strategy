using System;
using System.Drawing;
using System.Windows.Forms;

public class Escalacao : Form
{

    Bitmap bmp = null;
    Graphics g = null;
    PointF cursor = PointF.Empty;
    PointF? grabStart = null;
    PointF? grabDesloc = null;
    bool isDown = false;

    int frame = 0;

    private PictureBox pb = new PictureBox{
        Dock = DockStyle.Fill,
    };

    

    public Escalacao() {


        Timer tm = new Timer();
        tm.Interval = 10;
        
        WindowState = FormWindowState.Maximized;
        // FormBorderStyle = FormBorderStyle.None;
        BackColor = Color.DarkGreen;

        ComboBox select_tatica = new ComboBox
        {
            Location = new Point(1450, 850),
            Width = 140,
            Height = 80,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        select_tatica.Items.Add("4-3-3");
        select_tatica.Items.Add("4-2-2-2");
        select_tatica.Items.Add("4-4-2");
        select_tatica.Items.Add("4-2-4");
        select_tatica.Items.Add("3-4-3");
        select_tatica.SelectedIndex = 0;


        
        ComboBox select_Estilo = new ComboBox
        {
            Location = new Point(1650, 850),
            Width = 140,
            Height = 80,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        select_Estilo.Items.Add("Ofesivo");
        select_Estilo.Items.Add("Equilibrado");
        select_Estilo.Items.Add(item: "Defensivo");
        select_Estilo.SelectedIndex = 0;
        
        
        ComboBox select_Marcacao = new ComboBox
        {
            Location = new Point(1450, 900),
            Width = 140,
            Height = 100,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        select_Marcacao.Items.Add("Leve");
        select_Marcacao.Items.Add("Forte");
        select_Marcacao.Items.Add("Pesada");
        select_Marcacao.SelectedIndex = 0;
        

        ComboBox select_Ataque = new ComboBox
        {
            Location = new Point(1650, 900),
            Width = 140,
            Height = 80,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        select_Ataque.Items.Add("Variado");
        select_Ataque.Items.Add("Pelo Centro");
        select_Ataque.Items.Add("Pelas Laterais");
        select_Ataque.SelectedIndex = 0;

        pb.MouseDown += (o, e) =>
        {
            isDown = true;
        };

        pb.MouseUp += (o, e) =>
        {
            isDown = false;
            grabDesloc = null;
            grabStart = null;
        };

        pb.MouseMove += (o, e) =>
        {
            cursor = e.Location;
        };

        Controls.Add(select_tatica);
        Controls.Add(select_Estilo);
        Controls.Add(select_Marcacao);
        Controls.Add(select_Ataque);
        Controls.Add(pb);

        float X = 200, Y = 10, widRect = 300, heiRect = 200;
        int X1 = 1400, Y1 = 100, widRect1 = 400, heiRect1 = 650;
        int X2 = 1200, Y2 = 0, widRect2 = 720, heiRect2 = 1080;
        




        RectangleF reservas = new RectangleF();
        reservas.Location = new PointF(1400, 100);
        reservas.Width = 400;
        reservas.Height = 50;

        RectangleF goleiro = new RectangleF();
        goleiro.Location = new PointF(522, 820);
        goleiro.Width = 86;
        goleiro.Height = 88;

        RectangleF Lateral_Direito = new RectangleF();
        Lateral_Direito.Location = new PointF(800, 640);
        Lateral_Direito.Width = 86;
        Lateral_Direito.Height = 88;
        
        RectangleF Zagueiro_1 = new RectangleF();
        Zagueiro_1.Location = new PointF(621, 680);
        Zagueiro_1.Width = 86;
        Zagueiro_1.Height = 88;

        RectangleF Zagueiro_2 = new RectangleF();
        Zagueiro_2.Location = new PointF(422, 680);
        Zagueiro_2.Width = 86;
        Zagueiro_2.Height = 88;

        RectangleF Lateral_Esquerdo = new RectangleF();
        Lateral_Esquerdo.Location = new PointF(254, 640);
        Lateral_Esquerdo.Width = 86;
        Lateral_Esquerdo.Height = 88;






        this.Load += delegate
        {
            bmp = new Bitmap(
                pb.Width, 
                pb.Height
            );
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;
            tm.Start();
            Menu_Selecao(X2, Y2, widRect2, heiRect2);
            DrawField(Bitmap.FromFile("./img/Campo.png"), X, Y, widRect, heiRect);
            Selecao(X1, Y1, widRect1, heiRect1);
            DrawPiece(reservas, "Murilo", 87);
            DrawEmptyPiece(reservas);
            DrawEmptyPiece(goleiro);
            DrawEmptyPiece(Lateral_Direito);
            DrawEmptyPiece(Zagueiro_1);
            DrawEmptyPiece(Zagueiro_2);
            DrawEmptyPiece(Lateral_Esquerdo);

            
            // DrawEmptyPiece(goleiro);
            // DrawEmptyPiece(goleiro);
            // DrawEmptyPiece(goleiro);
            // DrawEmptyPiece(goleiro);
            // DrawEmptyPiece(goleiro);
            // DrawEmptyPiece(goleiro);
            // DrawEmptyPiece(goleiro);

        };

        KeyDown += (o, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        };
 
        
        tm.Tick += delegate
        {
            frame++;
            OnFrame(isDown, cursor){
                
            }
 
            pb.Refresh();
        };

    }

    private void OnFrame(bool isDown, PointF cursor)
    {
        throw new NotImplementedException();
    }

    private void DrawField(Image image, float X, float Y, float widRect, float heiRect)
    {
        g.DrawImage(image, new RectangleF(X, Y , image.Width, image.Height));
    }

    private void Selecao(int X, int Y, int widRect, int heiRect)
    {
        g.DrawRectangle(Pens.White, X, Y, widRect, heiRect);
    }

    private void Menu_Selecao(int X, int Y, int widRect, int heiRect)
    {
        g.FillRectangle(Brushes.DarkBlue, X, Y, widRect, heiRect);
    }

    public void DrawText(string text, Color color, RectangleF location)
    {
        var format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
 
        var brush = new SolidBrush(color);
 
        g.DrawString(text, SystemFonts.MenuFont, brush, location, format);
    }

    public RectangleF DrawPiece(RectangleF location, string name, int overall)
    {

        float realWidth = location.Width;
        var realSize = new SizeF(location.Width, location.Height);
 
        var deslocX = grabDesloc?.X ?? 0;
        var deslocY = grabDesloc?.Y ?? 0;
        PointF position = new PointF(location.X + deslocX, location.Y + deslocY);
        RectangleF rect = new RectangleF(position, realSize);
 
        bool cursorIn = rect.Contains(cursor);
 
        if (!cursorIn && (deslocX != 0 || deslocY != 0))
            rect = new RectangleF(location.Location, realSize);
 
        var pen = new Pen(cursorIn ? Color.Green : Color.Black, 1f);
        var yellowPen = new Pen(Color.Black, 3f);
        var whitePen = new Pen(Color.Black, 2f);
 
        g.FillRectangle(Brushes.LightBlue, rect);
        g.DrawRectangle(pen, rect.X, rect.Y, realWidth, rect.Height);

 
        if (!cursorIn || !isDown)
            return rect;
         
        if (grabStart == null)
        {
            grabStart = cursor;
            return rect;
        }
 
        grabDesloc = new PointF(cursor.X - grabStart.Value.X, cursor.Y - grabStart.Value.Y);
 
        return rect;
    }

    public RectangleF DrawEmptyPiece(RectangleF location)
    {
        float realWidth = location.Width;
        var realSize = new SizeF(location.Width, location.Height);
 
        var position = new PointF(location.X, location.Y);
        RectangleF rect = new RectangleF(position, realSize);
 
        bool cursorIn = rect.Contains(cursor);
 
        var pen = new Pen(cursorIn ? Color.Cyan : Color.Black, 1);
 
        g.FillRectangle(Brushes.LightBlue, rect);
        g.DrawRectangle(pen, rect.X, rect.Y, realWidth, rect.Height);
 
 
        if (!cursorIn || !isDown)
            return rect;
         
        if (grabStart == null)
        {
            grabStart = cursor;
            return rect;
        }
 
        return rect;
    }

 
}

