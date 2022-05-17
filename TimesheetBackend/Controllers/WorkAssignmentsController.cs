using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetBackend.Models;


namespace TimesheetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkAssignmentsController : ControllerBase
    {
        private readonly tuntidbContext db = new tuntidbContext();

        [HttpGet]

        public List<WorkAssignment> GetWorkAssignments()
        {
            var wa = db.WorkAssignments.Where(w => w.Active == true && w.Completed == false);
            return wa.ToList();
        }

        [HttpPost]
        [Route("")]
        public bool StartStop (Operation op)
        {
            if (op == null)
            {
                return false;
            }

            WorkAssignment assignment =(from w in db.WorkAssignments
                                                    where (w.Active == true) &&
                                                    (w.IdWorkAssignment == op.WorkAssignmentID)
                                                    select w).FirstOrDefault();

            if (assignment == null)
            {
                return false;
            }

            else if (op.OperationType == "start")
            {
                if (assignment.InProgress == true || assignment.Completed == true)
                {
                    return false;
                }

                assignment.InProgress = true;
                assignment.WorkStartedAt = DateTime.Now.AddHours(2); // tarkista kellonaika vielä, riippuen minkä maan serverillä backend on
                assignment.LastModifiedAt = DateTime.Now.AddHours(2);

                db.SaveChanges();

                Timesheet newEntry = new Timesheet()
                {
                    IdWorkAssignment = op.WorkAssignmentID,
                    StartTime = DateTime.Now.AddHours(2),
                    Active = true,
                    IdEmployee = op.EmployeeID,
                    IdCustomer = op.CustomerID,
                    CreatedAt = DateTime.Now.AddHours(2),
                    Comments = op.Comment,
                    Longitude = op.Longitude,
                    Latitude = op.Latitude,
                };

                db.Timesheets.Add(newEntry);
                db.SaveChanges();
                return true;
            }

            else
            {
                if (assignment.InProgress == false || assignment.Completed == true)
                {
                    return false;
                }

                assignment.InProgress = false;
                assignment.CompletedAt = DateTime.Now.AddHours(2);
                assignment.Completed = true;
                assignment.LastModifiedAt = DateTime.Now.AddHours(2);
                db.SaveChanges();

                Timesheet timesh = (from t in db.Timesheets 
                                    where (t.Active == true) &&
                                    (t.IdWorkAssignment == op.WorkAssignmentID)
                                    select t).FirstOrDefault();

                timesh.StopTime = DateTime.Now.AddHours(2);
                timesh.LastModifiedAt = DateTime.Now.AddHours(2);
                timesh.Comments = op.Comment;   
                timesh.Longitude = op.Longitude;
                timesh.Latitude = op.Latitude;
                db.SaveChanges();

                return true;
            }
        }
    }
}
