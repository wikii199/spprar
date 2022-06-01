using System;
using System.Collections.Generic;
using System.Data;


class Program
{
    public static void Main(string[] args)
    {
        int points = 0;
        points += Test(() =>
        {
            int p = Zadanie1("aaabbbzcdjefggasd").Equals("bcdefgjz") ? 2 : 0;
            p += Zadanie1("abcd").Equals("abcd") && Zadanie1("").Equals("") ? 1 : 0;
            return p;
        }, "Zadanie 1 ");
        points += Test(() =>
        {
            if (Zadanie2("abcd") && Zadanie2("") && !Zadanie2("abcda") && Zadanie2("a") && !Zadanie2("cccc"))
            {
                return 1;
            }

            return 0;
        }, "Zadanie 2");
        points += Test(() =>
        {
            QueueTest<int> q = new QueueTest<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            QueueTest<string> sq = new QueueTest<string>();
            sq.Enqueue("abcd");
            sq.Enqueue("");
            if (q.Dequeue() == 1 && q.Dequeue() == 2 && q.Dequeue() == 3 && sq.Dequeue().Equals("abcd") &&
                sq.Dequeue().Equals(""))
            {
                return 1;
            }

            return 0;
        }, "Zadanie 3");
        points += Test(() =>
        {
            Student[] students =
            {
                new Student() {Birth = new DateTime(200, 10, 10), Name = "Adam", Pesel = "01001001001"},
                new Student() {Birth = new DateTime(200, 10, 11), Name = "Adam", Pesel = "01001001001"},
                new Student() {Birth = new DateTime(200, 10, 10), Name = "Karol", Pesel = "01001001002"},
                new Student() {Birth = new DateTime(200, 10, 10), Name = "Ewa", Pesel = "01001001003"},
                new Student() {Birth = new DateTime(200, 10, 10), Name = "Alicja", Pesel = "01001001001"}
            };
            Student[] s =
            {
                new Student() {Birth = new DateTime(200, 10, 10), Name = "Adam", Pesel = "01001001001"},
                new Student() {Birth = new DateTime(200, 10, 10), Name = "Adam", Pesel = "01001001001"},
                new Student() {Birth = new DateTime(200, 10, 10), Name = "Adam", Pesel = "01001001001"},
            };
            if (CreateStudentGroup(students).Count == 3 && CreateStudentGroup(s).Count == 1)
            {
                return 1;
            }

            return 0;
        }, "Zadanie 4");
        points += Test(() =>
        {
            TreeNode node = new TreeNode() {Value = 5, Children = new List<TreeNode>()};
            node.Children.Add(new TreeNode()
            {
                Value = 6, Children = new List<TreeNode>()
                {
                    new TreeNode() {Value = 2, Children = new List<TreeNode>()},
                    new TreeNode() {Value = 2, Children = new List<TreeNode>()},
                    new TreeNode() {Value = 2, Children = new List<TreeNode>()},
                }
            });
            node.Children.Add(new TreeNode()
            {
                Value = 6, Children = new List<TreeNode>()
                {
                    new TreeNode() {Value = 3, Children = new List<TreeNode>()},
                    new TreeNode() {Value = 3, Children = new List<TreeNode>()},
                    new TreeNode() {Value = 3, Children = new List<TreeNode>()},
                }
            });
            TestTree tree1 = new TestTree() {Root = node};
            TestTree tree2 = new TestTree() {Root = node.Children[0]};
            int sum1 = 0;
            int sum2 = 0;
            tree1.Traverse(node => sum1 += node.Value);
            tree2.Traverse(node => sum2 += node.Value);
            return sum1 == 32  && sum2 == 12 ? 2 : 0;
        }, "Zadanie 5");
        points += Test(() =>
        {
            TestGraph graph = new TestGraph(5);
            graph.AddDirectedEdge(0, 1);
            graph.AddDirectedEdge(1, 2);
            graph.AddDirectedEdge(2, 3);
            graph.AddDirectedEdge(4, 0);
            graph.AddDirectedEdge(2, 4);
            TestGraph g = new TestGraph(3);
            g.AddDirectedEdge(0, 1);
            g.AddDirectedEdge(0, 2);
            g.AddDirectedEdge(1, 2);
            int p = graph.IsPath(0, 3) && graph.IsPath(0, 4) && graph.IsPath(4, 1) && !graph.IsPath(3, 1) ? 1 : 0;
            p += g.IsPath(0, 2) && !g.IsPath(2, 0) ? 1 : 0;
            return p;
        }, "Zadanie 6");
        Console.WriteLine($"Suma punktów: {points}");
        Console.WriteLine("Anna Welna");
    }

    /**
    ********************************************************************************************************************
    */

    /**
     * Zadanie 1 (2 pkt.)
     * 
     * Zdefiniuj metodę, która zwróci posortowany rosnąco najdłuższy fragment łańcucha wejściowego złożony z unikalnych znaków.
     *
     * Przykład 1
     * Wejście:
     * aaabbbzcdjefggasd
     * 
     * Najdłuższy fragment złożony z unikalnych znaków to: bzcdjefg 
     * po posortowaniu: bcdefgjz
     * Wyjście
     * bcdefgjz
     * 
     * Przykład 2
     * Wejście:
     * ecbad
     * Wyjście:
     * abcde
     */
    
