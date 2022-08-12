using System;

namespace Game.Scripts.Common.CSharp {
    public enum CheckIndexOfAction {
        Insert,
        Find,
        Delete
    }

    public static class Common {
        /// <summary>
        /// 插入时，判断内容是否超出本身的存储
        /// </summary>
        /// <param name="action">根据操作方式进行不同的判断</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void CheckIndexOutByAction(CheckIndexOfAction action, int a, int b) {
            switch (action) {
                case CheckIndexOfAction.Insert:
                    _CheckIndex1(a, b);
                    break;
                case CheckIndexOfAction.Delete:
                    _CheckIndex2(a,b);
                    break;
                case CheckIndexOfAction.Find:
                    _CheckIndex2(a,b);
                    break;
            }
        }
 
        private static void _CheckIndex1(int a, int b) {
            if(a < 0 || a > b){
                throw new Exception("error: 数组下标超出数组范围");
            }
        }
        
        private static void _CheckIndex2(int a, int b) {
            if(a < 0 || a >= b){
                throw new Exception("error: 数组下标超出数组范围");
            }
        }
    }
}