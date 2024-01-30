using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Views;

public class GameTactics{
    public GameTactics() { }

    public ComboBox TacticalTraining()
    {
        ComboBox tacticalTraining = new ComboBox
        {
            Location = new Point(1350, 870),
            Width = 150,
            Height = 80,
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
    public ComboBox Style()
    {
        ComboBox style = new ComboBox
        {
            Location = new Point(1550, 870),
            Width = 150,
            Height = 80,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        style.Items.Add("Attack");
        style.Items.Add("Balanced");
        style.Items.Add("Defensive");
        style.SelectedIndex = 0;

        return style;
    }
    public ComboBox MarkingType()
    {
        ComboBox markingType = new ComboBox
        {
            Location = new Point(1350, 920),
            Width = 150,
            Height = 100,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        markingType.Items.Add("Light");
        markingType.Items.Add("Strong");
        markingType.Items.Add("Heavy");
        markingType.SelectedIndex = 0;

        return markingType;
    }
    public ComboBox Attack()
    {
        ComboBox attack = new ComboBox
        {
            Location = new Point(1550, 920),
            Width = 150,
            Height = 80,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        attack.Items.Add("Mixed");
        attack.Items.Add("Center");
        attack.Items.Add("Wings");
        attack.SelectedIndex = 0;

        return attack;
    }
}