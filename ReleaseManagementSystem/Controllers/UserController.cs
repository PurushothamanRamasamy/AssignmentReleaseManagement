using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReleaseManagementSystem.Models;

namespace ReleaseManagementSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        ReleaseManagementContext db = new ReleaseManagementContext();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(EmployeeDetails employee)
        {
            if(employee!=null)
            {
                try
                {
                    db.EmployeeDetails.Add(employee);
                    db.SaveChanges();
                    ViewBag.Msg = "User Registered Successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Msg =ex.Message;

                }
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(EmployeeDetails employeeLoginDetails)
        {
            EmployeeDetails employee = db.EmployeeDetails.FirstOrDefault(emp => emp.UserName == employeeLoginDetails.UserName && emp.Password == employeeLoginDetails.Password);
            if (employee != null && employee.Role == "Developer")
            {
                return RedirectToAction("ViewDeveloperProjects", new { Emp_Id = employee.Employee_Id });
            }
            if (employee != null && employee.Role == "TeamLead")
            {
                return RedirectToAction("ViewProjectsByTeamLead", new { empId = employee.Employee_Id });
            }

            else
            {
                return View();
            }
            
        }

        public ActionResult AddProjectsByManager()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProjectsByManager(Projects projects)
        {
            if(projects!=null)
            {
                try
                {
                    db.Projects.Add(projects);
                    db.SaveChanges();
                    ViewBag.ProMsg = "Projects Added Successfully";
                }
                catch (Exception ex)
                {

                    ViewBag.ProMsg = "Error in Adding Projects "+ex.Message;
                }
            }
          
            return View();
        }

        public ActionResult ManagerAssignProjectToTeamLead()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult ManagerAssignProjectToTeamLead(MangerAssignProject mangerAssignProject)
        {

            try
            {
                Projects assignProject = db.Projects.FirstOrDefault(pro => pro.Project_Id == mangerAssignProject.Project_Id);
                assignProject.TeamLead_Id = mangerAssignProject.TeamLead_Id;
                db.SaveChanges();
                ViewBag.AssignMessage = "Project Assigned";
            }
            catch (Exception ex)
            {

                ViewBag.AssignMessage = ex.Message;
            }
            return View();
        }

        public ActionResult ViewProjectsByTeamLead(string empId)
        {
            List<ViewProjectbyTeamLeader> myProjects = new List<ViewProjectbyTeamLeader>();
            List<Projects> projects = db.Projects.Where(pro => pro.TeamLead_Id == empId).ToList();

            foreach (var item in projects)
            {
                ViewProjectbyTeamLeader viewProjectbyTeamLeader = new ViewProjectbyTeamLeader();
                viewProjectbyTeamLeader.Project_Id = item.Project_Id;
                viewProjectbyTeamLeader.ProjectName = item.Project_Name;
                viewProjectbyTeamLeader.ExpectedStartDate = item.Expected_Start_Date;
                viewProjectbyTeamLeader.ExpectedEndDate = item.Expected_End_Date;
                viewProjectbyTeamLeader.TeamLead = item.TeamLead_Id;

                myProjects.Add(viewProjectbyTeamLeader);
            }

            return View(myProjects);
        }

        public ActionResult AssignModulesByTeamLead(string tlid, string pId)
        {
            Projects projects = db.Projects.FirstOrDefault(pro => pro.Project_Id == pId && pro.TeamLead_Id == tlid);
            AddModule addModule = new AddModule();
            if(projects!=null)
            {
                addModule.Project_ID = projects.Project_Id;
                addModule.ProjectName = projects.Project_Name;
                addModule.Num_Ofmodules = projects.No_Of_Modules;
            }
            return View(addModule);
        }
        [HttpPost]
        public ActionResult AssignModulesByTeamLead(AddModule addModule)
        {
            if(addModule!=null)
            {
                try
                {
                    Projects projects = db.Projects.FirstOrDefault(projec => projec.Project_Id == addModule.Project_ID);
                    ProjectModules projectModules = new ProjectModules();

                    projects.No_Of_Modules = addModule.Num_Ofmodules;

                    projectModules.ProjectId = addModule.Project_ID;
                    projectModules.Module_Name = addModule.ModuleName;
                    projectModules.Assigned_Dev_Id = addModule.Assign_Developer;
                    projectModules.Assigned_Tester_Id = addModule.Assign_Tester;
                    db.ProjectModules.Add(projectModules);
                    db.SaveChanges();
                    ViewBag.assMod = "Module assigned";
                }
                catch (Exception ex)
                {

                    ViewBag.assMod = ex.Message;
                }
            }
            return View();
        }

        public ActionResult ViewDeveloperProjects(string Emp_Id)
        {

            List<Developer> developers = new List<Developer>();
            List<ProjectModules> modules = db.ProjectModules.Where(mod => mod.Assigned_Dev_Id == Emp_Id).ToList<ProjectModules>();
            foreach (var item in modules)
            {
                Developer developer = new Developer();
                Projects project = db.Projects.FirstOrDefault(pro => pro.Project_Id == item.ProjectId);
                List<Bugs> bugs = db.Bugs.Where(b => b.Module_Name == item.Module_Name).ToList();
                developer.Project_Id = item.ProjectId;
                developer.Module_Name = item.Module_Name;
                developer.Dev_status = item.Status_Dev;
                developer.Project_Name = project.Project_Name;
                developer.Test_Status = item.Status_Tester;
                developer.Bugs_Id.AddRange(bugs.Select(bugid => bugid.Bug_Id).ToList());
                developer.Bug_Description.AddRange(bugs.Select(bugdes => bugdes.Bug_Description).ToList());
                developer.Bug_status.AddRange(bugs.Select(bugsts => bugsts.Bug_Status).ToList());

                developers.Add(developer);

            }
            return View(developers);
        }
    }
}