using System;
using System.Threading;
using System.Collections.Generic;

SimpleObserverTest();

public void SimpleObserverTest()
{
  Console.WriteLine("简单实现的观察者模式：");
  Console.WriteLine("=======================");
  //1、初始化问答按钮
  var AnswerButton = new AnswerButton();

  //2、声明提问者
  var bigGod = new AnsweringMan("大神");

  //3、给提问者添加到提问器
  AnswerButton.AddSubscriber(bigGod);

  //4、循环提问
  while (bigGod.AnswerCount < 5)
  {
      AnswerButton.Answering();
      Console.WriteLine("-------------------");
      //睡眠5s
      Thread.Sleep(5000);
  }
}

public enum StudentType
{
    敬嘉,
    伟超,
    霖颖,
    宋菊,
    雯雯,
    心雨,
    辛如
}

/// <summary>
///     问答工具抽象类
///     用来维护订阅者列表，并通知订阅者
/// </summary>
public abstract class AnswerTool
{
    private readonly List<ISubscriber> _subscribers;

    protected AnswerTool()
    {
        _subscribers = new List<ISubscriber>();
    }

    public void AddSubscriber(ISubscriber subscriber)
    {
        if (!_subscribers.Contains(subscriber))
            _subscribers.Add(subscriber);
    }

    public void RemoveSubscriber(ISubscriber subscriber)
    {
        if (_subscribers.Contains(subscriber))
            _subscribers.Remove(subscriber);
    }

    public void Notify(StudentType type)
    {
        foreach (var subscriber in _subscribers)
            subscriber.Update(type);
    }
}

/// <summary>
///     抢答器
/// </summary>
public class AnswerButton : AnswerTool
{
    public void Answering()
    {
        Console.WriteLine("开始提问！");
        var type = (StudentType) new Random().Next(0, 5);
        Console.WriteLine("问题器：叮叮叮，{0}抢答成功", type);
        Notify(type);
    }
}

/// <summary>
///     订阅者（观察者）接口
///     由具体的订阅者实现Update()方法
/// </summary>
public interface ISubscriber
{
    void Update(StudentType type);
}

/// <summary>
///     提问者实现观察者接口
/// </summary>
public class AnsweringMan : ISubscriber
{
    public AnsweringMan(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public int AnswerCount { get; set; }

    public void Update(StudentType type)
    {
        AnswerCount++;
        Console.WriteLine("{0}：[{2}]回答问题，已经回答{1}个问题！", Name, AnswerCount, type);
    }
}