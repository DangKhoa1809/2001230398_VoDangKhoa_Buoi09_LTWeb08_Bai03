using LTWeb08_Bai03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTWeb08_Bai03.Controllers
{
    public class DeparmentController : Controller
    {
        // GET: Deparment
        QL_NhanSuEntities data = new QL_NhanSuEntities();

        public ActionResult SideBarDept()
        {
            var dsPB = data.Deparments.OrderBy(d => d.Name).ToList();
            return View(dsPB);
        }

        public ActionResult EmployeesByDept(int id)
        {
            var dept = data.Deparments.FirstOrDefault(d => d.DeptId == id);
            if (dept == null) return HttpNotFound();

            var employees = data.Employees.Where(e => e.DeptId == id).OrderBy(e => e.Id).ToList();
            ViewBag.DeptName = dept.Name;
            return View(employees);
        }
    }
}