namespace server.Models.Domain
{
    public class JobExecutionLog
    {
        public int ID { get; set; }
        public string JobName { get; set; }
        public DateTime ExecutionTime { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }

        public JobExecutionLog()
        {
        }
    }
 
}
