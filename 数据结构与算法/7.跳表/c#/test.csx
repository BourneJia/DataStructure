using System;

var a = test.getNextLevel();

Print(a);


public class test {
    public static int getNextLevel(){
      int lvl = 0;

      //如果随机数大于等于0.5，且在规定范围内，就进行返回，否则就提升级别
      while ((new Random()).NextDouble() < 0.5 && lvl <= 1 && lvl < 32)
        lvl++;

      return lvl;
  }
}