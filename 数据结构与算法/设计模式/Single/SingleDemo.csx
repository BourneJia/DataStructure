using System;

var singleDemo1 = SingleDemo.getInstance(1);
var singleDemo2 = SingleDemo.getInstance(2);

var singledemo3 = new SingleDemo();

Console.WriteLine(singleDemo1.dice());
Console.WriteLine(singleDemo2.dice());

public class SingleDemo
{
  private static SingleDemo demo1 = new SingleDemo();
  private static SingleDemo demo2 = new SingleDemo();

  private SingleDemo()
  {

  }

  public static SingleDemo getInstance(int whichOne)
  {
    if ( whichOne == 1)
    {
        if()
        return demo1;
    }
    else
    {
        return demo2;
    }
  }

  public int dice()
  {
    DateTime d = new DateTime();
    Console.WriteLine(d.ToString());

    Random r = new Random(d.Day);
    Console.WriteLine(r.ToString());
    int value = r.Next();
    Console.WriteLine(value.ToString());

    value = Math.Abs(value);
    value = value % 6;//取模

    value += 1;
    return value;
  }

}