namespace Interfaces
{
    public interface IOperationViewModel
    {
        string Key { get; }
        string Name { get; }

        IOperation Operation { get; set; }
    }
}
