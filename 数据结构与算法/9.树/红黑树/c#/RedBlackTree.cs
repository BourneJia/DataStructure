using System;

public class RedBlackTree<T> where T : IComparable<T> {
  private int mSize;
  private RedBlackNode<T> mRoot;

  //发现获取父节点的兄弟节点时，要返祖....然后辨别左右节点时的判断太长....因此加了个这个方法
  protected RedBlackNode<T> getSling(RedBlackNode<T> node){
    if(node == null || node.ParentNode == null)
      return null;  
      
    return node.Sibling;
  }

  public RedBlackTree(){
    mSize = 0;
    mRoot = null;
  }

  public int Size {
    get { return mSize;}
    set { mSize = value;}
  }

  public RedBlackNode<T> Root {
    get { return mRoot;}
    set { mRoot = value;}
  }

  //左旋转
  private void leftRotate(RedBlackNode<T> node){
    if(node.RightNode == null)
      throw new Exception("the node not rightNode");
    var replaceNode = node.RightNode;
    node.RightNode = replaceNode.LeftNode;

    if(replaceNode.LeftNode != null)
      replaceNode.LeftNode.ParentNode = node;
    
    replaceNode.ParentNode = node.ParentNode;

    //判断node是父节点的左边还是右边，进行重新赋值
    if(node.ParentNode == null)
      Root = replaceNode;
    else if (node == node.ParentNode.LeftNode)
      node.ParentNode.LeftNode = replaceNode;
    else
      node.ParentNode.RightNode = replaceNode;

    replaceNode.LeftNode = node;
    node.ParentNode = replaceNode;
  }

  //右旋转
  public void rightRotate(RedBlackNode<T> node){
    if(node.LeftNode == null)
      throw new Exception("the node not leftNode");

    var replaceNode = node.LeftNode;
    node.LeftNode = replaceNode.RightNode;

    if(replaceNode.RightNode != null)
      replaceNode.RightNode.ParentNode = node;

    replaceNode.ParentNode = node.ParentNode;

    if(node.ParentNode == null)
      Root = replaceNode;
    else if(node.ParentNode.LeftNode == node)
      node.ParentNode.LeftNode = replaceNode;
    else
      node.ParentNode.RightNode = replaceNode;

    replaceNode.RightNode = node;
    node.ParentNode = replaceNode;
  }

  //其实就是承接BinarySearchTree中的insert方法
  private bool insert(RedBlackNode<T> node){
    if(Root == null){
      Root = node;
      mSize++;
      return true;
    }

    if(node.ParentNode == null)
      node.ParentNode = Root;

    if(node.ParentNode.Data.CompareTo(node.Data) > 0){
      if(node.ParentNode.LeftNode == null){
        node.ParentNode.LeftNode = node;
        mSize++;
        return true;
      }
      node.ParentNode = node.ParentNode.LeftNode;
      return insert(node);
    }

    if(node.ParentNode.RightNode == null){
      node.ParentNode.RightNode = node;
      mSize++;
      return true;
    }

    node.ParentNode = node.ParentNode.RightNode;
    return insert(node);
  }

  private void adjustTreeAfterInsertion(RedBlackNode<T> currentNode){
    //插入的肯定为红色
    currentNode.Color = RedBlackColor.Red;

    //处理两个红色相邻问题 case1
    if(currentNode != null && currentNode != Root && currentNode.ParentNode.Color == Red){
      //判断父节点的兄弟是否为红色
      if(getSling(currentNode.ParentNode).Color == RedBlackColor.Red){
        //将关注节点的父节点设置为黑色
        currentNode.ParentNode.Color = RedBlackColor.Black;
        //将关注节点父节点的兄弟节点设置为黑色
        getSling(currentNode.ParentNode).Color = RedBlackColor.Black;
        //将关注节点的祖父节点设置为红
        currentNode.ParentNode.ParentNode.Color = RedBlackColor.Red;
        //将关注节点从currentnode变为current的祖父节点.进行递归操作
        //currentNode = currentNode.ParentNode.ParentNode;
        adjustTreeAfterInsertion(currentNode.ParentNode.ParentNode);
      }
      //判断父节点的兄弟节点是否为黑色，且关注节点是父节点的右子节点 case2
      else if(getSling(currentNode.ParentNode).Color == RedBlackColor.Black && currentNode.ParentNode.RightNode == currentNode){
        //设置关注节点变为原关注节点的父节点
        currentNode = currentNode.ParentNode;
        //对关注节点进行左旋转
        leftRotate(currentNode);
        //持续进行递归
        adjustTreeAfterInsertion(currentNode);
      }
      //判断关注节点的父兄弟节点是否为黑色，且关注节点是其父节点的左子节点
      else if(getSling(currentNode.ParentNode).Color == RedBlackColor.Black && currentNode.ParentNode.LeftNode == currentNode){
        //对关注节点进行右旋转
        rightRotate(currentNode);
        //将关注节点的父节点，以及关注节点的兄弟节点颜色互换
        var color = currentNode.ParentNode.Color;
        currentNode.ParentNode.Color = getSling(currentNode).Color;
        getSling(currentNode).Color = color;        
      }
    }

    //根节点一定要是黑色
    Root.Color = RedBlackColor.Black;
  }

}

public class RedBlackNode<T>{
  private T mData;
  private RedBlackColor mColor;
  private RedBlackNode<T> mLeftNode;
  private RedBlackNode<T> mRightNode;
  private RedBlackNode<T> mParentNode;

  public RedBlackNode(){
    mData = default(T);
    mColor = RedBlackColor.Red;
    mLeftNode = null;
    mRightNode = null;
    mParentNode = null;
  }

  public RedBlackNode(T value){
    mData = value;
    mColor = RedBlackColor.Red;
    mLeftNode = null;
    mRightNode = null;
    mParentNode = null;
  }

  public RedBlackNode(T value, RedBlackNode<T> leftNode, RedBlackNode<T> rightNode, RedBlackNode<T> parentNode){
    mData = value;
    mColor = RedBlackColor.Red;
    mLeftNode = leftNode;
    mRightNode = rightNode;
    mParentNode = parentNode;
  }

  public T Data {
    get { return mData;}
    set { mData = value;}
  }

  public RedBlackColor Color {
    get { return mColor;}
    set { mColor = value;}
  }

  public RedBlackNode<T> LeftNode{
    get { return mLeftNode;}
    set { mLeftNode = value;}
  }

  public RedBlackNode<T> RightNode{
    get { return mRightNode;}
    set { mRightNode = value;}
  }

  public RedBlackNode<T> ParentNode {
    get { return mParentNode;}
    set { mRightNode = value;}
  }

  public RedBlackNode<T> Sibling {
    get { return (this.ParentNode == null ? null : (this.ParentNode.LeftNode == this ? this.ParentNode.RightNode : this.ParentNode.LeftNode));}
  }
}

public enum RedBlackColor{
  Red   = 0,
  Black = 1
}