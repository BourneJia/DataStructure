using System;


public class HashTable<K,V>{
  private static int DefaultCap = 8;

  //private static float LoadFactor = 0.75f;

  private Entry<K, V>[] table;

  private int size = 0;
  private int use = 0;

  public HashTable(){
    table = new Entry<K, V>[DefaultCap];
  }

  public void Put(K key, V value){
    int index = hash(key);

    if(table[index] == null)
      table[index] = new Entry<K, V>(null);
    
    Entry<K,V> tmp = table[index];

    if(tmp.next == null){
      tmp.next = new Entry<K, V>(key, value,null);
      size++;
      use++;
    } else {
      do{
        tmp = tmp.next;
        if (tmp.key.Equals(key)){
          tmp.value = value;
          return;
        } 
      } while (tmp.next != null);

      Entry<K, V> temp = table[index].next;
      table[index].next = new Entry<K, V>(key , value, temp);
      size++;
    }
  }

  public void Remove(K key){
    int index = hash(key);
    Entry<K,V> e = table[index];
    if (e ==null || e.next == null){
      return;
    }

    Entry<K, V> pre;
    Entry<K, V> headNode = table[index];

    do {
      pre = e;
      e = e.next;
      if (key.Equals(e.key)) {
        pre.next = e.next;
        size--;
        if ( headNode.next == null)
          use--;
        return;
      }
    } while(e.next != null);
  }

  public V Get(K key){
    int index = hash(key);
    Entry<K,V> e = table[index];

    if (e == null || e.next == null)
      return default(V);

    while (e.next != null){
      e = e.next;
      if(key.Equals(e.key))
        return e.value;
    }
    return default(V);
  }

  private int hash(Object key){
    return key.GetHashCode();
  }
}

public class Entry<K, V>{
  public K key {get ; set;}

  public V value {get ;set ;}

  public Entry<K, V> next {get ; set;}

  public Entry(Entry<K, V> next){
    this.key = default(K);
    this.value = default(V);
    this.next = next;
  }

  public Entry(K key, V value, Entry<K, V> next){
    this.key = key;
    this.value = value;
    this.next = next;
  }
}