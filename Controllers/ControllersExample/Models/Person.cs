using System;

namespace ControllersExample.Models;

public class Person(Guid id, string name)
{
    public Guid Id { get; set; } = id;
    public string? Name { get; set; } = name;
}
