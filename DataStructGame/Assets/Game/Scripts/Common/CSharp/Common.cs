using System;

namespace Game.Scripts.Common.CSharp {
    public static class Common {
        public static void S_CheckIndexOutForEqual(int a, int b) {
            if(a < 0 || a >= b){
                throw new Exception("error: 数组下标超出数组范围");
            }
        }

        public static void S_CheckIndexOutForNotEqual(int a, int b) {
            if(a < 0 || a > b){
                throw new Exception("error: 数组下标超出数组范围");
            }
        }
        
        public enum NodeDirection {
            Previous,
            Next
        }
    }
}