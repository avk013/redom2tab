using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {/*
            textBox1.ScrollBars = ScrollBars.Both;
            ////////////////////////////тадагрид
            int stro = 0;
            for (int i = 0; i <= 100; i++) dataGridView1.Columns.Add("Column", i.ToString());
            for (int i = 0; i < 100; i++) dataGridView1.Rows.Add();
            for (int i = 0; i < 100; i++) dataGridView1.Rows[stro++].HeaderCell.Value = i.ToString();
            ////////////////////////////
            int a2, a1 = a2 = 1;
            //DataTable datas = new string [90,90];
            //dataGridView1.RowCount = 100;
            //dataGridView1.ColumnCount = 100;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Y:\\source\\repos\\test_\\test\\xml_file_in\\1.xml");
            // получим корневой элемент
            //textBox1.Text += xDoc.ToString()+ System.Environment.NewLine;
            XmlElement xRoot = xDoc.DocumentElement;
            //textBox1.Text += xRoot.ToString()+System.Environment.NewLine;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                a1 = 1;
                dataGridView1.Rows[a1].Cells[a2++].Value = xnode.FirstChild.InnerText;
                textBox1.Text += xnode.FirstChild.InnerText + System.Environment.NewLine;
                if (xnode.HasChildNodes)
                {

                    foreach (XmlNode xnode1 in xnode)
                    {

                        textBox1.Text += xnode1.FirstChild.InnerText + System.Environment.NewLine;
                        //  dataGridView1.Rows[a1++].Cells[a2].Value = xnode1.FirstChild.InnerText;
                        if (xnode1.HasChildNodes)
                        {

                            foreach (XmlNode xnode2 in xnode1)
                            {//a2=a2 + 1;
                             //  textBox1.Text += xnode2.FirstChild.InnerText + System.Environment.NewLine;
                             // dataGridView1.Rows[a1].Cells[a2++].Value = xnode2.FirstChild.InnerText;
                             //dataGridView1.Rows[a1++].Cells[a2].Value = xnode2.FirstChild.InnerText;
                            }
                        }
                    }
                }
                // получаем атрибут name
                /*if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("record");
                   // if (attr != null)
                      //  textBox1.Text += attr.Value;
                    //Console.WriteLine(attr.Value);
                }
                // обходим все дочерние узлы элемента user
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    // если узел - company
                    if (childnode.Name == "source_ip")
                        //if (childnode.
                    {
                        //Console.WriteLine($"count: {childnode.InnerText}");
                        textBox1.Text += childnode.InnerText;
                    }
                    // если узел age
                    if (childnode.Name == "domain")
                    {
                        //Console.WriteLine($"domain: {childnode.InnerText}");
                        textBox1.Text += childnode.InnerText;
                    }
                */
           
     //   }
    //}

    //    dataGridView1.DataSource = datas;
    //  Console.WriteLine();


    //dataGridView1.DataSource = datas;
    //dataGridView1.AutoResizeColumns();

}
        public class feedback
        {
            // public string feedback;
            public string source_ip;
            public string count;
            public string dkim;
            public string spf;
            public string domain;
        }
         public string[] textfromtag(string source, string begino, string finalo)
        {//ищем содержимое в тегах
            string[] res = { };
            begino = "<" + begino + ">";
            finalo = "<" + finalo + ">";
            int i = 0;
            finalo.Replace("/", @"\/");//для Reg маскируем слеш
            /////////
            //Regex Reg = new Regex(@"""" + begino + ""(.*?)"" + finalo+"""");
            System.Text.RegularExpressions.Regex Reg = new Regex(@begino + @"(.*?|(\s?|\S?))" + @finalo);
           // textBox28.Text = @begino + @"(.*?|(\s|\S))" + @finalo;
            MatchCollection reHref = Reg.Matches(source);

            foreach (Match match in reHref)
            {
                Array.Resize<string>(ref res, i + 1);
                res[i] = match.ToString();
                res[i] = res[i].Remove(0, begino.Length);
                res[i] = res[i].Remove(res[i].Length - finalo.Length, finalo.Length);
                i++;
               // textBox7.Text += i.ToString() + "=";
            }
            ////////
            return res;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string[] res = { };
            string[] res2 = { };
            string[] key = {"source_ip","count","dkim","spf","domain","result"  };

            DataTable dt = new DataTable("tab0");//создаем таблицу данных
            for (int i = 0; i < key.Length; i++)//инициализируем столбцы dt по ключам
                dt.Columns.Add(key[i]);
             //string mas1[];
            //string path = @"C:\SomeDir\hta.txt";
            //  using (FileStream fs = new FileStream("Y:\\source\\repos\\test_\\test\\xml_file_in\\1.xml", FileMode.Open))
            //            {                textBox1.Text = textfromtag(fs, "<record>", "<record>");
            //}
            string sfull="",path = "Y:\\source\\repos\\test_\\test\\xml_file_in\\1.xml";
            string[] readText = File.ReadAllLines(path);
            foreach (string s in readText)
            {
                sfull += s;
                //Console.Read();

             
                
            }
            res = textfromtag(sfull, "record", "/record");
            for (int j = 0; j < res.Length; j++)//считываем все в record
            {
                dt.Rows.Add(); // добавляем столбцы соглано 
                for (int i = 0; i < key.Length; i++)// разбираем по key
                {                    res2 = textfromtag(res[j], key[i], "/" + key[i]);
                    textBox1.Text = textBox1.Text + res2[0];
                    dt.Rows[j][key[i]] = res2[0];
                    //  MessageBox.Show(dt.Rows[0][key[i]].ToString());
                }
                textBox1.Text += System.Environment.NewLine;
            }
              dataGridView1.DataSource = dt;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
           
        }
        void tutu(DragEventArgs e)
        {
            listView1.SmallImageList = imageList1;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    // Code to read the contents of the text file
                    if (File.Exists(fileLoc))
                    {
                        // using (TextReader tr = new StreamReader(fileLoc))
                        {
                            //   MessageBox.Show(tr.ReadToEnd());
                           // MessageBox.Show(fileLoc);
                            ListViewItem lvi = new ListViewItem();
                            // установка названия файла
                            lvi.Text = fileLoc.Remove(0, fileLoc.LastIndexOf('\\') + 1);
                            lvi.Name = fileLoc;
                            string ext = fileLoc.Remove(0, fileLoc.LastIndexOf('.') + 1);
                            // MessageBox.Show(ext);
                            if (ext=="tar") lvi.ImageIndex = 2;
                            else if (ext=="xml") lvi.ImageIndex = 1;
                            else lvi.ImageIndex = 0;
                            //lvi.ImageIndex = 1; // установка картинки для файла
                                                // добавляем элемент в ListView
                            listView1.Items.Add(lvi);
                        }
                    }
                }
            }
            //MessageBox.Show("+");

        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {//перетаскивание файла на форму....
          //  tutu(e);
            /*listView1.SmallImageList = imageList1;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    // Code to read the contents of the text file
                    if (File.Exists(fileLoc))
                    {
                       // using (TextReader tr = new StreamReader(fileLoc))
                        {
                            //   MessageBox.Show(tr.ReadToEnd());
                            MessageBox.Show(fileLoc);
                            ListViewItem lvi = new ListViewItem();
                            // установка названия файла
                            lvi.Text = fileLoc.Remove(0, fileLoc.LastIndexOf('\\') + 1);
                            lvi.ImageIndex = 1; // установка картинки для файла
                                                // добавляем элемент в ListView
                            listView1.Items.Add(lvi);
                        }
                    }
                }
            }
            //MessageBox.Show("+");
            */
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
          tutu(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.AllowDrop = true;
          //  listView1.DragDrop += new DragEventHandler(listView1_DragDrop);
           // listView1.DragEnter += new DragEventHandler(listView1_DragEnter);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
          e.Effect = DragDropEffects.Copy;
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
                  }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {

            String fullPath = listView1.SelectedItems[0].Name;
            MessageBox.Show(fullPath);
        }
    }
}
