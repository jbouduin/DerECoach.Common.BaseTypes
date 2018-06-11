using System.Threading.Tasks;

namespace DerECoach.Common.BaseTypes
{
    public static class TaskExtensions
    {
        public static bool IsRunningOrWaiting(this Task task)
        {
            switch (task.Status)
            {
                case TaskStatus.Running:
                case TaskStatus.WaitingForActivation:
                case TaskStatus.WaitingForChildrenToComplete:
                case TaskStatus.WaitingToRun:
                    return true;
                default:
                    return false;
            }
        } 
    }
}