using System;
public interface IGridGenerator
{
    public event Action<ICell[,]> OnEmptyGridGenerated;
    public void GenerateEmptyGrid(int width, int height);
}
