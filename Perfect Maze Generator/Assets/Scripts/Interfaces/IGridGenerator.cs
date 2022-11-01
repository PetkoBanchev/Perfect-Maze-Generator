using System;
public interface IGridGenerator
{
    public CellType CellType { get; }

    public event Action<ICell[,]> OnEmptyGridGenerated;
    public void GenerateEmptyGrid();
}
