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
//using System.Xml;
//using System.Xml.Serialization;
//public class GZipStream : System.IO.Stream;
    using System.IO.Compression;

namespace test
{
    
    public partial class Form1 : Form
    {
        string path_now;
        public Form1()
        {
            InitializeComponent();
        }
         public void fil2tab(string filename)
        {
            string[] res = { };
            string[] res2 = { };
            string[] key = { "source_ip", "count", "dkim", "spf", "domain", "result" };

            DataTable dt = new DataTable("tab0");//создаем таблицу данных
            for (int i = 0; i < key.Length; i++)//инициализируем столбцы dt по ключам
                dt.Columns.Add(key[i]);
            //string mas1[];
            //string path = @"C:\SomeDir\hta.txt";
            //  using (FileStream fs = new FileStream("Y:\\source\\repos\\test_\\test\\xml_file_in\\1.xml", FileMode.Open))
            //            {                textBox1.Text = textfromtag(fs, "<record>", "<record>");
            //}
            string sfull = "", path = filename;
                //path = "Y:\\source\\repos\\test_\\test\\xml_file_in\\1.xml";
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
                {
                    res2 = textfromtag(res[j], key[i], "/" + key[i]);
                    textBox1.Text = textBox1.Text + res2[0];
                    dt.Rows[j][key[i]] = res2[0];
                    //  MessageBox.Show(dt.Rows[0][key[i]].ToString());
                }
                textBox1.Text += System.Environment.NewLine;
            }
            dataGridView1.DataSource = dt;
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
        public static string GZipDecompress(string fulPath)
        {   FileInfo fileToDecompress = new System.IO.FileInfo(fulPath);
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);
                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    { try
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                            return decompressedFileStream.Name;//возвращаем имя извлеченного файла
                        }
                        catch { return "none"; }
                    }
                }
            }
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
            path_now = Directory.GetCurrentDirectory()+"\\Temp\\";
            Directory.CreateDirectory(path_now);
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
            //MessageBox.Show(fullPath);
            
            // если не архив то открываем
            string ext = fullPath.Remove(0, fullPath.LastIndexOf('.') + 1);
            if (ext=="xml") fil2tab(fullPath);

            if (ext == "gz")
            {
                fullPath=GZipDecompress(fullPath);
            }
            if (ext == "zip")
            {
                ZipFile.ExtractToDirectory(fullPath, path_now);
                var intentedPath = string.Empty;
                //open archive
                //using 
                var archive = ZipFile.OpenRead(fullPath);
                    //.OpenRead(fullPath);
                {
                    //since there is only one entry grab the first
                    var entry = archive.Entries.First();
                    //the relative path of the entry in the zip archive
                    var fileName = entry.FullName;
                    //intended path once extracted would be
                    intentedPath = Path.Combine(fullPath, fileName);
                }
                fullPath = intentedPath;
            }

                    ext = fullPath.Remove(0, fullPath.LastIndexOf('.') + 1);
            if (ext == "xml") fil2tab(fullPath);
            tab_.SelectedIndex = 2;
        }
    }
}
