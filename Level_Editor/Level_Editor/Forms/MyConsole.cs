using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Collections.ObjectModel;

namespace WindowsGame1
{
    public partial class MyConsole : Form
    {
        PowerShell powershell;
        StringBuilder text;
        Stack<string> nextcommands;
        Stack<string> prevcommands;
        public MyConsole()
        {
            InitializeComponent();
            powershell = PowerShell.Create();
            text = new StringBuilder();
            
            prevcommands = new Stack<string>();
            nextcommands = new Stack<string>();

            powershell.AddCommand("Out-String");
            this.textBox1.KeyUp += new KeyEventHandler(MyConsole_KeyUp);
            this.textBox1.KeyUp += new KeyEventHandler(textBox1_KeyUp);
            this.Show();
            this.Activate();
        }

        void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (prevcommands.Count != 0)
                {
                    nextcommands.Push(prevcommands.Peek());
                    this.textBox1.Text = prevcommands.Pop();
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                if (nextcommands.Count != 0)
                {
                    prevcommands.Push(nextcommands.Peek());
                    this.textBox1.Text = nextcommands.Pop();
                }

            }
        }
        //
        void MyConsole_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode.ToString());
            if (e.KeyCode == Keys.Enter)
            {
                
                string script = this.textBox1.Text;
                prevcommands.Push(script);
                powershell.AddScript(script);
                Collection<PSObject> results;
                try
                {
                    results = powershell.Invoke();
               
                    foreach (PSObject r in results)
                    {
                        text.Append(r.ToString() + "\n");
                    }
                }
                catch (Exception ex) { text.Append(ex.Message+ "\n"); }

                this.richTextBox1.Text = text.ToString() + "\n";
                this.textBox1.Text = "";
                //send text to powershell
                //get powershell results
                //set cursor to line after results
            }
        }

        private void MyConsole_Load(object sender, EventArgs e)
        {

        }

        public void WriteLine(string st)
        {
            text.Append(st + "\n");
            this.richTextBox1.Text = text.ToString();
        }

        public void AddVariable(string name, Object o)
        {
            powershell.Runspace.SessionStateProxy.SetVariable(name, o);
        }
    }
}
