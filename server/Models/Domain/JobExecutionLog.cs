namespace server.Models.Domain
{
    public class JobExecutionLog
    {
        public int ID { get; set; }
        public string JobName { get; set; }
        public DateTime ExecutionTime { get; set; }
        public string Status { get; set; }
        public string? ErrorMessage { get; set; }

        public JobExecutionLog(int iD, string jobName, DateTime executionTime, string status, string? errorMessage)
        {
            ID = iD;
            JobName = jobName;
            ExecutionTime = executionTime;
            Status = status;
            ErrorMessage = errorMessage;
        }

        public JobExecutionLog(string jobName, DateTime executionTime, string status, string? errorMessage)
        {
            JobName = jobName;
            ExecutionTime = executionTime;
            Status = status;
            ErrorMessage = errorMessage;
        }
    }
 
}
