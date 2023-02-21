using System.Linq;
using System;

void main()
{
    var a = new QuadraticEquation("3x^2-4x+94");
    for (int i = 0; i < a.Roots.Length; i++)
    {
        Console.WriteLine(a.Roots[i]);
    }
}

main();


class QuadraticEquation
{
    private int a;
    private int b;
    private int c;


    private bool isValid = true;
    
    public double[] Roots 
    { get
        {
            if (_roots == null)
                CalculateRoots();
            return _roots;
        }
    }

    private double[]? _roots;

    public QuadraticEquation(string equation)
    {
        for (int i = 0; i < equation.Length; i++)
        {
            if (equation[i] != '-')
                continue;
            equation = equation.Insert(i, "+");
            i++;
        }
        
        string[] splited = equation.Split('+');
        List<string> splitedL = splited.ToList();
        splitedL.Remove("");
        splited = splitedL.ToArray();
        char[] eqVars = equation.ToList().FindAll(x => char.IsLetter(x)).Distinct().ToArray();
        if (eqVars.Length != 1)
            isValid = false;
        char unknownVarChar = eqVars[0];

        for (int i = 0; i < splited.Length; i++)
        {
            if (splited[i][0] == unknownVarChar)
                splited[i] = splited[i].Insert(0, "1");
            else if (splited[i][0] == '-' && splited[i][1] == unknownVarChar)
                splited[i] = splited[i].Insert(1, "1");
        }
        
        for (int i = 0; i < splited.Length; i++)
        {
            if (!splited[i].Contains(unknownVarChar))
                c += int.Parse(splited[i]);
            else if (!splited[i].Contains('^'))
                b += int.Parse(splited[i].Replace(unknownVarChar.ToString(), ""));
            else
                a += int.Parse(splited[i].Split(unknownVarChar)[0]);
        }

        Console.WriteLine();
    }

    private void CalculateRoots()
    {
        float d = b * b - 4 * a * c;
        if (d > 0)
        {
            _roots = new double[] {(-b+Math.Sqrt(d))/(2*a), (-b-Math.Sqrt(d))/(2*a) };
        }
        else if (d == 0)
        {
            _roots = new double[] { -b*1.0 / (2 * a) };
        }
        else 
            _roots = new double[] { };
    }
}