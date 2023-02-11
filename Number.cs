using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Calculator
{
    class Numbers
    {
        private float number;
        private char action;
        private Numbers Next;
        
        public Numbers(float number, char action)
        {
            this.number = number;
            this.action = action;
        }
        
        public string Result()
        {
            if (Next.action == '*' || Next.action == '÷')
            {
                _ = Next.action == '*' ? Next.number *= Next.Next.number : Next.number /= Next.Next.number;
                Change_place(Next, Next.Next);
            }
            else
            {
                _ = action == '+' ? number += Next.number : number -= Next.number;
                Change_place(this, Next);
            }

            if (Next != null)
                return Result();
            else
                return number.ToString();
        }

        private void Change_place(Numbers now, Numbers next)
        {
            now.action = next.action;
            now.Next = next.Next;
        }

        public void New_number(float number, char Action)
        {
            if (Next is null)
                Next = new Numbers(number, Action);
            else
                Next.New_number(number, Action);
        }

        public void Clear()
        {
            Next = null;
            number = 0;
            action = '+';
        }

        public void New_action(char action)
        {
            if (Next is null) this.action = action;
            else Next.New_action(action);
        }

        public void Proc(float number, char action)
        {
            if (Next is null) 
                Next = new Numbers(this.number * (number / 100), action);
            else Next.Proc(number, action);
        }
    }
}
