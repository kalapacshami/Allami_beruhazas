using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allami_beruhazas
{
    class Node<T,K> where K: IComparable where T : class
    {
        public K Key { get; set;}
        public T Data { get; set; }
        public Node<T, K> Left { get; set; }
        public Node<T, K> Right { get; set; }
        
        public Node(K Key, T Data) 
        {
            this.Key=Key;
            this.Data = Data;
            this.Left = null;
            this.Right = null;
        }
    }
    class BinaryExpressionTree<T,K> where K: IComparable  where T : class
    {
        public Node<T, K> Root { get; private set; }
        public BinaryExpressionTree()
        {
            this.Root = null;
            
        }
        //public void Build(List<K> key, List<T> cegek)
        //{
        //    for (int i = 0; i < key.Count; i++)
        //    {
        //        Root = Build(Root, key[i], cegek[i]);
        //    }
            
                
            
        //}
        public void Build(K key, T ceg)
        {
           
                Root = Build(Root, key, ceg);
           
        }
        public  Node<T, K> Build(Node<T, K> root,K key,T ceg)
        {
           
            if (root==null)
            {
                Node<T, K> uj =new Node<T, K>(key, ceg);
                return uj;

            }

            
            if (key.CompareTo(root.Key)<0)
            {
                root.Left=Build(root.Left,key,ceg);
            }
            else if (key.CompareTo(root.Key) > 0)
            {
                root.Right = Build(root.Right,key, ceg);
            }
            else
            {
                throw new KulcsUtkozes(key.ToString(),$"Újra nem lehet felvenni a {key.ToString()} céget");
            }
            return root;
        }
        string vissza;
        public override string ToString()
        {
            vissza = "";
            return Root == null ? "" : ToString(Root);
        }
        string ToString(Node<T, K> node)
        {

            if (node != null)
            {
                
                ToString(node.Left);
                vissza += node.Key + ";";
                ToString(node.Right);
               

            }
            return vissza;
        }

        public T Keres(Node<T, K> p, K key)
        {
            
            if (p!=null)
            {
                if (p.Key.CompareTo(key)>0)
                {
                    return Keres(p.Left, key);
                }
                else
                {
                    if (p.Key.CompareTo(key) < 0)
                    {
                        return Keres(p.Right, key);
                    }
                    else
                    {
                        return p.Data;
                    }
                }
            }
            throw new KeresHiba($"Nincs {key.ToString()} nevű cég! ");
        }

        

        public void deleteKey(K key) { Root = deleteRec(Root, key); }

        Node<T, K> minValue(Node<T, K> root)
        {
            Node<T, K> minv=new Node<T, K>(root.Key, root.Data);
            
            while (root.Left != null)
            {
                minv.Key = root.Left.Key;
                minv.Data=root.Left.Data;
                
                
                root = root.Left;
            }
            return minv;
        }
        Node<T, K> deleteRec(Node<T, K> root, K key)
        {
            T elem = Keres(root, key);
            if (root == null)
                return root;

            if (key.CompareTo(root.Key)<0)
                root.Left = deleteRec(root.Left, key);
            else if (key.CompareTo(root.Key) > 0)
                root.Right = deleteRec(root.Right, key);
            else
            {
                if (root.Left == null)
                    return root.Right;
                else if (root.Right == null)
                    return root.Left;

                Node<T, K> seged= minValue(root.Right);
                root.Key = seged.Key;
                root.Data = seged.Data;
                


                root.Right = deleteRec(root.Right, root.Key);
            }
            return root;
        }

    }
}
