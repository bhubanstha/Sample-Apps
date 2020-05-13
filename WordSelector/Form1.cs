using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordSelector
{
    public partial class Form1 : Form
    {

        bool rightKeyPress, leftKeyPress;
        int currentIndex = -1;
        List<int> foundIndexes = new List<int>();

        public Form1()
        {
            InitializeComponent();
            rtbContent.Text = @"ZOMBIES are a value stock. They are wordless and oozing and brain dead, but they’re an ever-expanding market with no glass ceiling. Zombies are a target-rich environment, literally and figuratively. The more you fill them with bullets, the more interesting they become. Roughly 5.3 million people watched the first episode of “The Walking Dead” on AMC, a stunning 83 percent more than the 2.9 million who watched the Season 4 premiere of “Mad Men.” This means there are at least 2.4 million cable-ready Americans who might prefer watching Christina Hendricks if she were an animated corpse.

Statistically and aesthetically that dissonance seems perverse. But it probably shouldn’t. Mainstream interest in zombies has steadily risen over the past 40 years. Zombies are a commodity that has advanced slowly and without major evolution, much like the staggering creatures George Romero popularized in the 1968 film “Night of the Living Dead.” What makes that measured amplification curious is the inherent limitations of the zombie itself: You can’t add much depth to a creature who can’t talk, doesn’t think and whose only motive is the consumption of flesh. You can’t humanize a zombie, unless you make it less zombie-esque. There are slow zombies, and there are fast zombies— that’s pretty much the spectrum of zombie diversity. It’s not that zombies are changing to fit the world’s condition; it’s that the condition of the world seems more like a zombie offensive. Something about zombies is becoming more intriguing to us. And I think I know what that something is.

";
        }

        public Form1(string filePath)
        {
            InitializeComponent();
            string text = System.IO.File.ReadAllText(filePath);
            rtbContent.Text = text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string s = rtbContent.Text;
            string space = " ";

            for (int i = s.IndexOf(space); i > -1; i = s.IndexOf(space, i + 1))
            {
                foundIndexes.Add(i);
            }
        }

        private void btnAssociate_Click(object sender, EventArgs e)
        {
            bool associated = new FileAssociation
            {
                Extension = ".abc",
                ProgId = "MyWordSelectionProgramUpdate",
                FileTypeDescription = "My Word Selection Program Update"
            }.SetAssociation();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            RichTextBox tb = rtbContent;
            tb.SelectAll();
            tb.SelectionBackColor = Color.Transparent;
            
            
            //capture left arrow key
            if (keyData == Keys.Left)
            {

                if (currentIndex <= -1)
                {
                    currentIndex = foundIndexes.Count;
                }
                if (rightKeyPress)
                {
                    currentIndex--;
                }
                tb.SelectionStart = currentIndex == foundIndexes.Count ?
                    foundIndexes[currentIndex - 1] : (currentIndex == 0 ? 0 : foundIndexes[currentIndex - 1] + 1);
                tb.SelectionLength = currentIndex == foundIndexes.Count ?
                    tb.Text.Length - foundIndexes[currentIndex - 1] :
                    foundIndexes[currentIndex] - tb.SelectionStart;
                tb.SelectionBackColor = Color.Yellow;
                currentIndex--;
                leftKeyPress = true;
                rightKeyPress = false;

            }
            //capture right arrow key
            else if (keyData == Keys.Right)
            {
                if (leftKeyPress)
                {
                    currentIndex++;
                }
                if (currentIndex == foundIndexes.Count)
                {
                    currentIndex = -1;
                }
                tb.SelectionStart = currentIndex == -1 ? 0 : foundIndexes[currentIndex] + 1;
                tb.SelectionLength = currentIndex == foundIndexes.Count - 1 ?
                    tb.Text.Length - tb.SelectionStart :
                    foundIndexes[currentIndex + 1] - tb.SelectionStart;
                tb.SelectionBackColor = Color.Yellow;
                currentIndex++;
                leftKeyPress = false;
                rightKeyPress = true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
