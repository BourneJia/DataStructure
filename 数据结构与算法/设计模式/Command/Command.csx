using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

test();

public void test(){

  if((new Random().Next(0,10) > 5)){
    Console.WriteLine("学生准备好了");
    //申明学生进行讲课命令
    Command StudentCommand = new StudentSpeakByCommand();

    Invoker invoker = new Invoker(StudentCommand);

    invoker.InovkerName = "大神";

    invoker.Invoke();

    Console.ReadLine();
  }
  else{
    Console.WriteLine("学生没有准备好");
      //申明老师进行讲课命令
    Command TeacherCommand = new TeacherSpeakByCommand();

    //申明调用者
    Invoker invoker = new Invoker(TeacherCommand);
    invoker.InovkerName = "大神";

    //下达命令
    invoker.Invoke();
  }

}


/// <summary>
/// 调用者角色
/// </summary>
public class Invoker
{
    /// <summary>
    /// 申明调用的命令，并用构造函数注入
    /// </summary>
    private readonly Command command;

    public string InovkerName { get; set; }

    public Invoker(Command command)
    {
        this.command = command;
    }

    /// <summary>
    /// 调用以执行具体命令
    /// </summary>
    public void Invoke()
    {
        Console.WriteLine(string.Format("『{0}』下达命令：{1}", this.InovkerName, this.command.CommandName));
        this.command.Execute();
    }
}

/// <summary>
/// 命令者角色
/// </summary>
public abstract class Command
{
    protected readonly Receiver receiver;

    public string CommandName { get; set; }

    public Command(Receiver receiver)
    {
        this.receiver = receiver;
    }

    /// <summary>
    /// 抽象执行具体命令方法
    /// 由之类实现
    /// </summary>
    public abstract void Execute();
}

/// <summary>
/// 学生上课命令
/// </summary>
public class StudentSpeakByCommand : Command
{
    string commandName = "学生上课";

    public StudentSpeakByCommand() :
        base(new StudentCenter())
    {
        base.CommandName = commandName;
    }

    public override void Execute()
    {
        this.receiver.Plan();
        this.receiver.Action();
    }
}

/// <summary>
/// 老师讲课命令
/// </summary>
public class TeacherSpeakByCommand : Command
{
    string commandName = "老师进行讲课";

    public TeacherSpeakByCommand() :
        base(new TeacherCenter())
    {
        base.CommandName = commandName;
    }

    public override void Execute()
    {
        this.receiver.Plan();
        this.receiver.Action();
    }
}


/// <summary>
/// 接收者角色
/// </summary>
public abstract class Receiver
{
    protected string ReceiverName { get; set; }

    //定义每个执行者都必须处理的业务逻辑
    public abstract void Plan();
    public abstract void Action();
}


/// <summary>
/// 作战中心
/// </summary>
public class StudentCenter : Receiver
{
    public StudentCenter()
    {
        this.ReceiverName = "学生中心";
    }
    public override void Plan()
    {
        Console.WriteLine(string.Format("{0}:制定讲课计划。", this.ReceiverName));
    }

    public override void Action()
    {
        Console.WriteLine(string.Format("{0}:开始进行讲课，讲课完成！", this.ReceiverName));
    }
}

/// <summary>
/// 老师中心
/// </summary>
public class TeacherCenter : Receiver
{
    public TeacherCenter()
    {
        this.ReceiverName = "老师中心";
    }
    public override void Plan()
    {
        Console.WriteLine(string.Format("{0}:制定讲课计划。", this.ReceiverName));
    }

    public override void Action()
    {
        Console.WriteLine(string.Format("{0}:开始进行讲课，讲课完成！", this.ReceiverName));
    }
}
