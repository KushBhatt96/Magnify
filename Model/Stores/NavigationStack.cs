using Magnify.ViewModel;

namespace Magnify.Model.Stores
{
    public class NavigationStack
    {
        public NavigationNode? Head { get; set; }
        public int Count { get; set; }

        public NavigationStack()
        {
            Head = null;
        }

        public void Push(NavigationNode node)
        {
            if(node == null)
            {
                // TODO: Maybe add logging
                return;
            }

            node.Next = Head;
            Head = node;
            Count++;
        }

        public NavigationNode? Pop()
        {
            if(Head == null)
            {
                return null;
            }

            NavigationNode temp = Head;
            Head = Head.Next;
            temp.Next = null;
            Count--;
            return temp;
        }

        public BaseViewModel? Peek()
        {
            if(Head == null)
            {
                return null;
            }

            return Head.ViewModel;
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }
    }

    public class NavigationNode
    {
        public BaseViewModel ViewModel { get; set; }

        public NavigationNode? Next { get; set; }

        public NavigationNode(BaseViewModel viewModel)
        {
            ViewModel = viewModel;
            Next = null;
        }
    }
}
