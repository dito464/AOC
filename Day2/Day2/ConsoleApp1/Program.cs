// See https://aka.ms/new-console-template for more information

using Input;
using Solver;


Input.Input inputClass = new Input.Input();
string input = inputClass.input;
(int, int) answer = Day2.day2Solve(input);

Console.WriteLine($"{answer}");
