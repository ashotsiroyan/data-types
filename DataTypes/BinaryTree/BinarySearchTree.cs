﻿using System;

namespace DataTypes
{
    public class BinarySearchTree<T> : IBinaryTree<T>
        where T : IComparable
    {
        private BinaryTreeNode<T> root;
        private int size;

        public void Insert(T data)
        {
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(data);

            Insert(node);
        }

        public void Insert(BinaryTreeNode<T> node)
        {
            if (root == null)
                root = node;
            else
                AddNode(node, ref root);

            ++size;
        }

        public BinaryTreeNode<T> Find(T data)
        {
            BinaryTreeNode<T> current = root;

            while (current != null)
            {
                int compare = data.CompareTo(current.Data);

                if (compare < 0)
                    current = current.Left;
                else if (compare > 0)
                    current = current.Right;
                else
                    return current;
            }

            return null;
        }

        public BinaryTreeNode<T> FindParent(BinaryTreeNode<T> node)
        {
            BinaryTreeNode<T> current = root;
            BinaryTreeNode<T> parent = null;

            while (current != null)
            {
                if (current == node)
                    return parent;

                parent = current;

                int compare = node.Data.CompareTo(current.Data);

                if (compare < 0)
                    current = current.Left;
                else if(compare > 0)
                    current = current.Right;
            }

            return null;
        }

        public Pair<T> FindPair(T data)
        {
            Pair<T> pair = new Pair<T>();
            pair.parent = null;
            pair.child = root;

            while (pair.child != null)
            {
                int compare = data.CompareTo(pair.child.Data);

                if (compare < 0)
                {
                    pair.parent = pair.child;
                    pair.child = pair.child.Left;
                }
                else if (compare > 0)
                {
                    pair.parent = pair.child;
                    pair.child = pair.child.Right;
                }
                else
                    break;
            }

            return pair;
        }

        public void Delete(T data)
        {
            Pair<T> pair = FindPair(data);

            if(pair.child != null)
            {
                if (pair.parent == null)
                    DeleteNode(ref root);
                else if (pair.parent.Left == pair.child)
                    DeleteNode(ref pair.parent.Left);
                else
                    DeleteNode(ref pair.parent.Right);

                --size;
            }
        }

        public void Clear()
        {
            root = null;
            size = 0;
        }

        public BinaryTreeNode<T> Root { get { return root; } }

        public int Size { get { return size; } }

        public LinkedList<T> TraverseInOrder()
        {
            LinkedList<T> list = new LinkedList<T>();
            TraverseInOrder(root, list);

            return list;
        }

        public LinkedList<T> TraversePreOrder()
        {
            LinkedList<T> list = new LinkedList<T>();
            TraversePreOrder(root, list);

            return list;
        }

        public LinkedList<T> TraversePostOrder()
        {
            LinkedList<T> list = new LinkedList<T>();
            TraversePostOrder(root, list);

            return list;
        }

        private void DeleteNode(ref BinaryTreeNode<T> node)
        {
            if (node.Left == null)
            {
                node = node.Right;
            }
            else if (node.Right == null)
            {
                node = node.Left;
            }
            else
            {
                BinaryTreeNode<T> current;

                for (current = node.Left; current.Right != null; current = current.Right)
                    continue;

                current.Right = node.Right;
                node = node.Left;
            }
        }

        private void TraverseInOrder(BinaryTreeNode<T> node, LinkedList<T> list)
        {
            if (node != null)
            {
                TraverseInOrder(node.Left, list);
                list.Add(node.Data);
                TraverseInOrder(node.Right, list);
            }
        }

        private void TraversePreOrder(BinaryTreeNode<T> node, LinkedList<T> list)
        {
            if (node != null)
            {
                list.Add(node.Data);
                TraversePreOrder(node.Left, list);
                TraversePreOrder(node.Right, list);
            }
        }

        private void TraversePostOrder(BinaryTreeNode<T> node, LinkedList<T> list)
        {
            if (node != null)
            {
                TraversePostOrder(node.Left, list);
                TraversePostOrder(node.Right, list);
                list.Add(node.Data);
            }
        }

        private void AddNode(BinaryTreeNode<T> node, ref BinaryTreeNode<T> root)
        {
            if (root == null)
            {
                root = node;
            }
            else
            {
                int compare = node.Data.CompareTo(root.Data);

                if (compare < 0)
                {
                    AddNode(node, ref root.Left);
                }
                else if (compare > 0)
                {
                    AddNode(node, ref root.Right);
                }
            }
        }

        public override string ToString()
        {
            LinkedList<T> list = TraverseInOrder();
            string treeString = "";

            foreach (T data in list)
            {
                treeString += data + " ";
            }

            return treeString;
        }
    }
}
