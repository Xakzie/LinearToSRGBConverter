using System;
using System.Drawing;
using System.Windows.Forms;

namespace LinearToSRGBConverter
{
    public partial class Form1 : Form
    {
        TextBox textBoxR, textBoxG, textBoxB, textBoxRNew, textBoxGNew, textBoxBNew;
        Label labelArrowR, labelArrowG, labelArrowB, labelCredit;
        Panel colorPanel;

        public Form1()
        {
            Text = "Linear → sRGB Converter";
            Width = 280;
            Height = 200;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            Label labelR = new Label() { Text = "Linear R:", Top = 15, Left = 10, Width = 55 };
            textBoxR = new TextBox() { Top = 10, Left = 65, Width = 80 };
			labelArrowR = new Label() {Text = "->", Top = 15, Left = 150, Width = 20};
			textBoxRNew = new TextBox() { Top = 10, Left = 170, Width = 80, ReadOnly = true};

            Label labelG = new Label() { Text = "Linear G:", Top = 45, Left = 10, Width = 55 };
            textBoxG = new TextBox() { Top = 40, Left = 65, Width = 80 };
			labelArrowG = new Label() {Text = "->", Top = 45, Left = 150, Width = 20};
			textBoxGNew = new TextBox() { Top = 40, Left = 170, Width = 80, ReadOnly = true};

            Label labelB = new Label() { Text = "Linear B:", Top = 75, Left = 10, Width = 55 };
            textBoxB = new TextBox() { Top = 70, Left = 65, Width = 80 };
			labelArrowB = new Label() {Text = "->", Top = 75, Left = 150, Width = 20};
			textBoxBNew = new TextBox() { Top = 70, Left = 170, Width = 80, ReadOnly = true};

            Button convertButton = new Button() { Text = "Convert", Top = 100, Left = 50, Width = 80, Height = 40 };
            convertButton.Click += ConvertButton_Click;
			
			labelCredit = new Label() {Text = "Made By Xakzi, v1.01", Top = 145, Width = 200};

            colorPanel = new Panel()
            {
                Top = 100,
                Left = 150,
                Width = 80,
                Height = 40,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Controls.Add(labelR);
            Controls.Add(textBoxR);
            Controls.Add(labelG);
            Controls.Add(textBoxG);
            Controls.Add(labelB);
            Controls.Add(textBoxB);
            Controls.Add(convertButton);
            Controls.Add(colorPanel);
			Controls.Add(labelArrowR);
			Controls.Add(labelArrowG);
			Controls.Add(labelArrowB);
			Controls.Add(textBoxRNew);
			Controls.Add(textBoxGNew);
			Controls.Add(textBoxBNew);
			Controls.Add(labelCredit);
			
			// Lägg till fokus-/klickhändelser för alla textfält
            AddFocusHandlers(textBoxR);
            AddFocusHandlers(textBoxG);
            AddFocusHandlers(textBoxB);
            AddFocusHandlers(textBoxRNew);
            AddFocusHandlers(textBoxGNew);
            AddFocusHandlers(textBoxBNew);
        }
		
		// 🔹 Lägg till både Enter + MouseUp-event för att markera text
        private void AddFocusHandlers(TextBox tb)
        {
            tb.Enter += TextBox_Focus;
            tb.MouseUp += TextBox_Focus;
        }

        // 🔹 Markera text vid fokus eller klick
        private void TextBox_Focus(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                // Fördröjningen säkerställer att markeringen inte tas bort direkt efter klick
                tb.BeginInvoke((Action)(() => tb.SelectAll()));
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            try
            {
                double rLinear = double.Parse(textBoxR.Text) * 255.0 / 100.0;
				double gLinear = double.Parse(textBoxG.Text) * 255.0 / 100.0;
				double bLinear = double.Parse(textBoxB.Text) * 255.0 / 100.0;

                // Linear -> sRGB konvertering
                double rSRGB = Math.Pow(rLinear / 255.0, 1.0 / 2.2) * 255.0;
                double gSRGB = Math.Pow(gLinear / 255.0, 1.0 / 2.2) * 255.0;
                double bSRGB = Math.Pow(bLinear / 255.0, 1.0 / 2.2) * 255.0;

                int r = (int)Math.Round(rSRGB);
                int g = (int)Math.Round(gSRGB);
                int b = (int)Math.Round(bSRGB);
				
				textBoxRNew.Text = $"{r}";
				textBoxGNew.Text = $"{g}";
				textBoxBNew.Text = $"{b}";
                colorPanel.BackColor = Color.FromArgb(r, g, b);
            }
            catch
            {
                MessageBox.Show("Please enter valid numbers for R, G and B.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
