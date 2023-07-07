using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Test_Problema7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.ScrollBars = ScrollBars.Vertical;
            // Allow the TAB key to be entered in the TextBox control.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rootDirectoryPath = @"C:\Users\Alexandra"; // rootpath ul tau

            // Creezi directory tree ul
            TreeStructBuild rootDirectory = CreateDirectoryTree(rootDirectoryPath);

            //creezi un string unde stochezi tree ul
            //folosesti stringbuilder class pentru ca asta iti permite sa modifici string ul fara a-i aloca alt loc in memorie
            StringBuilder output = new StringBuilder();

            rootDirectory.PrintTree(output, true);
            //scrii in textbox
            textBox1.Text= (output.ToString());
            
        }
        public static TreeStructBuild CreateDirectoryTree(string path, TreeStructBuild parent = null)
        {
            //creezi un obiect de tip DirectoryInfo pt a putea opera directoare
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            TreeStructBuild currentDirectory = new TreeStructBuild(directoryInfo.Name, parent);
            //creezi un vector de tip DIrectoryInfo in care pasezi subdirectoare 
            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();
            foreach (var subDirectory in subDirectories)
            {
                try
                {
                    if (currentDirectory == null || currentDirectory.Parent == null || currentDirectory.Parent.Parent == null)
                    {
                        //pentru a merge doar la adancimea de 3

                        TreeStructBuild childDirectory = CreateDirectoryTree(subDirectory.FullName, currentDirectory);
                        //apelezi din nou metoda de fiecare data cand gasesti un director nou

                        currentDirectory.Children.Add(childDirectory);
                    }

                }
                //verifici daca ai acces sau nu la un directory, si daca nu ai treci peste el si afisezi mesaj
                catch (UnauthorizedAccessException e)
                {
                    
                    //pentru a continua foreach loop ul si dupa ce detecteaza eroarea
                }
            }



            return currentDirectory;
        }
    }
}
