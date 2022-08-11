using System;


/**
该跳表结构中，add部分的最后步骤有些疑惑，需要再看下情况
*/
public class SkipList<T> where T : IComparable<T>{
  private int mCount {get; set;}
  private int mCurrentMaxLevel {get; set;}
  private Random mRandomizer {get; set;}

  private SkipListNode<T> mFirstNode {get;set;}

  private readonly int MaxLevel = 32;
  private readonly double Probability = 0.5;

  //用于添加数据时，随机动态更新索引表
  private int getNextLevel(){
    int lvl = 0;

    //如果随机数大于等于0.5，且在规定范围内，就进行返回，否则就提升级别
    while (mRandomizer.NextDouble() < Probability && lvl <= mCurrentMaxLevel && lvl < MaxLevel)
      lvl++;

    return lvl;
  }

  public SkipList(){
    mCount = 0;
    mCurrentMaxLevel = 1;
    mRandomizer = new Random();
    mFirstNode = new SkipListNode<T>(default(T),MaxLevel);

    //设置各个级别的头部
    for (int i = 0; i < MaxLevel; i++)
      mFirstNode.Forword[i] = mFirstNode;
  }

  public SkipListNode<T> Root{
    get { return mFirstNode;}
  }

  public bool IsEmpty {
    get { return Count == 0;}
  }

  public int Count {
    get { return mCount;}
  }

  public int Level {
    get { return mCurrentMaxLevel;}
  }

  public void Add(T dataItem){
    var current = mFirstNode;
    //被修改的节点（进行指向替换操作）
    var toBeUpdated = new SkipListNode<T>[MaxLevel];

    //进行遍历查找对应的节点
    for (int i = mCurrentMaxLevel - 1; i >= 0; i--) {
      while(current.Forword[i] != mFirstNode && current.Forword[i].Data.CompareTo(dataItem) < 0)
        current = current.Forword[i];
      toBeUpdated[i] = current;
    }

    //意义不明的操作，感觉可以不要
    current = current.Forword[0];

    //随机获取下个lvl，以及判定是否需要更新链表的等级
    int lvl = getNextLevel();
    if (lvl > mCurrentMaxLevel){
      for (int i = mCurrentMaxLevel; i < lvl; i++)
        toBeUpdated[i] = mFirstNode;
      
      mCurrentMaxLevel = lvl;
    }

    var newNode = new SkipListNode<T>(dataItem, lvl);

    //此处如果lvl=0，貌似就无法添加，但是lvl是有可能等于0的
    for(int i = 0; i < lvl; i++){
      newNode.Forword[i] = toBeUpdated[i].Forword[i];
      toBeUpdated[i].Forword[i] = newNode;
    }

    mCount++;
  }

  public bool Remove(T dataItem, out T deleted){
    var current = mFirstNode;
    var toBeUpdated = new SkipListNode<T>[MaxLevel];

    for(int i = mCurrentMaxLevel-1; i >= 0; i--){
      while(current.Forword[i] != mFirstNode && current.Forword[i].Data.CompareTo(dataItem) < 0)
        current = current.Forword[i];
      toBeUpdated[i] = current;
    }

    //获取需要删除节点的下一个指向，用于后续的指向替换
    current = current.Forword[0];

    if(current.Data.Equals(dataItem) == false){
      deleted = default(T);
      return false;
    }

    //删除操作
    for(int i = 0; i < mCurrentMaxLevel; i++){
      if(toBeUpdated[i].Forword[i] == current)
        toBeUpdated[i].Forword[i] = current.Forword[i];
    }

    mCount--;
    //检查是否需要删除更高的等级
    while(mCurrentMaxLevel > 1 && mFirstNode.Forword[mCurrentMaxLevel - 1] == mFirstNode)
      mCurrentMaxLevel--;

    deleted = current.Data;
    return true;
  }

  public bool Find(T item, out T result){
    var current = mFirstNode;

    for(int i = mCurrentMaxLevel-1; i >= 0; i--)
      while(current.Forword[i] != mFirstNode && current.Forword[i].Data.CompareTo(item) < 0)
        current = current.Forword[i];

    //上面遍历得到的节点是需要节点（item）的前一个，真正需要的是上面得到节点的下一个指向，因此进行再次赋值
    current = current.Forword[0];

    if(current.Data.Equals(item)){
      result = current.Data;
      return true;
    }

    result = default(T);
    return false;
  }
}

public class SkipListNode<T>{
  //临时方法，用来给数据进行排序使用，发现直接用compare 对比就行，ASCII进行对比
  //private int key;
  private T mData;
  private SkipListNode<T>[] mForwords;

  public SkipListNode(T value, int level){
    if (level < 0)
     throw new IndexOutOfRangeException();
    
    mData = value;
    Forword = new SkipListNode<T>[level];
  }

  //原本想通过key进行过排序，发现直接用compare 对比就行，ASCII进行对比
  // public int Key{
  //   get {return key;}
  //   set {key = value;}
  // }

  public T Data{
    get {return mData;}
    set {mData = value;}
  }

  public SkipListNode<T>[] Forword{
    get { return mForwords;}
    set { mForwords = value;}
  }

  public int Level{
    get { return Forword.Length;}
  }
}
