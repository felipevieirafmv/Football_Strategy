using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Views;

public static class GameTactics{
    public static ComboBox TacticalTraining()
    {
        ComboBox tacticalTraining = new ComboBox
        {
            Location = new Point((int)(Screen.PrimaryScreen.Bounds.Width*0.703f), (int)(Screen.PrimaryScreen.Bounds.Height*0.805f)),
            Width = (int)(Screen.PrimaryScreen.Bounds.Width*0.078f),
            Height = (int)(Screen.PrimaryScreen.Bounds.Height*0.074f),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        tacticalTraining.Items.Add("4-3-3");
        tacticalTraining.Items.Add("4-2-2-2");
        tacticalTraining.Items.Add("4-4-2");
        tacticalTraining.Items.Add("4-2-4");
        tacticalTraining.Items.Add("3-4-3");
        tacticalTraining.SelectedIndex = 0;

        return tacticalTraining;
    }
    public static ComboBox Style()
    {
        ComboBox style = new ComboBox
        {
            Location = new Point((int)(Screen.PrimaryScreen.Bounds.Width*0.807f), (int)(Screen.PrimaryScreen.Bounds.Height*0.805f)),
            Width = (int)(Screen.PrimaryScreen.Bounds.Width*0.078f),
            Height = (int)(Screen.PrimaryScreen.Bounds.Height*0.074f),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        style.Items.Add("Attack");
        style.Items.Add("Balanced");
        style.Items.Add("Defensive");
        style.SelectedIndex = 0;

        return style;
    }
    public static ComboBox MarkingType()
    {
        ComboBox markingType = new ComboBox
        {
            Location = new Point((int)(Screen.PrimaryScreen.Bounds.Width*0.703f), (int)(Screen.PrimaryScreen.Bounds.Height*0.851f)),
            Width = (int)(Screen.PrimaryScreen.Bounds.Width*0.078f),
            Height = (int)(Screen.PrimaryScreen.Bounds.Height*0.074f),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        markingType.Items.Add("Light");
        markingType.Items.Add("Strong");
        markingType.Items.Add("Heavy");
        markingType.SelectedIndex = 0;

        return markingType;
    }
    public static ComboBox Attack()
    {
        ComboBox attack = new ComboBox
        {
            Location = new Point((int)(Screen.PrimaryScreen.Bounds.Width*0.807f), (int)(Screen.PrimaryScreen.Bounds.Height*0.851f)),
            Width = (int)(Screen.PrimaryScreen.Bounds.Width*0.078f),
            Height = (int)(Screen.PrimaryScreen.Bounds.Height*0.074f),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        attack.Items.Add("Mixed");
        attack.Items.Add("Center");
        attack.Items.Add("Wings");
        attack.SelectedIndex = 0;

        return attack;
    }
}