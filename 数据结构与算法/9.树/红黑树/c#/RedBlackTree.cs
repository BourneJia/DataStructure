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

  public RedBlackNode<T> Find(T itemData){
    if(mSize == 0)
      throw new Exception("The tree is empty");
    
    return find(Root,itemData);
  }

  public bool Insert(T itemData){
    var newNode = new RedBlackNode<T>(itemData);

    var success = insert(newNode);

    if(!success)
      throw new Exception("node does not insert");

    if(newNode != Root)
      if(newNode.ParentNode.Color != RedBlackColor.Black)
        adjustTreeAfterInsertion(newNode);

    Root.Color = RedBlackColor.Black;

    return success;
  }

  public bool Remove(T itemData){
    if(mSize == 0)
      throw new Exception("The tree is empty");

    var node = Find(itemData);

    var status = remove(node);

    if(!status){
      throw new Exception("The node not found");
    }

    return status;
  }

  //查询操作
  private RedBlackNode<T> find(RedBlackNode<T> node,T itemData){
    if(node == null || node.Data.Equals(item))
      return node;

    if(node.Data.CompareTo(itemData) > 0)
      return find(node.LeftNode, itemData);

    if(node.Data.CompareTo(itemData) < 0)
      return find(node.RightNode, itemData);

    return null;
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
  private void rightRotate(RedBlackNode<T> node){
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
      //判断关注节点的父兄弟节点是否为黑色，且关注节点是其父节点的左子节点 CASE3
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

  //进行删除操作
  private bool remove(RedBlackNode<T> deleteNode){
    if(deleteNode == null)
      return false;

    if(deleteNode.Equals(Root) && deleteNode.LeftNode == null && deleteNode.RightNode == null)
      Root = null;
    else{
      //把删除的节点内容报错下，进行后续调整使用
      RedBlackNode<T> replaceNode;

      if(deleteNode.LeftNode == null && deleteNode.RightNode == null){
        replaceNode = deleteNode;
        transplant(deleteNode,null);
      } else if (deleteNode.RightNode != null && deleteNode.LeftNode == null){
        replaceNode = deleteNode.RightNode;
        transplant(deleteNode, deleteNode.RightNode);
      } else if (deleteNode.LeftNode != null && deleteNode.RightNode == null){
        replaceNode = deleteNode.LeftNode;
        transplant(deleteNode, deleteNode.LeftNode);
      } else {
        var replaceNodey = findMinNode(deleteNode.RightNode);
        replaceNode = replaceNodey.RightNode;

        if(replaceNodey.ParentNode == deleteNode){
          if(replaceNode != null)
            replaceNode.ParentNode = replaceNodey;
        } else {
          transplant(replaceNodey, replaceNodey.RightNode);
          replaceNodey.RightNode = deleteNode.RightNode;
          replaceNodey.RightNode.ParentNode = replaceNodey;
        }
        transplant(deleteNode,replaceNodey);
        replaceNodey.LeftNode = deleteNode.LeftNode;
        replaceNodey.LeftNode.ParentNode = replaceNodey;
        replaceNodey.Color = deleteNode.Color;

        if(Root == deleteNode){
          Root = replaceNodey;
          Root.ParentNode = null;
        }
      }

      if(deleteNode.Color == RedBlackColor.Black)
        adjustTreeAfterRemoval(replaceNode);
    }

    mSize--;

    return true;
  }

  /**类似BinarySearchTree进行删除后，需要进行调整操作.
    left
      case1 关注节点的兄弟节点是红色
      case2 关注节点的兄弟节点是黑色，且兄弟节点的两个子节点都是黑色
      case3 关注节点的兄弟节点是黑色，且兄弟节点的左节点是红色，右节点是黑色
      case4 关注节点的兄弟节点是黑色，且兄弟节点的右节点是红色
    right
      ......（与left类似，方向会有变化）
  */
  private void adjustTreeAfterRemoval(RedBlackNode<T> node){
    //当node不是根节点，且颜色为黑色时，进入循环
    while(!node.Equals(Root) && node.Color == RedBlackColor.Black){
      //判断node是否为父节点的左子节点
      if(node == node.ParentNode.LeftNode){
        //创建node的兄弟节点
        var sliceNode = node.ParentNode.RightNode;
        //case1
        if(sliceNode.Color == RedBlackColor.Red){
          //把node兄弟节点的颜色设置为黑色
          sliceNode.Color = RedBlackColor.Black;
          //设置node父节点的颜色为红色
          node.ParentNode.Color = RedBlackColor.Red;
          //把node父节点作为关注点进行旋转
          leftRotate(node.ParentNode);
          //旋转后node的兄弟节点变化了，需要重新设置sliceNode
          sliceNode = node.ParentNode.RightNode;
        }
        //case2
        if(sliceNode.LeftNode.Color == RedBlackColor.Black && sliceNode.RightNode.Color == RedBlackColor.Black){
          //设置node的兄弟节点为红色
          sliceNode = RedBlackColor.Red;
          //设置node为原本node的父节点，后续会调整颜色
          node = node.ParentNode;
        } 
        // case3
        else { 
          if(sliceNode.RightNode.Color == RedBlackColor.Black){
            //调整node兄弟节点的左节点颜色为黑色
            sliceNode.LeftNode.Color = RedBlackColor.Black;
            //调整node兄弟节点的颜色为红色
            sliceNode.Color = RedBlackColor.Red;
            //进行右旋转
            rightRotate(sliceNode);
            //由于进行了旋转，兄弟节点变更，需要重新设置下
            sliceNode = node.ParentNode.RightNode;
          }
          //case 4
          //设置兄弟节点的颜色为node父节点的颜色
          sliceNode.Color = node.ParentNode.Color;
          //再设置node父节点的颜色为黑色
          node.ParentNode.Color = RedBlackColor.Black;
          //再设置兄弟节点的右节点颜色为黑色
          sliceNode.RightNode.Color = RedBlackColor.Black;
          //进行左旋转
          leftRotate(node.ParentNode);
          node = Root;
        }
      }
      else {

      }
    }
    node.Color = RedBlackColor.Black;
  }

  //与BinarySearchTree中的replaceNodeInParent一致，进行删除时，对应节点与其子节点进行替换
  private void transplant(RedBlackNode<T> node, RedBlackNode<T> newNode){
    if(node.ParentNode == null)
      Root = newNode;
    else if(node.Equals(node.ParentNode.LeftNode))
      node.ParentNode.LeftNode = newNode;
    else
      node.ParentNode.RightNode = newNode;
    
    if(newNode != null)
      newNode.ParentNode = node.ParentNode;
  }

  //获取该节点中最小的分支节点，与BinarySearchTree中的函数一致
  private RedBlackNode<T> findMinNode(RedBlackNode<T> node){
    if (node == null)
      return node;
    
    var currentNode = node;

    while(currentNode.LeftNode != null)
      currentNode = currentNode.LeftNode;
    
    return currentNode;
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