using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

public class Escalacao : Form
{

    Bitmap bmp = null;
    Graphics g = null;
    PointF cursor = PointF.Empty;
    PointF? grabStart = null;
    PointF? grabDesloc = null;
    bool isDown = false;

    private PictureBox pb = new PictureBox{
        Dock = DockStyle.Fill,
    };

    public Escalacao() {
        
        WindowState = FormWindowState.Maximized;
        // FormBorderStyle = FormBorderStyle.None;
        BackColor = Color.DarkGreen;

        ComboBox select_tatica = new ComboBox
        {
            Location = new Point(1400, 750),
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
            Location = new Point(1600, 750),
            Width = 140,
            Height = 80,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        select_Estilo.Items.Add("Ofesivo");
        select_Estilo.Items.Add("Equilibrado");
        select_Estilo.Items.Add("Defensivo");
        select_Estilo.SelectedIndex = 0;
        
        
        ComboBox select_Marcacao = new ComboBox
        {
            Location = new Point(1400, 800),
            Width = 140,
            Height = 80,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        select_Marcacao.Items.Add("Leve");
        select_Marcacao.Items.Add("Forte");
        select_Marcacao.Items.Add("Pesada");
        select_Marcacao.SelectedIndex = 0;
        

        ComboBox select_Ataque = new ComboBox
        {
            Location = new Point(1600, 800),
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

        this.Load += delegate
        {
            bmp = new Bitmap(
                pb.Width, 
                pb.Height
            );
            g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            pb.Image = bmp;
        };

    }
}

