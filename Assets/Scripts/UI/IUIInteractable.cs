public enum Type
{
    Up, Down, Left, Right, Accept, Cancel
}

public interface IUIInteractable
{
   
    void Interact(Type type, int index);

}
