using System;

public class BinarySearchTree<T> where T : IComparable<T>{
  private int mSize;
  private Node<T> mRoot;
  
  public BinarySearchTree(){
    mSize = 0;
    mRoot = null;
  }

  public Node<T> Root{
    get { return mRoot;}
    set { mRoot = value;}
  }

  public T Find(T item){
    if(mSize == 0)
      throw new Exception("Tree is empty");
    
    //var node = Root;
    return find(Root, item).Data;
  }

  public bool Insert(T item){
    var newNode = new Node<T>();
    newNode.Data = item;
    return insert(newNode);
  }

  public bool Remove(T item){
    if(mSize == 0)
      throw new Exception("Tree is empty");
    var newNode = new Node<T>();
    newNode.Data = item;
    var isSuccess = remove(newNode);

    return isSuccess;
  }

  private Node<T> find(Node<T> node, T item){
    if(node == null || node.Data.Equals(item))
      return node;

    if(node.Data.CompareTo(item) > 0)
      return find(node.LeftNode, item);

    if(node.Data.CompareTo(item) < 0)
      return find(node.RightNode, item);

    return null;
  }

  private bool insert(Node<T> nodeItem){
    if(mSize == 0){
      Root = nodeItem;
      mSize++;
      return true;
    }

    if(nodeItem.ParentNode == null)
      nodeItem.ParentNode = Root;

    if(nodeItem.ParentNode.Data.CompareTo(nodeItem.Data) > 0){
      if(nodeItem.ParentNode.LeftNode == null){
        nodeItem.ParentNode.LeftNode = nodeItem;
        mSize++;
        return true;
      }

      nodeItem.ParentNode = nodeItem.ParentNode.LeftNode;
      return insert(nodeItem);
    } 

    if(nodeItem.ParentNode.RightNode == null){
      nodeItem.ParentNode.RightNode = nodeItem;
      mSize++;
      return true;
    }

    nodeItem.ParentNode = nodeItem.ParentNode.RightNode;
    return insert(nodeItem);
  }

  private bool remove(Node<T> node){
    if(node == null)
      return false;

    var parent = node.ParentNode;

    if(node.LeftNode != null && node.RightNode != null){
      var minNode = findMinNode(node.RightNode);
      node.Data = minNode.Data;
      return remove(minNode);
    }

    if(node.LeftNode != null){
      replaceNodeInParent(node, node.LeftNode);
      mSize--;
    } else if(node.RightNode != null){
      replaceNodeInParent(node, node.RightNode);
      mSize--;
    } else {
      replaceNodeInParent(node, null);
      mSize--;
    }

    return true;
  }

  private void replaceNodeInParent(Node<T> node, Node<T> newNode){
    if(node.ParentNode != null){
      if(node.ParentNode.LeftNode.Equals(node))
        node.ParentNode.LeftNode = newNode;
    } else {
      node.ParentNode.RightNode = newNode;
    }

    if(newNode != null)
      newNode.ParentNode = node.ParentNode;
  }

  private Node<T> findMinNode(Node<T> node){
    if (node == null)
      return node;
    
    var currentNode = node;

    while(currentNode.LeftNode != null)
      currentNode = currentNode.LeftNode;
    
    return currentNode;
  }
}

public class Node<T>{
  private T mData;
  private Node<T> leftNode;
  private Node<T> rightNode;
  //用于插入与删除时的递归使用
  private Node<T> parentNode;

  public T Data {
    get { return mData;}
    set { mData = value;}
  }

  public Node<T> LeftNode {
    get { return leftNode;}
    set { leftNode = value;}
  }

  public Node<T> RightNode {
    get { return rightNode;}
    set { rightNode = value;}
  }

  public Node<T> ParentNode {
    get { return parentNode;}
    set { parentNode = value;}
  }
}