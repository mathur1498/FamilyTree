using System;
using System.Collections.Generic;
namespace Family
{
    class Program
    {
        public class Node
        {
            //properties of the child node
            public int name_id;
            public string name;
            public double amount;
            public double min_amount;
            public List<Node> child = new List<Node>();//connection to other nodes
        };
        //creates the node
        static Node newNode(int name_id, string name, double amount)
        {
            Node temp = new Node();
            temp.name_id = name_id;
            temp.name = name;
            temp.amount = amount;
            temp.min_amount = temp.amount * 0.05;//this stores the minimum amount i.e. 5% of the total amount
            return temp;
        }

        static Node searchParent(Node root, string parent)
        {
            if (root == null)
                return root;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            //traversal through the list i.e. child nodes of the node
            while (q.Count != 0)
            {
                int n = q.Count;
                while (n > 0)
                {
                    Node p = q.Peek();
                    q.Dequeue();
                    if (p.name.Equals(parent))
                        return p;
                    for (int i = 0; i < p.child.Count; i++)
                        q.Enqueue(p.child[i]);
                    n--;
                }
            }
            return null;
        }
 
        static void LevelOrderTraversal(Node root)
        {
            if (root == null)
                return;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root); 
            while (q.Count != 0)
            {
                int n = q.Count;
                while (n > 0)
                {
                    Node p = q.Peek();
                    q.Dequeue();
                    Console.Write(p.amount + " : " + p.name + " : " + p.min_amount + " " + "|| " );
                    for (int i = 0; i < p.child.Count; i++)
                        q.Enqueue(p.child[i]);
                    n--;
                }
                Console.WriteLine();
            }
        }
        public static void Main(String[] args)
        {
            Console.WriteLine("Enter Name_id, Name and Amount of the Super Parent");
            int id = Convert.ToInt32(Console.ReadLine());
            string name = Console.ReadLine();
            double amount = Convert.ToDouble(Console.ReadLine());
            Node root = newNode(id,name,amount);
            int ch=0;
            while(ch!=4)
            {
                Console.Clear();
                Console.WriteLine("\nChoose from the options:\n1.Insert the child\n2.Display the family\n3.Display the descendents of person\n4.Exit\n");
                ch = Convert.ToInt32(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        Console.WriteLine("Enter the parent name");
                        name = Console.ReadLine();
                        Node n = searchParent(root, name);
                        if (n == null)
                        {
                            Console.WriteLine("No such parent found\n");
                            break;
                        }
                        Console.WriteLine("Enter Name_id, Name and Amount of the child");
                        id = Convert.ToInt32(Console.ReadLine());
                        name = Console.ReadLine();
                        amount = Convert.ToDouble(Console.ReadLine());
                        //check if the child can be added or not
                        if (n.min_amount < n.amount - amount)
                        {
                            n.amount -= amount;
                            n.child.Add(newNode(id, name, amount));
                        }
                        else
                        {
                            Console.WriteLine("\nThe parent is broke so can't produce this child\n");
                            Console.ReadLine();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Level order traversal through the family hierarchy");
                        LevelOrderTraversal(root);
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Enter the parent name");
                        name = Console.ReadLine();
                        Node p = searchParent(root, name);
                        if (p == null)
                        {
                            Console.WriteLine("No such parent found\n");
                            break;
                        }
                        Console.WriteLine("The descendents of " + p.name + " are:\n");
                        LevelOrderTraversal(p);
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("You've successfully exited");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Enter a valid value");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
