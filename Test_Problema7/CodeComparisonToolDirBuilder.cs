using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Test_Problema7
{
    public class TreeStructBuild
    {
        public string Name { get; private set; }
        //creezi o lista parinte de tipul CodeComparisonToolDirBuilder
        public TreeStructBuild Parent { get; set; }
        public string Hash { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        //creezi o lista numita children de tipul CodeComparisonToolDirBuilder 
        public List<TreeStructBuild> Children { get; private set; }

        //ii creezi constructorul
        public TreeStructBuild(string name, TreeStructBuild parent)
        {
            Name = name;
            Parent = parent;
            Children = new List<TreeStructBuild>();
        }
        //o metoda pt a determina daca copilul are sau nu nume
        public bool Contains(string name)
        {
            return Children.Any(d => d.Name.Equals(name));
        }

        //o metoda pentru a returna obiectul cu numele cautat de noi
        public TreeStructBuild Get(string name)
        {
            return Children.FirstOrDefault(d => d.Name.Equals(name));
        }

        //o metoda pt a returna numele ca un string
        public override string ToString()
        {
            return Name;
        }

        //o metoda pt a genera un o structura tip tree
        public void PrintTree(StringBuilder output, bool isRoot)
        {
            //metodei ii asignezi un obiect de clasa stringbuilder si o variabila de tip bool
            string prefix;

            //aici voi crea prefixul pt fiecare copil
            //copil de gradul 0
            string pre_0 = "    ";

            //copil de gradul 1
            string pre_1 = "│   ";

            //copil de gradul 2
            string pre_2 = "|-- ";

            //copil de gradul 3 sau mai mult
            string pre_3 = "`-- ";

            //creezi un obiect de tipul CodeComparisonToolDirBuilder si ii dai numele tree
            TreeStructBuild tree = this;

            if (tree.Parent != null && !(tree.Equals(tree.Parent.Children.Last())))
            {
                //verifica daca parintele sau exista si daca el nu e ultimul copil al parintelui 
                prefix = pre_2;
            }
            else
            {
                //altfel inseamna ca obiectul tree e ultimul copil
                prefix = pre_3;
            }

            while (tree.Parent != null && tree.Parent.Parent != null)
            {
                //cat timp parintele si "bunicul" obicetului sunt diferite de null indeplinest:
                if (tree.Parent != null && !(tree.Parent.Equals(tree.Parent.Parent.Children.Last())))
                {
                    //verifica daca parintele exista si daca pairntele nu este ultimul copil al "bunicului"
                    //adica elementul nu e primult element, e al doilea
                    prefix = pre_1 + prefix;
                }
                else
                {
                    //altfel asta inseamna ca e primul element
                    prefix = pre_0 + prefix;
                }

                tree = tree.Parent;
                //ne mutam la parintele obiectului
            }

            if (isRoot)
            {
                //verificam daca e radacina/primul parinte
                output.AppendLine(this.Name);
            }
            else
            {
                //altfel inseamna ca are prefix, deci e copil
                output.AppendLine(prefix + this.Name);
            }

            foreach (TreeStructBuild child in this.Children)
            {
                //construiesti schema pt fiecare copil din stiva de copii
                child.PrintTree(output, false);
            }
        }
        public static void TreeStruct(TreeStructBuild parent, List<string> edges)
        {
            if (edges.Count == 0) return;


            List<TreeStructBuild> matchedChildren = new List<TreeStructBuild>();
            //creezi o lista de obiecte 

            foreach (TreeStructBuild tree in parent.Children)
            {
                //pt fiecare obiect de tip CodeComparisonToolDirBuilder care se afla in lista de copii ai parintilor ii vom adauga numele in lista de copii
                if (tree.Name == edges[0])
                {
                    matchedChildren.Add(tree);
                }
            }

            TreeStructBuild pointer;
            //verifici daca are copii
            if (matchedChildren.Count != 0)
            {
                pointer = matchedChildren[0];
            }
            else
            {
                //daca nu are copii il adaugi in lista de parinti/ adica daca nu are extremitati
                pointer = new TreeStructBuild(edges[0], parent);
                parent.Children.Add(pointer);
            }

            edges.RemoveAt(0);
            TreeStruct(pointer, edges);
        }

    }
}