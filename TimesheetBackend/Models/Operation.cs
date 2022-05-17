namespace TimesheetBackend.Models
{
    public class Operation
    {
        public int EmployeeID { get; set; }
        public int CustomerID { get; set; } //kyssäri pois, otettiin videolla?
        public int WorkAssignmentID { get; set; }
        public string OperationType { get; set; }
        public string Comment { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
