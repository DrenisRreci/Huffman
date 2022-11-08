using System;
using System.Collections;
using System.Windows.Forms;

namespace Huffman
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pieChart.Titles.Add("Ersparnis");
        }
        HuffmanBaum meinBaum = new HuffmanBaum();

        private void btStart_Click(object sender, EventArgs e)
        {
            

            if(String.IsNullOrEmpty(tbInput.Text))
            {
                MessageBox.Show("Geben Sie eine Zeichenkette an!");
                return;
            }
            tbOutput.Clear();
            meinBaum.ErstelleBaum(tbInput.Text);

            BitArray encoded = meinBaum.Encode(tbInput.Text);
            tbOutput.AppendText("Encoded message: ");
            foreach(bool bit in encoded)
            {
                tbOutput.AppendText(bit ? "1" : "0");
            }
            tbOutput.AppendText(Environment.NewLine + "Decoded message: " + meinBaum.Decode(encoded) + Environment.NewLine);

            foreach(Knoten occ in meinBaum.GeordneteKnoten)
            {
                if (String.IsNullOrWhiteSpace(occ.Symbol.ToString()))
                {
                    tbOutput.AppendText("Symbol:\t'<LEER>'\t->\t" + occ.Frequenz + Environment.NewLine);
                }
                else
                {
                    tbOutput.AppendText("Symbol:\t'" + occ.Symbol + "'\t->\t" + occ.Frequenz + Environment.NewLine);
                }
            }

            double prozent = Math.Round((encoded.Length * 100.0) / (tbInput.Text.Length * 8), 2);

            tbOutput.AppendText("Datenersparnis:\t" + encoded.Length + "/" + tbInput.Text.Length*8  + "\t(" + prozent + "%)");
            pieChart.Series["s1"].Points.Clear();
            pieChart.Series["s1"].Points.AddXY("Komprimiert", prozent);
            pieChart.Series["s1"].Points.AddXY("Original", 100-prozent);

            meinBaum.printTree(meinBaum.Wurzel);


        }
    }
}
