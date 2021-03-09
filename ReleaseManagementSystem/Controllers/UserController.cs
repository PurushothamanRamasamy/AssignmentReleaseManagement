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
        public  ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        ReleaseManagementContext db = new ReleaseManagementContext();
        public ActionResult Register()
        {
            EmployeeDetails employee = new EmployeeDetails();
            return View(employee);
        }
        [HttpPost]
        public ActionResult Register(EmployeeDetails employee)
        {
            if (ModelState.IsValid)
            {
                if (employee != null)
                {
                    try
                    {
                        db.EmployeeDetails.Add(employee);
                        db.SaveChanges();
                        ViewBag.Msg = "User Registered Successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Msg = ex.Message;

                    }
                }
            }

            return View();
            
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login employeeLoginDetails)
        {
            if (ModelState.IsValid)
            {
                EmployeeDetails employee = db.EmployeeDetails.FirstOrDefault(emp => emp.UserName == employeeLoginDetails.UserName && emp.Password == employeeLoginDetails.Password);
                if (employee != null && employee.Role == "Developer")
                {
                    ViewBag.UserDetails =":"+ employee.UserName + " Role :"+employee.Role;
                    Session["EmpId"] = employee.Employee_Id;
                    return RedirectToAction("ViewDeveloperProjects", new { Emp_Id = employee.Employee_Id });
                }
                if (employee != null && employee.Role == "TeamLead")
                {
                    ViewBag.UserDetails = employee.UserName + " Role :" + employee.Role;
                    Session["EmpId"] = employee.Employee_Id;
                    return RedirectToAction("ViewProjectsByTeamLead", new { empId = employee.Employee_Id });
                }
                if (employee != null && employee.Role == "Tester")
                {
                    ViewBag.UserDetails = employee.UserName + " Role :" + employee.Role;
                    Session["EmpId"] = employee.Employee_Id;
                    return RedirectToAction("ViewTesterProjects", new { eMpId = employee.Employee_Id });
                }
                if (employee != null && employee.Role == "Manager")
                {
                    ViewBag.UserDetails = employee.UserName + " Role :" + employee.Role;
                    Session["EmpId"] = employee.Employee_Id;
                    return RedirectToAction("ViewProjectsByManager", new { eMpId = employee.Employee_Id });
                }
            }
            ViewBag.LoginMesssage = "Invalid user name or password";
            return View();


        }

        public ActionResult ViewProjectsByManager(string eMpId)
        {
            if(Session["EmpId"]!=null)
            {
                List<Projects> projects = db.Projects.Where(project => project.Manager_Id == eMpId).ToList();
                return View(projects);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult AddProjectsByManager()
        {
            if (Session["EmpId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
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
                    return RedirectToAction("ViewProjectsByManager", new { eMpId = projects.Manager_Id });
                }
                catch (Exception ex)
                {

                    ViewBag.ProMsg = "Error in Adding Projects "+ex.Message;
                }
            }
          
            return View();
        }

        public ActionResult ManagerAssignProjectToTeamLead(string id)
        {
            if (Session["EmpId"] != null)
            {
                MangerAssignProject mangerAssignProject = new MangerAssignProject();
                Projects projects = db.Projects.FirstOrDefault(project => project.Project_Id == id);
                mangerAssignProject.Project_Id = projects.Project_Id;
                mangerAssignProject.TeamLead_Id = projects.TeamLead_Id;
                return View(mangerAssignProject);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult ManagerAssignProjectToTeamLead(MangerAssignProject mangerAssignProject)
        {

            if (Session["EmpId"]!=null)
            {
                try
                {
                    Projects assignProject = db.Projects.FirstOrDefault(pro => pro.Project_Id == mangerAssignProject.Project_Id);
                    assignProject.TeamLead_Id = mangerAssignProject.TeamLead_Id;
                    db.SaveChanges();
                    return RedirectToAction("ViewProjectsByManager", new { eMpId = assignProject.Manager_Id });
                }
                catch (Exception ex)
                {

                    ViewBag.AssignMessage = ex.Message;
                }
                return View();

            }
            else
            {
                return RedirectToAction("Login");
            }
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
                    return RedirectToAction("ViewModulesByTeamLead");
                }
                catch (Exception ex)
                {

                    ViewBag.assMod = ex.Message;
                }
            }
            return View();
        }
        public ActionResult ViewModulesByTeamLead()
        {
            List<ViewModulesbyTeamLeader> myModules = new List<ViewModulesbyTeamLeader>();
            List<ProjectModules> modules = db.ProjectModules.ToList();

            foreach (var item in modules)
            {
                ViewModulesbyTeamLeader viewModulesbyTeamLeader = new ViewModulesbyTeamLeader();
                viewModulesbyTeamLeader.ProjectId = item.ProjectId;
                viewModulesbyTeamLeader.Module_Name = item.Module_Name;
                viewModulesbyTeamLeader.Assigned_Dev_Id = item.Assigned_Dev_Id;
                viewModulesbyTeamLeader.Assigned_Tester_Id = item.Assigned_Tester_Id;
                viewModulesbyTeamLeader.Status_Dev = item.Status_Dev;
                viewModulesbyTeamLeader.Status_Tester = item.Status_Tester;
                myModules.Add(viewModulesbyTeamLeader);
            }

            return View(myModules);
        }
        public ActionResult ApproveModulesByTeamLead(string moduleId)
        {
            ProjectModules mymodules = db.ProjectModules.FirstOrDefault(pro => pro.Module_Name == moduleId);

            ApproveStatus approve = new ApproveStatus();
            if (mymodules != null)
            {
                approve.Project_ID = mymodules.ProjectId;
                approve.ModuleName = mymodules.Module_Name;
                approve.Status_Dev = mymodules.Status_Dev;
                approve.Status_Tester = mymodules.Status_Tester;
            }
            return View(approve);
        }
        [HttpPost]
        public ActionResult ApproveModulesByTeamLead(ApproveStatus approveStatus)
        {
            if (approveStatus != null)
            {
                try
                {
                    ProjectModules project = db.ProjectModules.FirstOrDefault(projec => projec.Module_Name == approveStatus.ModuleName && projec.ProjectId == approveStatus.Project_ID);

                    /*project.ProjectId = approveStatus.Project_ID;

                    project.Module_Name = approveStatus.ModuleName;
                    project.Status_Dev = approveStatus.Status_Dev;
                    project.Status_Tester = approveStatus.Status_Tester;*/
                    project.Approve_Status = !approveStatus.Approve_Status;

                    //db.ProjectModules.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("ViewModulesByTeamLead");
                    //ViewBag.Message="Approved";
                }
                catch (Exception ex)
                {

                    ViewBag.AssignedMod = ex.Message;
                }
            }

            return View();
        }


        public ActionResult ViewTesterProjects(string eMpId)
        {

            List<Tester> testers = new List<Tester>();
            List<ProjectModules> modules = db.ProjectModules.Where(mod => mod.Assigned_Tester_Id == eMpId).ToList<ProjectModules>();
            foreach (var item in modules)
            {
                Tester tester = new Tester();
                Projects project = db.Projects.FirstOrDefault(pro => pro.Project_Id == item.ProjectId);
                tester.Project_Id = item.ProjectId;
                tester.Module_Name = item.Module_Name;
                tester.Assign_Developer = item.Assigned_Dev_Id;
                tester.Dev_status = item.Status_Dev;
                testers.Add(tester);

            }
            return View(testers);
        }


        public ActionResult UpdateTesterStatus(string moduleId, string projectID)
        {
            ProjectModules mymodules = db.ProjectModules.FirstOrDefault(pro => pro.ProjectId == projectID && pro.Module_Name == moduleId);

            UpdateTesterStatus testerStatus = new UpdateTesterStatus();
            if (mymodules != null)
            {
                testerStatus.Project_ID = mymodules.ProjectId;
                testerStatus.ModuleName = mymodules.Module_Name;
                testerStatus.Assign_Developer = mymodules.Assigned_Dev_Id;
                testerStatus.Status_Dev = mymodules.Status_Dev;
            }
            return View(testerStatus);
        }
        [HttpPost]
        public ActionResult UpdateTesterStatus(UpdateTesterStatus update)
        {
            if (update != null)
            {
                try
                {
                    ProjectModules project = db.ProjectModules.FirstOrDefault(projec => projec.Module_Name == update.ModuleName && projec.ProjectId == update.Project_ID);
                    project.Status_Tester = !update.Update_Tester_Status;

                    //db.ProjectModules.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("ViewTesterProjects", new { eMpId = project.Assigned_Tester_Id });
                    //ViewBag.Message="Approved";
                }
                catch (Exception ex)
                {

                    ViewBag.AssignedMod = ex.Message;
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
                developer.ActualStartDate = project.Actual_Start_Date;
                developer.ActualEndDate = project.Actual_End_Date;
                developer.Project_Name = project.Project_Name;
                developer.Test_Status = item.Status_Tester;
                developer.Bug_Description.AddRange(bugs.Select(bugdes => bugdes.Bug_Description).ToList());
                developer.Bug_status.AddRange(bugs.Select(bugsts => bugsts.Bug_Status).ToList());

                developers.Add(developer);

            }
            return View(developers);
        }

        public ActionResult UpdateDevloperStatus(string proId,string modName)
        {
            ProjectModules projectModules = db.ProjectModules.FirstOrDefault(mod => mod.ProjectId == proId && mod.Module_Name == modName);

            
            return View(projectModules);
        }
        [HttpPost]
        public ActionResult UpdateDevloperStatus(ProjectModules getprojectModules)
        {
            try
            {
                ProjectModules projectModules = db.ProjectModules.FirstOrDefault(mod => mod.ProjectId == getprojectModules.ProjectId && mod.Module_Name == getprojectModules.Module_Name);
                projectModules.Status_Dev = true;
                db.SaveChanges();
                //ViewBag.sts = getprojectModules.Status_Dev+" "+ getprojectModules.Module_Name+" "+getprojectModules.ProjectId;
                return RedirectToAction("ViewDeveloperProjects", new { Emp_Id = getprojectModules.Assigned_Dev_Id });


            }
            catch (Exception ex)
            {
                throw ex;
                
            }

            
        }
        public ActionResult RaiseBug()
        {
            return View();
        }
    }
}

/*
 public JsonResult IsUserExists(string UserId)
        {
            return IsExist(UserId) ? Json(true, JsonRequestBehavior.AllowGet) : Json(false, JsonRequestBehavior.AllowGet);
        }
[RemoteAttribute("IsUserExists","Customer",ErrorMessage ="User already exists! Choose some other name")]
 */