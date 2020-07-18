using System;
using System.Collections.Generic;
using System.Text;

class StringExpressionEvaluator
{
    static string[] number_strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    public static int evaluate(string expression)
    {
        char[] tokens = expression.ToCharArray();
        Stack<int> values = new Stack<int>();
        Stack<char> ops = new Stack<char>();
        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] == ' ')
            {
                continue;
            }
            if (tokens[i] >= '0' && tokens[i] <= '9')
            {
                StringBuilder sbuf = new StringBuilder();
                while (i < tokens.Length && tokens[i] >= '0' && tokens[i] <= '9')
                {
                    sbuf.Append(tokens[i++]);
                }
                values.Push(int.Parse(sbuf.ToString()));
            }
            else if (tokens[i] == '(')
            {
                ops.Push(tokens[i]);
            }

            // Closing brace encountered, solve entire brace  
            else if (tokens[i] == ')')
            {
                while (ops.Peek() != '(')
                {
                    values.Push(applyOp(ops.Pop(), values.Pop(), values.Pop()));
                }
                ops.Pop();
            }
            else if (tokens[i] == '+' || tokens[i] == '-' || tokens[i] == '*' || tokens[i] == '/')
            {
                while (ops.Count > 0 && hasPrecedence(tokens[i], ops.Peek()))
                {
                    values.Push(applyOp(ops.Pop(), values.Pop(), values.Pop()));

                }
                ops.Push(tokens[i]);
            }
        }
        while (ops.Count > 0)
        {
            values.Push(applyOp(ops.Pop(), values.Pop(), values.Pop()));
        }
        return values.Pop();
    }
    public static bool hasPrecedence(char op1, char op2)
    {
        if (op2 == '(' || op2 == ')')
        {
            return false;
        }
        if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
        {
            return false;
        }

        return true;
    }
    public static int applyOp(char op, int b, int a)
    {
        switch (op)
        {
            case '+': return a + b;
            case '-': return a - b;
            case '*': return a * b;
            case '/': return a / b;
        }
        return 0;
    }
    public static string StringChallenge(string str)
    {
        string mathexpression = "";
        // code goes here  
        bool ended = false;
        for (int i = 0; i < str.Length; i++)
        {
            bool isnumber = false;

            string substr = str.Substring(i);

            for (int j = 0; j < number_strings.Length; j++)
            {
                
                if (substr.Length >= number_strings[j].Length)
                {

                    if (number_strings[j] == substr.Substring(0, number_strings[j].Length))
                    {
                        
                        isnumber = true;
                        mathexpression = mathexpression + j.ToString();
                       

                        if (substr.Length == number_strings[j].Length)
                        {
                            
                            ended = true;
                            break;
                        }
                        else if (i > 0)
                        {
                            i = i + number_strings[j].Length - 1;
                        }
                        else
                        {
                            i = i + number_strings[j].Length - 2;
                        }
                        break;
                    }
                }

            }
            if (ended == true)
            {
                break;
            }
            else if (isnumber == false)
            {
                if (substr.Substring(0, 4) == "plus")
                {
                    mathexpression = mathexpression + " + ";
                    i = i + 3;
                }
                else if (substr.Substring(0, 5) == "minus")
                {
                    mathexpression = mathexpression + " - ";
                    i = i + 4;
                }
                else if(substr.Substring(0,6)=="divide")
                {
                    mathexpression = mathexpression + " / ";
                    i = i + 5;
                }
                else if(substr.Substring(0,8)=="multiply")
                {
                    mathexpression = mathexpression + " * ";
                    i = i + 7;
                }
            }

        }
        int result = evaluate(mathexpression);
        string resultstr = "";
        int start = 0;
        if (result < 0)
        {
            resultstr = resultstr + "negative";
            start = 1;
        }
        while (start < result.ToString().Length)
        {
            int index = int.Parse(result.ToString()[start].ToString());
            
            resultstr = resultstr + number_strings[index];
            start++;
        }

        return resultstr;

    }

    static void Main()
    {
        Console.WriteLine("----------------Instructions--------------------------");
        Console.WriteLine("'multiply' for Mulplication (*)");
        Console.WriteLine("'divide' for Division (/)");
        Console.WriteLine("'plus' for Sum(+)");
        Console.WriteLine("'minus' for Subtraction(-)");
        Console.WriteLine("Write the expression without spaces");
        Console.WriteLine("--------------------------------------------------------");
        Console.Write("Enter an expression in English i.e oneplustwo for 1+2:");
        string input = Console.ReadLine();
        // keep this function call here
        Console.Write("Solution of Expression: ");
        Console.WriteLine(StringChallenge(input));
    }

}