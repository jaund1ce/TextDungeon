using System;

public class Player
{
    public string Name { get; set; }
    public string Class { get; set; }
    public int Level { get; set; } = 1; 
    public int Attack { get; set; } = 10;
    public int Defense { get; set; } = 5;
    public int Health { get; set; } = 100;
    public int Gold { get; set; } = 1500;
}
