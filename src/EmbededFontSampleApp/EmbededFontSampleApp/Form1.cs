using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmbededFontSampleApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            

            LoadFont();

            comboBox1.SelectedIndexChanged += (s, e) =>
            {
                var cbx = (ComboBox)s;

               var fontFamily= cbx.SelectedItem as FontFamily;

                Font = new Font(fontFamily, Font.SizeInPoints);

                Invalidate(true);

                this.UpdateStyles();
            };

            label1.Text = sampleTextData1;

            label2.Text = sampleTextData2;
        }

        private void LoadFont()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();

            string[] fontResourceNames =  {
                "EmbededFontSampleApp.Fonts.D2Coding.ttf",
                "EmbededFontSampleApp.Fonts.NanumGothic.ttf" };

            foreach (var fontResourceName in fontResourceNames)
            {

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fontResourceName))
                {
                    int streamLength = (int)stream.Length;

                    IntPtr data = Marshal.AllocCoTaskMem(streamLength);

                    byte[] fontData = new byte[streamLength];

                    stream.Read(fontData, 0, streamLength);

                    Marshal.Copy(fontData, 0, data, streamLength);

                    pfc.AddMemoryFont(data, streamLength);

                    stream.Close();

                    Marshal.FreeCoTaskMem(data);
                }
            }

            comboBox1.Items.AddRange(pfc.Families);

            var fontFamliy = pfc.Families[0];

            Font = new Font(fontFamliy.Name, Font.SizeInPoints);
        }


        private string sampleTextData1 = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

        private string sampleTextData2 = "1234567890";
    }
}
