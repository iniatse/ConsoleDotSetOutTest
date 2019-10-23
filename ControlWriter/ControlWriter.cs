using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;

namespace WpfApp1
{
    public class ControlWriter : TextWriter
    {
        private TextBox textBox;
        private ScrollViewer scrollViewer;

        public ControlWriter(TextBox textBox, ScrollViewer scrollViewer)
        {
            this.textBox = textBox;
            this.scrollViewer = scrollViewer;
        }

        public override void Write(char value)
        {
            textBox.Dispatcher.Invoke(() =>
            {
                textBox.AppendText(value.ToString());
                scrollViewer.ScrollToBottom();
            }, System.Windows.Threading.DispatcherPriority.Background);
        }

        public override void Write(string value)
        {
            textBox.Dispatcher.Invoke(() =>
            {
                textBox.AppendText(value.ToString());
                scrollViewer.ScrollToBottom();
            }, System.Windows.Threading.DispatcherPriority.Background);
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
