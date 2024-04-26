using System;

public interface IHaveBar
{
    public event Action<int, int> OnValueChanded;
}
