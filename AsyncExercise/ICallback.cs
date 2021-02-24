using System;
namespace AsyncExercise
{
    public interface ICallback
    {
        void WhenResultReceived(int[] result);
    }
}