    public static string Zadanie1(string input)
    {
        if (input.Length == 0)
        {
            return "";
        }
        int max = 0;
        string curr = "";
        string longest = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (!curr.Contains(input[i]))
            {
                curr += input[i];
            }
            else
            {
                if (curr.Length > max)
                {
                    max = curr.Length;
                    longest = curr;
                }
                curr = "";
            }
        }
        if (curr.Length > max)
        {
            max = curr.Length;
            longest = curr;
        }
        var arr = longest.ToCharArray();
        Array.Sort(arr);
        string longestSorted = "";
        foreach (var character in arr)
        {
            longestSorted += character.ToString();
        }
        return longestSorted;
    }

    /**
     * Zadanie 2 (1 pkt.)
     * Zdefiniuj metodę Zadanie2, która zwraca true, jeśli łańcuch 'input' składa się z unikalnych znaków
     *
     * Przykład 1
     * Wejście:
     * abcd
     * wyjście:
     * true
     *
     * Przykład 2
     * Wejście:
     * aabcd
     * Wyjście
     * false
     */
    public static bool Zadanie2(string input)
    {
        int[] charArray = new int[255];
        for (int i = 0; i < input.Length; i++)
        {
            charArray[(int)input[i]] += 1;
            if (charArray[(int)input[i]] > 1) return false;
        }
        return true;
    }

    /**
     * Zadanie 3 (1 pkt.)
     * Dana jest klasa kolejki dowiązaniowej
     * Zdefiniuj metodę Dequeue, która usuwa element z kolejki
     */
    class NodeTest<T>
    {
        public T Value { get; set; }
        public NodeTest<T> Next { get; set; }
    }

    class QueueTest<T>
    {
        private NodeTest<T> _head;
        private NodeTest<T> _tail;

        public void Enqueue(T value)
        {
            NodeTest<T> nodeTest = new NodeTest<T>() {Value = value};
            if (isEmpty())
            {
                _head = nodeTest;
                _tail = nodeTest;
                return;
            }

            _tail.Next = nodeTest;
            _tail = nodeTest;
        }

        /**
         * Zaimplementuj metodę usuwania z kolejki
         */
        public T Dequeue()
        {
            if (isEmpty()) throw new Exception("Empty Queue!");
            T result = _head.Value;
            _head = _head.Next;
            return result;
        }

        public bool isEmpty()
        {
            return _head == null;
        }
    }


    /**
     * Zadanie 4 (1 pkt.)
     * Dana jest klasa Student i metoda, która tworzy grupy studenckie.
     * Popraw klasę Student, aby utworzona grupa studencka składała się z unikalnych studentów.
     * Studenci moga mieć identyczne imiona i daty urodzin, ale każdy ma inny pesel.
     */
    public class Student : IEquatable<Student>
    {
        public string Pesel { get; init; }
        public string Name { get; init; }
        public DateTime Birth { get; init; }

        public bool Equals(Student other)
        {
            if (this.Pesel == other.Pesel) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Student)) return false;
            return Equals(obj as Student);
        }

        public override int GetHashCode()
        {
            return Pesel.GetHashCode();
        }
    }
    public static ISet<Student> CreateStudentGroup(Student[] students)
    {
        HashSet<Student> group = new HashSet<Student>();
        foreach (Student student in students)
        {
            if (!group.Contains(student)) group.Add(student);
        }
        return group;
    }

    /**
     * Zadanie 5 (2 pkt.)
     * Dana jest klasa drzewa. Zaimplementuj metodę do  przeglądania drzewa, aby dla każdego węzła wywołać delegata `action`.
     */
    public class TreeNode
    {
        public int Value { get; set; }
        public List<TreeNode> Children { get; set; }
    }

    public class TestTree
    {
        public TreeNode Root { get; init; }

        public void Traverse(Action<TreeNode> action)
        {
            Traverse(Root, action);
        }

        private void Traverse(TreeNode node, Action<TreeNode> action)
        {
            action(node);
            foreach (var child in node.Children)
            {
                Traverse(child, action);
            }
        }
    }

    /**
     * Zadanie 6 (2 pkt.)
     * Dana jest klasa impelmentująca graf nieważony i skierowany w postaci macierzy sąsiedztw.
     * Zaimplementuj metodę, która zwraca true, jeśli istnieje ścieżka między węzłami 'start' i 'end'.
     */
    class TestGraph
    {
        private int[,] _matrix;

        public TestGraph(int n)
        {
            _matrix = new int[n, n];
        }

        public bool AddDirectedEdge(int source, int target)
        {
            if (Check(source) && Check(target))
            {
                _matrix[source, target] = 1;
                return true;
            }

            return false;
        }

        private bool Check(int node)
        {
            return node >= 0 && node < _matrix.GetLength(0);
        }

        public bool IsPath(int start, int end)
        {
            if (!Check(start) || !Check(end))
            {
                return false;
            }

            var visited = new bool[_matrix.GetLength(0)];
            return IsPath(start, end, visited);
        }

        private bool IsPath(int start, int end, bool[] visited)
        {
            if (start == end)
            {
                return true;
            }

            visited[start] = true;
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                if (_matrix[start, i] == 1 && !visited[i])
                {
                    if (IsPath(i, end, visited))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    /**
    *********************************************************************************************************************
    */
    public static int Test(Func<int> action, string message)
    {
        try
        {
            int p = action.Invoke();
            Console.WriteLine($"{message}: {p}");
            return p;
        }
        catch (Exception e)
        {
            Console.WriteLine($"{message}: 0");
            return 0;
        }
    }
}