using System;
using System.Collections.Generic;

namespace Library
{
    public class Calculator
    {
        private Dictionary<string, Func<int, int, int>> _ops = 
                    new Dictionary<string, Func<int, int, int>>
                    {
                        {"Add", (a,b)=> a + b},
                        {"Sub", (a,b)=> a - b},
                        {"Mul", (a,b)=> a * b},
                        {"Div", (a,b)=> a / b},
                        {"Mod", (a,b)=> a % b},
                    };

        public int Eval(int lhs, string op, int rhs){
            return _ops[op](lhs, rhs);
         }

        public void AddOperator(string name, Func<int, int, int> function){
            _ops[name] = function;
        }
       
        
    }
}